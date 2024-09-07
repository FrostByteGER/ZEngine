using Silk.NET.Maths;
using Silk.NET.Windowing;
using ZEngine.Engine.Rendering.RHI;
using ZEngine.Engine.Rendering.RHI.Vulkan;
using ZEngine.Engine.Services;

namespace ZEngine.Engine.Rendering.Window
{
    public interface IWindowManager : IEngineService
    {
        IWindow Window { get; }
        AbstractRenderHardwareInterface RHI { get; }

        void InitWindow();
        void RunWindow();
        void DeinitWindow();
    }

    public class SilkWindowManager : IWindowManager
    {
        public IWindow Window { get; private set; }
        public AbstractRenderHardwareInterface RHI { get; private set; }

        public void InitWindow()
        {
            // TODO: Choose Graphics API here
            // TODO: Pull parameters from config
            var settings = new WindowOptions(true, new Vector2D<int>(50, 50), new Vector2D<int>(1280, 720), 0, 0,
                GraphicsAPI.DefaultVulkan, "ZEngine v0.1", WindowState.Normal, WindowBorder.Resizable, false, false, VideoMode.Default, 
                null, null, null, false, false, false );
            Window = Silk.NET.Windowing.Window.Create(WindowOptions.DefaultVulkan);
            RHI = new ExampleVkRHI(Window); //new VulkanRHI(Window);
            Window.Initialize();
            RHI.Initialize();
        }

        public void RunWindow()
        {
            //TODO: Check if initialized
            Window.Run();
        }

        public void DeinitWindow()
        {
            RHI.Deinitialize();
        }

        public void Initialize()
        {
            
        }

        public void Deinitialize()
        {
            
        }
    }
}