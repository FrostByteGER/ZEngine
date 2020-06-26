using System.Drawing;
using Silk.NET.Input;
using Silk.NET.Input.Common;
using Silk.NET.OpenGL;
using Silk.NET.Windowing.Common;

namespace AssetForge
{
    public class MainEditorWindow
    {
        private ImGuiController Controller { get; set; }
        private GL Gl { get; set; }
        private IInputContext InputContext { get; set; }
        private IWindow Window { get; set; }

        public void Initialize()
        {
            Window = Silk.NET.Windowing.Window.Create(WindowOptions.Default);
            Window.Load += WindowOnLoad;
            Window.Resize += WindowOnResize;
            Window.Render += WindowOnRender;
            Window.Closing += WindowOnClosing;
        }

        public void Start()
        {
            Window.Run();
        }

        private void WindowOnClosing()
        {
            Controller?.Dispose();
            InputContext?.Dispose();
            Gl?.Dispose();
        }

        private void WindowOnRender(double deltaTime)
        {
            Controller.Update((float) deltaTime);
            Gl.ClearColor(Color.FromArgb(255,0 ,32, 40));
            Gl.Clear((uint) (ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit));

            ImGuiNET.ImGui.ShowDemoWindow();

            Controller.Render();
        }

        private void WindowOnResize(Size s)
        {
            Gl.Viewport(s);
        }

        private void WindowOnLoad()
        {
            Gl = Window.CreateOpenGL();
            InputContext = Window.CreateInput();
            Controller = new ImGuiController(Gl, Window, InputContext);
        }
    }
}