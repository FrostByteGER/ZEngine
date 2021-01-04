using Silk.NET.Windowing;

namespace ZEngine.Engine.Rendering.RHI
{
    public abstract class AbstractRenderHardwareInterface
    {
        protected IWindow Window { get; set; }

        protected AbstractRenderHardwareInterface(IWindow window)
        {
            Window = window;
        }

        public abstract void Initialize();
        public abstract void Deinitialize();
        public abstract void DrawFrame(double deltaTime);
    }
}