using System;
using System.Windows.Forms;
using SFML.Graphics;
using SFML.System;
using WeifenLuo.WinFormsUI.Docking;
using View = SFML.Graphics.View;

namespace AssetForge
{
    public partial class LevelView : DockContent
    {
        private RenderWindow _renderWindow;
        private SFMLRenderControl _renderControl;

        private bool _formLoaded;

        public LevelView()
        {
            InitializeComponent();

            _renderControl = new SFMLRenderControl();
            keepAlivePanel.Controls.Add(_renderControl);

            _renderControl.Dock = DockStyle.Fill;

            renderLoopWorker.RunWorkerAsync(_renderControl.Handle);
        }

        private void renderLoopWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _renderWindow = new RenderWindow((IntPtr)e.Argument);
            _renderWindow.SetView(new View(new FloatRect(0,0,Size.Width,Size.Height)));
            var RS = new RectangleShape(new Vector2f(50,50))
            {
                FillColor = Color.Magenta,
                Position = new Vector2f(Size.Width / 2f, Size.Height / 2f),
                Origin = new Vector2f(25,25)
            };
            while (_renderWindow.IsOpen)
            {
                _renderWindow.DispatchEvents();
                _renderWindow.Clear(Color.Black);
                //TODO: Draw Level!
                _renderWindow.Draw(RS);
                RS.Rotation += 0.01f;
                _renderWindow.Display();
            }
        }

        protected override void DestroyHandle()
        {
            // Set the panels parent to null to make it float in limbo until the dockstate got changed.
            // This appearently keeps the internal OpenGL handle alive, instead of killing it.
            if(_formLoaded)
                keepAlivePanel.Parent = null;
            base.DestroyHandle();
        }

        private void LevelView_Load(object sender, EventArgs e)
        {
            _formLoaded = true;
        }

        private void LevelView_DockStateChanged(object sender, EventArgs e)
        {
            // Okay now that the new handle got created(more like pulled out of limbo), reset the panel's parent so our OpenGL handle still lives.
            // No idea how and why it works. Source: https://sourceforge.net/p/dockpanelsuite/discussion/402316/thread/73ed4119/
            if (IsHandleCreated && _formLoaded)
                keepAlivePanel.Parent = FindForm();
        }

        private void LevelView_ResizeEnd(object sender, EventArgs e)
        {
            _renderWindow.SetView(new View(new FloatRect(0, 0, Size.Width, Size.Height)));
        }
    }
}
