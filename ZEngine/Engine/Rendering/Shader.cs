using ZEngine.Engine.IO.Assets;

namespace ZEngine.Engine.Rendering
{
    public enum ShaderType
    {
        SPIRV,
        GLSL,
        HLSL
    }

    public class ShaderMetaData : AssetMetaData
    {
        public ShaderType Type { get; }
    }

    public class Shader<T> : Asset<ShaderMetaData> where T : ShaderData
    {
        public T ShaderData { get; private set; }
    }
}