using Silk.NET.Vulkan;

namespace ZEngine.Engine.Rendering.RHI.Vulkan
{
    public class VulkanShaderData : ShaderData
    {
        public ShaderModule Shader { get; private set; }
    }
}