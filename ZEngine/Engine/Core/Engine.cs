using System.Drawing;
using Silk.NET.Windowing.Common;
using ZEngine.Engine.Core.Messages;
using ZEngine.Engine.Game;
using ZEngine.Engine.Game.Level;
using ZEngine.Engine.IO.Assets;
using ZEngine.Engine.Messaging;
using ZEngine.Engine.Services;
using ZEngine.Engine.Services.Locator;
using ZEngine.Engine.Services.Provider;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Core
{

	public class Engine : IEngineServiceProvider
    {

		public static Engine Instance { get; } = new Engine();

        internal IWindow Window { get; private set; }

        // Frame and Physics
		public IClock EngineCoreClock;


        // Core Engine
        public EngineServiceLocator EngineServiceLocator { get; } = new EngineServiceLocator();
		public Bootstrap Bootstrapper { get; set; }
	    public GameInstance GameInstance { get; set; } = new GameInstance();
		public GameInfo GameInfo { get; set; } = new GameInfo();

        // Engine Managers
        private IAssetManager AssetManager { get; set; }
        private ILevelManager LevelManager { get; set; }
        private IEngineMessageBus MessageBus { get; set; }

        // Engine Settings
        public int EngineWindowHeight { get; set; }        = 800;
	    public int EngineWindowWidth { get; set; }         = 600;
        public bool EngineInitialized { get; private set; }
		public bool PauseEngineOnWindowFocusLose { get; set; }


        private Engine()
	    {
		    
	    }

        private void ParseCommandLineArguments(string[] args)
        {
            var parserResult = CommandLine.Parser.Default.ParseArguments<Engine>(args);
        }

        public void StartEngine(string[] args)
        {
            Debug.Log("Initializing Engine!", DebugLogCategories.Engine);
            StartEngineInternal();
        }

        private void StartEngineInternal()
        {
            // Bootstrap before everything else so we have the log and all other services initialized!
            Bootstrapper.SetupInternal(EngineServiceLocator);

            EngineCoreClock = GetService<IEngineClock>();

            var settings = new WindowOptions(true, true, new Point(50, 50), new Size(1280, 720), 0, 0,
                GraphicsAPI.Default, GameInfo.GenerateFullGameName(), WindowState.Normal, WindowBorder.Resizable, VSyncMode.Off, 5, true,
                VideoMode.Default);
            Window = Silk.NET.Windowing.Window.Create(settings);
            Window.Load += OnEngineWindowLoad;
            Window.Closing += OnEngineWindowClose;
            Window.Resize += OnEngineWindowResized;
            Window.Render += WindowOnRender;
            Window.Update += WindowOnUpdate;
            Window.FocusChanged += WindowOnFocusChanged;

            AssetManager = GetService<IAssetManager>();
            AssetManager.Init();
            LevelManager = GetService<ILevelManager>();
            MessageBus = GetService<IEngineMessageBus>();
            /*
			AssetManager.TextureFolderName = GameInfo.GameTextureFolderName;
			AssetManager.SoundFolderName = GameInfo.GameSoundFolderName;
			AssetManager.ConfigFolderName = GameInfo.GameConfigFolderName;
			AssetManager.FontFolderName = GameInfo.GameFontFolderName;
			AssetManager.ShaderFolderName = GameInfo.GameShaderFolderName;
			AssetManager.LevelFolderName = GameInfo.GameLevelFolderName;
			AssetManager.Initialize();
			*/
            EngineInitialized = true;

            // Finally initialize the window
            Window.Run();
        }

        private void ShutdownEngine()
        {
            Debug.Log("Shutting down Engine!");

            //Level.CollisionCircle.Dispose();
            //Level.CollisionRectangle.Dispose();
            MessageBus.Publish(new EngineShutdownMessage(this));
        }

        private void OnEngineWindowLoad()
        {
            MessageBus.Publish(new EngineWindowLoadedMessage(this));
            //LevelManager.LoadLevel("Default");
        }

        private void WindowOnUpdate(double deltaTime)
        {
            var dt = (float) deltaTime;
            // Tick Physics
            /*
            if (ActiveLevel.PhysicsWorld.CanTick)
            {
                EngineCoreClock.StartPhysics();
                ActiveLevel.PhysicsWorld?.PhysicsTick(FrameDelta);
                EngineCoreClock.StopPhysics();
            }
            */

            //if (InputManager.CanTick) 
            //    InputManager.Tick(FrameDelta);
            if (LevelManager.CanTick)
                LevelManager.Tick(dt);

            Debug.FlushQueue();
		}

        private void WindowOnRender(double deltaTime)
        {
			//ActiveLevel.LevelDraw(ref _engineWindow);
		}

        private void WindowOnFocusChanged(bool state)
        {
            if (!PauseEngineOnWindowFocusLose)
                return;

            MessageBus.Publish(new EngineFocusChangeMessage(this, state));
        }

        private void OnEngineWindowResized(Size s)
		{
			
            EngineWindowWidth = s.Width;
			EngineWindowHeight = s.Height;
			//foreach (var p in ActiveLevel.Players)
			//{
				//p.PlayerCamera.Center = new Vector2();
				//p.PlayerCamera.Size = new Vector2(s.Width, s.Height);
			//}
			//EngineWindow.SetView(new View(new Vector2(s.Width/2f, s.Height/2f), new Vector2(s.Width, s.Height)));
        }

        private void OnEngineWindowClose()
        {
            ShutdownEngine();
		}

        // TODO: Add Compiled Lambda Dictionary 
	    public static void ConstructActor()
	    {
		    
	    }

        public T GetService<T>(string id = null) where T : IEngineService
        {
            return EngineServiceLocator.GetService<T>(id);
        }
    }

    internal class EngineFocusChangeMessage : AbstractMessage
    {
        public bool NewFocusState { get; private set; }
        public EngineFocusChangeMessage(object sender, bool newState) : base(sender)
        {
            NewFocusState = newState;
        }
    }
}
