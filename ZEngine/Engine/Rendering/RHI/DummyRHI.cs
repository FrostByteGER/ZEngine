using Silk.NET.Windowing;

namespace ZEngine.Engine.Rendering.RHI;

public class DummyRHI : AbstractRenderHardwareInterface
{
    public DummyRHI(IWindow window) : base(window)
    {
    }

    public override void Initialize()
    {
        
    }

    public override void Deinitialize()
    {
        
    }

    public override void DrawFrame(double deltaTime)
    {
        
    }
}