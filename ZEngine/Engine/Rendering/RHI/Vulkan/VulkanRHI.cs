﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Silk.NET.Core;
using Silk.NET.Core.Contexts;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using Silk.NET.Vulkan.Extensions.KHR;
using Silk.NET.Windowing;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Rendering.RHI.Vulkan
{
    public unsafe class VulkanRHI : AbstractRenderHardwareInterface
    {
        private struct QueueFamilyIndices
        {
            public uint? GraphicsFamily { get; set; }
            public uint? PresentFamily { get; set; }

            public bool IsComplete()
            {
                return GraphicsFamily.HasValue && PresentFamily.HasValue;
            }
        }

        private struct SwapChainSupportDetails
        {
            public SurfaceCapabilitiesKHR Capabilities { get; set; }
            public SurfaceFormatKHR[] Formats { get; set; }
            public PresentModeKHR[] PresentModes { get; set; }
        }

        public const int MaxFramesInFlight = 8;

        // Per Renderer state
        private Instance _instance;
        private Vk _vk;
        private SurfaceKHR _surface;
        private KhrSurface _vkSurface;
        private SwapchainKHR _swapchain;
        private KhrSwapchain _vkSwapchain;
        private DebugUtilsMessengerEXT _debugMessenger;
        private ExtDebugUtils _debugUtils;
        private PhysicalDevice _physicalDevice;
        private Device _device;
        private Queue _graphicsQueue;
        private Queue _presentQueue;
        private Image[] _swapchainImages;
        private Format _swapchainImageFormat;
        private Extent2D _swapchainExtent;
        private ImageView[] _swapchainImageViews;
        private RenderPass _renderPass;
        private PipelineLayout _pipelineLayout;
        
        private Framebuffer[] _swapchainFramebuffers;
        private CommandPool _commandPool;
        private CommandBuffer[] _commandBuffers;
        private Semaphore[] _imageAvailableSemaphores;
        private Semaphore[] _renderFinishedSemaphores;
        private Fence[] _inFlightFences;
        private Fence[] _imagesInFlight;
        private uint _currentFrame;

        // Per Object/Instance state
        private Pipeline _graphicsPipeline;

        private bool EnableValidationLayers { get; set; } = true;
        private IVkSurface VulkanSurface { get; }
        private string[] ValidationLayers { get; set; } = { "VK_LAYER_KHRONOS_validation" };
        private string[] InstanceExtensions { get; set; }= { ExtDebugUtils.ExtensionName };
        private string[] DeviceExtensions { get; set; }= { KhrSwapchain.ExtensionName };


        public VulkanRHI(IWindow window) : base(window)
        {
            VulkanSurface = window.VkSurface;
        }

        public override void Initialize()
        {
            CreateInstance();
            SetupDebugMessenger();
            CreateSurface();
            PickGraphicsDevice();
            CreateLogicalGraphicsDevice();
            CreateSwapChain();
            CreateImageViews();
            CreateRenderPass();
            CreateGraphicsPipeline();
            CreateFrameBuffers();
            CreateCommandPool();
            CreateCommandBuffers();
            CreateSyncObjects();
        }

        public override void DrawFrame(double deltaTime)
        {
            var fence = _inFlightFences[_currentFrame];
            _vk.WaitForFences(_device, 1, in fence, Vk.True, ulong.MaxValue);

            uint imageIndex;
            _vkSwapchain.AcquireNextImage
                (_device, _swapchain, ulong.MaxValue, _imageAvailableSemaphores[_currentFrame], default, &imageIndex);

            if (_imagesInFlight[imageIndex].Handle != 0)
            {
                _vk.WaitForFences(_device, 1, in _imagesInFlight[imageIndex], Vk.True, ulong.MaxValue);
            }

            _imagesInFlight[imageIndex] = _inFlightFences[_currentFrame];

            SubmitInfo submitInfo = new SubmitInfo { SType = StructureType.SubmitInfo };

            Semaphore[] waitSemaphores = { _imageAvailableSemaphores[_currentFrame] };
            PipelineStageFlags[] waitStages = { PipelineStageFlags.PipelineStageColorAttachmentOutputBit };
            submitInfo.WaitSemaphoreCount = 1;
            var signalSemaphore = _renderFinishedSemaphores[_currentFrame];
            fixed (Semaphore* waitSemaphoresPtr = waitSemaphores)
            {
                fixed (PipelineStageFlags* waitStagesPtr = waitStages)
                {
                    submitInfo.PWaitSemaphores = waitSemaphoresPtr;
                    submitInfo.PWaitDstStageMask = waitStagesPtr;

                    submitInfo.CommandBufferCount = 1;
                    var buffer = _commandBuffers[imageIndex];
                    submitInfo.PCommandBuffers = &buffer;

                    submitInfo.SignalSemaphoreCount = 1;
                    submitInfo.PSignalSemaphores = &signalSemaphore;

                    _vk.ResetFences(_device, 1, &fence);

                    if (_vk.QueueSubmit
                            (_graphicsQueue, 1, &submitInfo, _inFlightFences[_currentFrame]) != Result.Success)
                    {
                        throw new Exception("Failed to submit draw command buffer!");
                    }
                }
            }

            fixed (SwapchainKHR* swapchain = &_swapchain)
            {
                PresentInfoKHR presentInfo = new PresentInfoKHR
                {
                    SType = StructureType.PresentInfoKhr,
                    WaitSemaphoreCount = 1,
                    PWaitSemaphores = &signalSemaphore,
                    SwapchainCount = 1,
                    PSwapchains = swapchain,
                    PImageIndices = &imageIndex
                };

                _vkSwapchain.QueuePresent(_presentQueue, &presentInfo);
            }

            _currentFrame = (_currentFrame + 1) % MaxFramesInFlight;
        }

        public override void Deinitialize()
        {
            // Wait for device to become idle
            _vk.DeviceWaitIdle(_device);

            for (var i = 0; i < MaxFramesInFlight; i++)
            {
                _vk.DestroySemaphore(_device, _renderFinishedSemaphores[i], null);
                _vk.DestroySemaphore(_device, _imageAvailableSemaphores[i], null);
                _vk.DestroyFence(_device, _inFlightFences[i], null);
            }

            _vk.DestroyCommandPool(_device, _commandPool, null);

            foreach (var framebuffer in _swapchainFramebuffers)
            {
                _vk.DestroyFramebuffer(_device, framebuffer, null);
            }

            _vk.DestroyPipeline(_device, _graphicsPipeline, null);
            _vk.DestroyPipelineLayout(_device, _pipelineLayout, null);
            _vk.DestroyRenderPass(_device, _renderPass, null);

            foreach (var imageView in _swapchainImageViews)
            {
                _vk.DestroyImageView(_device, imageView, null);
            }

            _vkSwapchain.DestroySwapchain(_device, _swapchain, null);
            _vk.DestroyDevice(_device, null);

            if (EnableValidationLayers)
            {
                _debugUtils.DestroyDebugUtilsMessenger(_instance, _debugMessenger, null);
            }

            _vkSurface.DestroySurface(_instance, _surface, null);
            _vk.DestroyInstance(_instance, null);
        }

        private void CreateInstance()
        {
            _vk = Vk.GetApi();

            if (EnableValidationLayers && !CheckValidationLayerSupport())
            {
                throw new NotSupportedException("Validation layers requested, but not available!");
            }

            var appInfo = new ApplicationInfo
            {
                SType = StructureType.ApplicationInfo,
                PApplicationName = (byte*)Marshal.StringToHGlobalAnsi("Hello Triangle"),
                ApplicationVersion = new Version32(1, 0, 0),
                PEngineName = (byte*)Marshal.StringToHGlobalAnsi("No Engine"),
                EngineVersion = new Version32(1, 0, 0),
                ApiVersion = Vk.Version11
            };

            var createInfo = new InstanceCreateInfo
            {
                SType = StructureType.InstanceCreateInfo,
                PApplicationInfo = &appInfo
            };

            var extensions = (byte**)VulkanSurface.GetRequiredExtensions(out var extCount);
            var newExtensions = stackalloc byte*[(int)(extCount + InstanceExtensions.Length)];
            for (var i = 0; i < extCount; i++)
            {
                newExtensions[i] = extensions[i];
            }

            for (var i = 0; i < InstanceExtensions.Length; i++)
            {
                newExtensions[extCount + i] = (byte*)SilkMarshal.MarshalStringToPtr(InstanceExtensions[i]);
            }

            extCount += (uint)InstanceExtensions.Length;
            createInfo.EnabledExtensionCount = extCount;
            createInfo.PpEnabledExtensionNames = newExtensions;

            if (EnableValidationLayers)
            {
                createInfo.EnabledLayerCount = (uint)ValidationLayers.Length;
                createInfo.PpEnabledLayerNames = (byte**)SilkMarshal.MarshalStringArrayToPtr(ValidationLayers);
            }
            else
            {
                createInfo.EnabledLayerCount = 0;
                createInfo.PNext = null;
            }

            fixed (Instance* instance = &_instance)
            {
                if (_vk.CreateInstance(&createInfo, null, instance) != Result.Success)
                {
                    throw new Exception("Failed to create instance!");
                }
            }

            _vk.CurrentInstance = _instance;

            if (!_vk.TryGetInstanceExtension(_instance, out _vkSurface))
            {
                throw new NotSupportedException("KHR_surface extension not found.");
            }

            if (!_vk.TryGetDeviceExtension(_instance, _device, out _vkSwapchain))
            {
                throw new NotSupportedException("KHR_swapchain extension not found.");
            }

            Marshal.FreeHGlobal((IntPtr)appInfo.PApplicationName);
            Marshal.FreeHGlobal((IntPtr)appInfo.PEngineName);

            if (EnableValidationLayers)
            {
                SilkMarshal.FreeStringArrayPtr((IntPtr)createInfo.PpEnabledLayerNames, ValidationLayers.Length);
            }
        }

        private void SetupDebugMessenger()
        {
            if (!EnableValidationLayers) return;
            if (!_vk.TryGetInstanceExtension(_instance, out _debugUtils)) return;

            var createInfo = new DebugUtilsMessengerCreateInfoEXT();
            PopulateDebugMessengerCreateInfo(ref createInfo);

            fixed (DebugUtilsMessengerEXT* debugMessenger = &_debugMessenger)
            {
                if (_debugUtils.CreateDebugUtilsMessenger
                        (_instance, &createInfo, null, debugMessenger) != Result.Success)
                {
                    throw new Exception("Failed to create debug messenger.");
                }
            }
        }

        private void PopulateDebugMessengerCreateInfo(ref DebugUtilsMessengerCreateInfoEXT createInfo)
        {
            createInfo.SType = StructureType.DebugUtilsMessengerCreateInfoExt;
            createInfo.MessageSeverity = DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityVerboseBitExt |
                                         DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityWarningBitExt |
                                         DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityErrorBitExt;
            createInfo.MessageType = DebugUtilsMessageTypeFlagsEXT.DebugUtilsMessageTypeGeneralBitExt |
                                     DebugUtilsMessageTypeFlagsEXT.DebugUtilsMessageTypePerformanceBitExt |
                                     DebugUtilsMessageTypeFlagsEXT.DebugUtilsMessageTypeValidationBitExt;
            createInfo.PfnUserCallback = FuncPtr.Of<DebugUtilsMessengerCallbackFunctionEXT>(DebugCallback);
        }

        private uint DebugCallback(DebugUtilsMessageSeverityFlagsEXT messageSeverity, DebugUtilsMessageTypeFlagsEXT messageTypes, DebugUtilsMessengerCallbackDataEXT* pCallbackData, void* pUserData)
        {
            var message = $"{messageSeverity} {messageTypes} {Marshal.PtrToStringAnsi((IntPtr)pCallbackData->PMessage)}";
            switch (messageSeverity)
            {
                case DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityVerboseBitExt:
                    Debug.LogDebug(message, DebugLogCategories.Engine);
                    break;
                case DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityInfoBitExt:
                    Debug.Log(message, DebugLogCategories.Engine);
                    break;
                case DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityWarningBitExt:
                    Debug.LogWarning(message, DebugLogCategories.Engine);
                    break;
                case DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityErrorBitExt:
                    Debug.LogError(message, DebugLogCategories.Engine);
                    break;
                default:
                    Debug.Log(message, DebugLogCategories.Engine);
                    break;
            }
            
            // Vulkan expects us to always return false.
            return Vk.False;
        }

        private void CreateSurface()
        {
            _surface = VulkanSurface.Create<AllocationCallbacks>(_instance.ToHandle(), null).ToSurface();
        }

        private void PickGraphicsDevice()
        {
            var deviceCount = 0u;
            _vk.EnumeratePhysicalDevices(_instance, &deviceCount, null);

            if (deviceCount == 0)
            {
                throw new NotSupportedException("Failed to find GPUs with Vulkan support.");
            }

            var devices = stackalloc PhysicalDevice[(int)deviceCount];
            _vk.EnumeratePhysicalDevices(_instance, &deviceCount, devices);

            for (var i = 0; i < deviceCount; i++)
            {
                var device = devices[i];
                if (IsDeviceSuitable(device))
                {
                    _physicalDevice = device;
                    return;
                }
            }

            throw new Exception("No suitable device.");
        }

        private bool IsDeviceSuitable(PhysicalDevice device)
        {
            var indices = FindQueueFamilies(device);

            var extensionsSupported = CheckDeviceExtensionSupport(device);

            var swapChainAdequate = false;
            if (extensionsSupported)
            {
                var swapChainSupport = QuerySwapChainSupport(device);
                swapChainAdequate = swapChainSupport.Formats.Length != 0 && swapChainSupport.PresentModes.Length != 0;
            }

            return indices.IsComplete() && extensionsSupported && swapChainAdequate;
        }

        private SwapChainSupportDetails QuerySwapChainSupport(PhysicalDevice device)
        {
            var details = new SwapChainSupportDetails();
            _vkSurface.GetPhysicalDeviceSurfaceCapabilities(device, _surface, out var surfaceCapabilities);
            details.Capabilities = surfaceCapabilities;

            var formatCount = 0u;
            _vkSurface.GetPhysicalDeviceSurfaceFormats(device, _surface, &formatCount, null);

            if (formatCount != 0)
            {
                details.Formats = new SurfaceFormatKHR[formatCount];
                var formats = stackalloc SurfaceFormatKHR[(int)formatCount];
                _vkSurface.GetPhysicalDeviceSurfaceFormats(device, _surface, &formatCount, formats);

                for (var i = 0; i < formatCount; i++)
                {
                    details.Formats[i] = formats[i];
                }
            }

            var presentModeCount = 0u;
            _vkSurface.GetPhysicalDeviceSurfacePresentModes(device, _surface, &presentModeCount, null);

            if (presentModeCount != 0)
            {
                details.PresentModes = new PresentModeKHR[presentModeCount];
                var modes = stackalloc PresentModeKHR[(int)presentModeCount];
                _vkSurface.GetPhysicalDeviceSurfacePresentModes(device, _surface, &presentModeCount, modes);

                for (var i = 0; i < formatCount; i++)
                {
                    details.PresentModes[i] = modes[i];
                }
            }

            return details;
        }

        private bool CheckDeviceExtensionSupport(PhysicalDevice device)
        {
            uint extensionCount;
            _vk.EnumerateDeviceExtensionProperties(device, (byte*)null, &extensionCount, null);

            var availableExtensions = stackalloc ExtensionProperties[(int)extensionCount];
            _vk.EnumerateDeviceExtensionProperties(device, (byte*)null, &extensionCount, availableExtensions);

            var requiredExtensions = new List<string>();
            requiredExtensions.AddRange(DeviceExtensions);

            for (var i = 0u; i < extensionCount; i++)
            {
                requiredExtensions.Remove(Marshal.PtrToStringAnsi((IntPtr)availableExtensions[i].ExtensionName));
            }

            return requiredExtensions.Count == 0;
        }

        private QueueFamilyIndices FindQueueFamilies(PhysicalDevice device)
        {
            var indices = new QueueFamilyIndices();

            uint queryFamilyCount = 0;
            _vk.GetPhysicalDeviceQueueFamilyProperties(device, &queryFamilyCount, null);

            var queueFamilies = stackalloc QueueFamilyProperties[(int)queryFamilyCount];

            _vk.GetPhysicalDeviceQueueFamilyProperties(device, &queryFamilyCount, queueFamilies);
            for (var i = 0u; i < queryFamilyCount; i++)
            {
                var queueFamily = queueFamilies[i];

                // Support basic graphics operations
                if (queueFamily.QueueFlags.HasFlag(QueueFlags.QueueGraphicsBit))
                {
                    indices.GraphicsFamily = i;
                }

                _vkSurface.GetPhysicalDeviceSurfaceSupport(device, i, _surface, out var presentSupport);
                // Support Image presentation aka render to an image/screen
                if (presentSupport == Vk.True)
                {
                    indices.PresentFamily = i;
                }

                if (indices.IsComplete())
                {
                    break;
                }
            }

            return indices;
        }

        private void CreateLogicalGraphicsDevice()
        {
            var indices = FindQueueFamilies(_physicalDevice);
            var uniqueQueueFamilies = new[] { indices.GraphicsFamily.Value, indices.PresentFamily.Value };
            var queueCreateInfos = stackalloc DeviceQueueCreateInfo[uniqueQueueFamilies.Length];

            var queuePriority = 1f;
            for (var i = 0; i < uniqueQueueFamilies.Length; i++)
            {
                var queueCreateInfo = new DeviceQueueCreateInfo
                {
                    SType = StructureType.DeviceQueueCreateInfo,
                    QueueFamilyIndex = uniqueQueueFamilies[i],
                    QueueCount = 1,
                    PQueuePriorities = &queuePriority
                };
                queueCreateInfos[i] = queueCreateInfo;
            }

            var deviceFeatures = new PhysicalDeviceFeatures();

            var createInfo = new DeviceCreateInfo
            {
                SType = StructureType.DeviceCreateInfo,
                QueueCreateInfoCount = (uint) uniqueQueueFamilies.Length,
                PQueueCreateInfos = queueCreateInfos,
                PEnabledFeatures = &deviceFeatures,
                EnabledExtensionCount = (uint) DeviceExtensions.Length
            };

            var enabledExtensionNames = SilkMarshal.MarshalStringArrayToPtr(DeviceExtensions);
            createInfo.PpEnabledExtensionNames = (byte**)enabledExtensionNames;

            if (EnableValidationLayers)
            {
                createInfo.EnabledLayerCount = (uint)ValidationLayers.Length;
                createInfo.PpEnabledLayerNames = (byte**)SilkMarshal.MarshalStringArrayToPtr(ValidationLayers);
            }
            else
            {
                createInfo.EnabledLayerCount = 0;
            }

            fixed (Device* device = &_device)
            {
                if (_vk.CreateDevice(_physicalDevice, &createInfo, null, device) != Result.Success)
                {
                    throw new Exception("Failed to create logical device.");
                }
            }

            fixed (Queue* graphicsQueue = &_graphicsQueue)
            {
                _vk.GetDeviceQueue(_device, indices.GraphicsFamily.Value, 0, graphicsQueue);
            }

            fixed (Queue* presentQueue = &_presentQueue)
            {
                _vk.GetDeviceQueue(_device, indices.PresentFamily.Value, 0, presentQueue);
            }

            _vk.CurrentDevice = _device;
        }

        private void CreateSwapChain()
        {
            var swapChainSupport = QuerySwapChainSupport(_physicalDevice);

            var surfaceFormat = ChooseSwapSurfaceFormat(swapChainSupport.Formats);
            var presentMode = ChooseSwapPresentMode(swapChainSupport.PresentModes);
            var extent = ChooseSwapExtent(swapChainSupport.Capabilities);

            var imageCount = swapChainSupport.Capabilities.MinImageCount + 1;
            // Cap the maximum image count if needed
            if (swapChainSupport.Capabilities.MaxImageCount > 0 && imageCount > swapChainSupport.Capabilities.MaxImageCount)
            {
                imageCount = swapChainSupport.Capabilities.MaxImageCount;
            }

            var createInfo = new SwapchainCreateInfoKHR
            {
                SType = StructureType.SwapchainCreateInfoKhr,
                Surface = _surface,
                MinImageCount = imageCount,
                ImageFormat = surfaceFormat.Format,
                ImageColorSpace = surfaceFormat.ColorSpace,
                ImageExtent = extent,
                ImageArrayLayers = 1,
                ImageUsage = ImageUsageFlags.ImageUsageColorAttachmentBit
            };

            var indices = FindQueueFamilies(_physicalDevice);
            uint[] queueFamilyIndices = { indices.GraphicsFamily.Value, indices.PresentFamily.Value };

            fixed (uint* qfiPtr = queueFamilyIndices)
            {
                if (indices.GraphicsFamily != indices.PresentFamily)
                {
                    createInfo.ImageSharingMode = SharingMode.Concurrent;
                    createInfo.QueueFamilyIndexCount = 2;
                    createInfo.PQueueFamilyIndices = qfiPtr;
                }
                else
                {
                    createInfo.ImageSharingMode = SharingMode.Exclusive;
                }

                createInfo.PreTransform = swapChainSupport.Capabilities.CurrentTransform;
                createInfo.CompositeAlpha = CompositeAlphaFlagsKHR.CompositeAlphaOpaqueBitKhr;
                createInfo.PresentMode = presentMode;
                createInfo.Clipped = Vk.True;

                createInfo.OldSwapchain = default;

                fixed (SwapchainKHR* swapchain = &_swapchain)
                {
                    if (_vkSwapchain.CreateSwapchain(_device, &createInfo, null, swapchain) != Result.Success)
                    {
                        throw new Exception("Failed to create swap chain!");
                    }
                }
            }

            _vkSwapchain.GetSwapchainImages(_device, _swapchain, &imageCount, null);
            _swapchainImages = new Image[imageCount];
            fixed (Image* swapchainImage = _swapchainImages)
            {
                _vkSwapchain.GetSwapchainImages(_device, _swapchain, &imageCount, swapchainImage);
            }

            _swapchainImageFormat = surfaceFormat.Format;
            _swapchainExtent = extent;
        }

        private Extent2D ChooseSwapExtent(SurfaceCapabilitiesKHR capabilities)
        {
            if (capabilities.CurrentExtent.Width != uint.MaxValue)
            {
                return capabilities.CurrentExtent;
            }

            var actualExtent = new Extent2D
            { Height = (uint)Window.Size.Y, Width = (uint)Window.Size.X };
            actualExtent.Width = new[]
            {
                capabilities.MinImageExtent.Width,
                new[] {capabilities.MaxImageExtent.Width, actualExtent.Width}.Min()
            }.Max();
            actualExtent.Height = new[]
            {
                capabilities.MinImageExtent.Height,
                new[] {capabilities.MaxImageExtent.Height, actualExtent.Height}.Min()
            }.Max();

            return actualExtent;
        }

        private PresentModeKHR ChooseSwapPresentMode(PresentModeKHR[] presentModes)
        {
            foreach (var availablePresentMode in presentModes)
            {
                if (availablePresentMode == PresentModeKHR.PresentModeMailboxKhr)
                {
                    return availablePresentMode;
                }
            }

            return PresentModeKHR.PresentModeImmediateKhr;
        }

        private SurfaceFormatKHR ChooseSwapSurfaceFormat(SurfaceFormatKHR[] formats)
        {
            foreach (var format in formats)
            {
                if (format.Format == Format.B8G8R8A8Srgb)
                {
                    return format;
                }
            }

            return formats[0];
        }

        private void CreateImageViews()
        {
            _swapchainImageViews = new ImageView[_swapchainImages.Length];

            for (var i = 0; i < _swapchainImages.Length; i++)
            {
                var createInfo = new ImageViewCreateInfo
                {
                    SType = StructureType.ImageViewCreateInfo,
                    Image = _swapchainImages[i],
                    ViewType = ImageViewType.ImageViewType2D,
                    Format = _swapchainImageFormat,
                    Components =
                    {
                        R = ComponentSwizzle.Identity,
                        G = ComponentSwizzle.Identity,
                        B = ComponentSwizzle.Identity,
                        A = ComponentSwizzle.Identity
                    },
                    SubresourceRange =
                    {
                        AspectMask = ImageAspectFlags.ImageAspectColorBit,
                        BaseMipLevel = 0,
                        LevelCount = 1,
                        BaseArrayLayer = 0,
                        LayerCount = 1
                    }
                };

                ImageView imageView = default;
                if (_vk.CreateImageView(_device, &createInfo, null, &imageView) != Result.Success)
                {
                    throw new Exception("Failed to create image views!");
                }

                _swapchainImageViews[i] = imageView;
            }
        }

        private void CreateRenderPass()
        {
            var colorAttachment = new AttachmentDescription
            {
                Format = _swapchainImageFormat,
                Samples = SampleCountFlags.SampleCount1Bit,
                LoadOp = AttachmentLoadOp.Clear,
                StoreOp = AttachmentStoreOp.Store,
                StencilLoadOp = AttachmentLoadOp.DontCare,
                StencilStoreOp = AttachmentStoreOp.DontCare,
                InitialLayout = ImageLayout.Undefined,
                FinalLayout = ImageLayout.PresentSrcKhr
            };

            var colorAttachmentRef = new AttachmentReference
            {
                Attachment = 0,
                Layout = ImageLayout.ColorAttachmentOptimal
            };

            var subpass = new SubpassDescription
            {
                PipelineBindPoint = PipelineBindPoint.Graphics,
                ColorAttachmentCount = 1,
                PColorAttachments = &colorAttachmentRef
            };

            var dependency = new SubpassDependency
            {
                SrcSubpass = Vk.SubpassExternal,
                DstSubpass = 0,
                SrcStageMask = PipelineStageFlags.PipelineStageColorAttachmentOutputBit,
                SrcAccessMask = 0,
                DstStageMask = PipelineStageFlags.PipelineStageColorAttachmentOutputBit,
                DstAccessMask = AccessFlags.AccessColorAttachmentReadBit | AccessFlags.AccessColorAttachmentWriteBit
            };

            var renderPassInfo = new RenderPassCreateInfo
            {
                SType = StructureType.RenderPassCreateInfo,
                AttachmentCount = 1,
                PAttachments = &colorAttachment,
                SubpassCount = 1,
                PSubpasses = &subpass,
                DependencyCount = 1,
                PDependencies = &dependency
            };

            fixed (RenderPass* renderPass = &_renderPass)
            {
                if (_vk.CreateRenderPass(_device, &renderPassInfo, null, renderPass) != Result.Success)
                {
                    throw new Exception("Failed to create render pass!");
                }
            }
        }

        private byte[] LoadShader(string shaderName)
        {
            return File.ReadAllBytes(shaderName);
        }

        private void CreateGraphicsPipeline()
        {
            var vertShaderCode = LoadShader("shader.vert.spv");
            var fragShaderCode = LoadShader("shader.frag.spv");

            var vertShaderModule = CreateShaderModule(vertShaderCode);
            var fragShaderModule = CreateShaderModule(fragShaderCode);

            var vertShaderStageInfo = new PipelineShaderStageCreateInfo
            {
                SType = StructureType.PipelineShaderStageCreateInfo,
                Stage = ShaderStageFlags.ShaderStageVertexBit,
                Module = vertShaderModule,
                PName = (byte*)SilkMarshal.MarshalStringToPtr("main")
            };

            var fragShaderStageInfo = new PipelineShaderStageCreateInfo
            {
                SType = StructureType.PipelineShaderStageCreateInfo,
                Stage = ShaderStageFlags.ShaderStageFragmentBit,
                Module = fragShaderModule,
                PName = (byte*)SilkMarshal.MarshalStringToPtr("main")
            };

            var shaderStages = stackalloc PipelineShaderStageCreateInfo[2];
            shaderStages[0] = vertShaderStageInfo;
            shaderStages[1] = fragShaderStageInfo;

            var vertexInputInfo = new PipelineVertexInputStateCreateInfo
            {
                SType = StructureType.PipelineVertexInputStateCreateInfo,
                VertexBindingDescriptionCount = 0,
                VertexAttributeDescriptionCount = 0
            };

            var inputAssembly = new PipelineInputAssemblyStateCreateInfo
            {
                SType = StructureType.PipelineInputAssemblyStateCreateInfo,
                Topology = PrimitiveTopology.TriangleList,
                PrimitiveRestartEnable = Vk.False
            };

            var viewport = new Viewport
            {
                X = 0.0f,
                Y = 0.0f,
                Width = _swapchainExtent.Width,
                Height = _swapchainExtent.Height,
                MinDepth = 0.0f,
                MaxDepth = 1.0f
            };

            var scissor = new Rect2D { Offset = default, Extent = _swapchainExtent };

            var viewportState = new PipelineViewportStateCreateInfo
            {
                SType = StructureType.PipelineViewportStateCreateInfo,
                ViewportCount = 1,
                PViewports = &viewport,
                ScissorCount = 1,
                PScissors = &scissor
            };

            var rasterizer = new PipelineRasterizationStateCreateInfo
            {
                SType = StructureType.PipelineRasterizationStateCreateInfo,
                DepthClampEnable = Vk.False,
                RasterizerDiscardEnable = Vk.False,
                PolygonMode = PolygonMode.Fill,
                LineWidth = 1.0f,
                CullMode = CullModeFlags.CullModeBackBit,
                FrontFace = FrontFace.Clockwise,
                DepthBiasEnable = Vk.False
            };

            var multisampling = new PipelineMultisampleStateCreateInfo
            {
                SType = StructureType.PipelineMultisampleStateCreateInfo,
                SampleShadingEnable = Vk.False,
                RasterizationSamples = SampleCountFlags.SampleCount1Bit
            };

            var colorBlendAttachment = new PipelineColorBlendAttachmentState
            {
                ColorWriteMask = ColorComponentFlags.ColorComponentRBit |
                                 ColorComponentFlags.ColorComponentGBit |
                                 ColorComponentFlags.ColorComponentBBit |
                                 ColorComponentFlags.ColorComponentABit,
                BlendEnable = Vk.False
            };

            var colorBlending = new PipelineColorBlendStateCreateInfo
            {
                SType = StructureType.PipelineColorBlendStateCreateInfo,
                LogicOpEnable = Vk.False,
                LogicOp = LogicOp.Copy,
                AttachmentCount = 1,
                PAttachments = &colorBlendAttachment
            };

            colorBlending.BlendConstants[0] = 0.0f;
            colorBlending.BlendConstants[1] = 0.0f;
            colorBlending.BlendConstants[2] = 0.0f;
            colorBlending.BlendConstants[3] = 0.0f;

            var pipelineLayoutInfo = new PipelineLayoutCreateInfo
            {
                SType = StructureType.PipelineLayoutCreateInfo,
                SetLayoutCount = 0,
                PushConstantRangeCount = 0
            };

            fixed (PipelineLayout* pipelineLayout = &_pipelineLayout)
            {
                if (_vk.CreatePipelineLayout(_device, &pipelineLayoutInfo, null, pipelineLayout) != Result.Success)
                {
                    throw new Exception("Failed to create pipeline layout!");
                }
            }

            var pipelineInfo = new GraphicsPipelineCreateInfo
            {
                SType = StructureType.GraphicsPipelineCreateInfo,
                StageCount = 2,
                PStages = shaderStages,
                PVertexInputState = &vertexInputInfo,
                PInputAssemblyState = &inputAssembly,
                PViewportState = &viewportState,
                PRasterizationState = &rasterizer,
                PMultisampleState = &multisampling,
                PColorBlendState = &colorBlending,
                Layout = _pipelineLayout,
                RenderPass = _renderPass,
                Subpass = 0,
                BasePipelineHandle = default
            };

            fixed (Pipeline* graphicsPipeline = &_graphicsPipeline)
            {
                if (_vk.CreateGraphicsPipelines
                        (_device, default, 1, &pipelineInfo, null, graphicsPipeline) != Result.Success)
                {
                    throw new Exception("Failed to create graphics pipeline!");
                }
            }

            _vk.DestroyShaderModule(_device, fragShaderModule, null);
            _vk.DestroyShaderModule(_device, vertShaderModule, null);
        }

        private ShaderModule CreateShaderModule(byte[] code)
        {
            var createInfo = new ShaderModuleCreateInfo
            {
                SType = StructureType.ShaderModuleCreateInfo,
                CodeSize = new UIntPtr((uint)code.Length)
            };
            fixed (byte* codePtr = code)
            {
                createInfo.PCode = (uint*)codePtr;
            }

            var shaderModule = new ShaderModule();
            if (_vk.CreateShaderModule(_device, &createInfo, null, &shaderModule) != Result.Success)
            {
                throw new Exception("Failed to create shader module!");
            }

            return shaderModule;
        }

        private void CreateFrameBuffers()
        {
            _swapchainFramebuffers = new Framebuffer[_swapchainImageViews.Length];

            for (var i = 0; i < _swapchainImageViews.Length; i++)
            {
                var attachment = _swapchainImageViews[i];
                var framebufferInfo = new FramebufferCreateInfo
                {
                    SType = StructureType.FramebufferCreateInfo,
                    RenderPass = _renderPass,
                    AttachmentCount = 1,
                    PAttachments = &attachment,
                    Width = _swapchainExtent.Width,
                    Height = _swapchainExtent.Height,
                    Layers = 1
                };

                var framebuffer = new Framebuffer();
                if (_vk.CreateFramebuffer(_device, &framebufferInfo, null, &framebuffer) != Result.Success)
                {
                    throw new Exception("Failed to create framebuffer!");
                }

                _swapchainFramebuffers[i] = framebuffer;
            }
        }

        private void CreateCommandPool()
        {
            var queueFamilyIndices = FindQueueFamilies(_physicalDevice);

            var poolInfo = new CommandPoolCreateInfo
            {
                SType = StructureType.CommandPoolCreateInfo,
                QueueFamilyIndex = queueFamilyIndices.GraphicsFamily.Value
            };

            fixed (CommandPool* commandPool = &_commandPool)
            {
                if (_vk.CreateCommandPool(_device, &poolInfo, null, commandPool) != Result.Success)
                {
                    throw new Exception("Failed to create command pool!");
                }
            }
        }

        private void CreateCommandBuffers()
        {
            _commandBuffers = new CommandBuffer[_swapchainFramebuffers.Length];

            var allocInfo = new CommandBufferAllocateInfo
            {
                SType = StructureType.CommandBufferAllocateInfo,
                CommandPool = _commandPool,
                Level = CommandBufferLevel.Primary,
                CommandBufferCount = (uint)_commandBuffers.Length
            };

            fixed (CommandBuffer* commandBuffers = _commandBuffers)
            {
                if (_vk.AllocateCommandBuffers(_device, &allocInfo, commandBuffers) != Result.Success)
                {
                    throw new Exception("Failed to allocate command buffers!");
                }
            }

            for (var i = 0; i < _commandBuffers.Length; i++)
            {
                var beginInfo = new CommandBufferBeginInfo { SType = StructureType.CommandBufferBeginInfo };

                if (_vk.BeginCommandBuffer(_commandBuffers[i], &beginInfo) != Result.Success)
                {
                    throw new Exception("Failed to begin recording command buffer!");
                }

                var renderPassInfo = new RenderPassBeginInfo
                {
                    SType = StructureType.RenderPassBeginInfo,
                    RenderPass = _renderPass,
                    Framebuffer = _swapchainFramebuffers[i],
                    RenderArea = { Offset = new Offset2D { X = 0, Y = 0 }, Extent = _swapchainExtent }
                };

                var clearColor = new ClearValue
                { Color = new ClearColorValue { Float32_0 = 0, Float32_1 = 0, Float32_2 = 0, Float32_3 = 1 } };
                renderPassInfo.ClearValueCount = 1;
                renderPassInfo.PClearValues = &clearColor;

                _vk.CmdBeginRenderPass(_commandBuffers[i], &renderPassInfo, SubpassContents.Inline);

                _vk.CmdBindPipeline(_commandBuffers[i], PipelineBindPoint.Graphics, _graphicsPipeline);

                _vk.CmdDraw(_commandBuffers[i], 3, 1, 0, 0);

                _vk.CmdEndRenderPass(_commandBuffers[i]);

                if (_vk.EndCommandBuffer(_commandBuffers[i]) != Result.Success)
                {
                    throw new Exception("Failed to record command buffer!");
                }
            }
        }

        private void CreateSyncObjects()
        {
            _imageAvailableSemaphores = new Semaphore[MaxFramesInFlight];
            _renderFinishedSemaphores = new Semaphore[MaxFramesInFlight];
            _inFlightFences = new Fence[MaxFramesInFlight];
            _imagesInFlight = new Fence[MaxFramesInFlight];

            SemaphoreCreateInfo semaphoreInfo = new SemaphoreCreateInfo();
            semaphoreInfo.SType = StructureType.SemaphoreCreateInfo;

            FenceCreateInfo fenceInfo = new FenceCreateInfo();
            fenceInfo.SType = StructureType.FenceCreateInfo;
            fenceInfo.Flags = FenceCreateFlags.FenceCreateSignaledBit;

            for (var i = 0; i < MaxFramesInFlight; i++)
            {
                Semaphore imgAvSema, renderFinSema;
                Fence inFlightFence;
                if (_vk.CreateSemaphore(_device, &semaphoreInfo, null, &imgAvSema) != Result.Success ||
                    _vk.CreateSemaphore(_device, &semaphoreInfo, null, &renderFinSema) != Result.Success ||
                    _vk.CreateFence(_device, &fenceInfo, null, &inFlightFence) != Result.Success)
                {
                    throw new Exception("Failed to create synchronization objects for a frame!");
                }

                _imageAvailableSemaphores[i] = imgAvSema;
                _renderFinishedSemaphores[i] = renderFinSema;
                _inFlightFences[i] = inFlightFence;
            }
        }

        private bool CheckValidationLayerSupport()
        {
            uint layerCount = 0;
            _vk.EnumerateInstanceLayerProperties(&layerCount, (LayerProperties*)0);

            var availableLayers = new LayerProperties[layerCount];
            fixed (LayerProperties* availableLayersPtr = availableLayers)
                _vk.EnumerateInstanceLayerProperties(&layerCount, availableLayersPtr);

            foreach (var layerName in ValidationLayers)
            {
                var layerFound = false;

                foreach (var layerProperties in availableLayers)
                {
                    if (layerName == Marshal.PtrToStringAnsi((IntPtr)layerProperties.LayerName))
                    {
                        layerFound = true;
                        break;
                    }
                }

                if (!layerFound)
                {
                    return false;
                }
            }

            return true;
        }
    }
}