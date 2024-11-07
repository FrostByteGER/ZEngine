using Silk.NET.Maths;
using ZEngine.Engine.Core.Messages;
using ZEngine.Engine.Game;
using ZEngine.Engine.Game.Level;
using ZEngine.Engine.IO.Assets;
using ZEngine.Engine.Messaging;
using ZEngine.Engine.Rendering.Window;
using ZEngine.Engine.Services;
using ZEngine.Engine.Services.Locator;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Core
{

	public class Engine
    {

		public static Engine Instance { get; } = new();

        // Frame and Physics
		public IClock CoreClock;
		
        // Core Engine
		public Bootstrap Bootstrapper { get; set; }
	    public GameInstance GameInstance { get; set; } = new();
		public GameInfo GameInfo { get; set; } = new();

        // Engine Managers
        private IAssetManager AssetManager { get; set; }
        private ILevelManager_OLD LevelManagerOld { get; set; }
        private IWindowManager WindowManager { get; set; }
        private IMessageBus MessageBus { get; set; }

        // Engine Settings
        public int WindowHeight { get; set; }        = 800;
	    public int WindowWidth { get; set; }         = 600;
        public bool Initialized { get; private set; }
		public bool PauseEngineOnWindowFocusLose { get; set; }


        private Engine()
	    {
		    
	    }

        private void ParseCommandLineArguments(string[] args)
        {
            //var parserResult = CommandLine.Parser.Default.ParseArguments<Engine>(args);
        }

        public void StartEngine(string[] args)
        {
            Debug.Log("Initializing Engine!", DebugLogCategories.Engine);
            ParseCommandLineArguments(args);
            StartEngineInternal();
        }

        private void StartEngineInternal()
        {
            // Bootstrap before everything else so we have the log and all other services initialized!
            Bootstrapper.SetupInternal();
            CoreClock = EngineServiceLocator.GetService<IClock>();

            AssetManager = EngineServiceLocator.GetService<IAssetManager>();
            AssetManager.Init();
            LevelManagerOld = EngineServiceLocator.GetService<ILevelManager_OLD>();
            WindowManager = EngineServiceLocator.GetService<IWindowManager>();
            MessageBus = EngineServiceLocator.GetService<IMessageBus>();
            WindowManager.InitWindow();
            WindowManager.Window.Load += OnEngineWindowLoad;
            WindowManager.Window.Closing += OnEngineWindowClose;
            WindowManager.Window.Resize += OnEngineWindowResized;
            WindowManager.Window.Render += WindowOnRender;
            WindowManager.Window.Update += WindowOnUpdate;
            WindowManager.Window.FocusChanged += WindowOnFocusChanged;
            /*
			AssetManager.TextureFolderName = GameInfo.GameTextureFolderName;
			AssetManager.SoundFolderName = GameInfo.GameSoundFolderName;
			AssetManager.ConfigFolderName = GameInfo.GameConfigFolderName;
			AssetManager.FontFolderName = GameInfo.GameFontFolderName;
			AssetManager.ShaderFolderName = GameInfo.GameShaderFolderName;
			AssetManager.LevelFolderName = GameInfo.GameLevelFolderName;
			AssetManager.Initialize();
			*/
            Initialized = true;
            WindowManager.RunWindow();
        }

        private void ShutdownEngine()
        {
            Debug.Log("Shutting down Engine!");

            MessageBus.Publish(new EngineShutdownMessage(this));
        }

        private void OnEngineWindowLoad()
        {
            MessageBus.Publish(new EngineWindowLoadedMessage(this));
        }

        private void WindowOnUpdate(double deltaTime)
        {
            var dt = (float) deltaTime;

            if (LevelManagerOld.CanTick)
                LevelManagerOld.Tick(dt);

            Debug.FlushQueue();
		}

        private void WindowOnRender(double deltaTime)
        {
            WindowManager.RHI.DrawFrame(deltaTime);
		}

        private void WindowOnFocusChanged(bool state)
        {
            if (!PauseEngineOnWindowFocusLose)
                return;

            MessageBus.Publish(new EngineFocusChangeMessage(this, state));
        }

        private void OnEngineWindowResized(Vector2D<int> s)
		{
			
            WindowWidth = s.X;
			WindowHeight = s.Y;
        }

        private void OnEngineWindowClose()
        {
            ShutdownEngine();
		}
    }
}
