using System.Drawing;
using System.Numerics;
using Silk.NET.Maths;
using ZEngine.Engine.Core.Messages;
using ZEngine.Engine.Game;
using ZEngine.Engine.Game.Level;
using ZEngine.Engine.IO.Assets;
using ZEngine.Engine.Messaging;
using ZEngine.Engine.Rendering.Window;
using ZEngine.Engine.Services;
using ZEngine.Engine.Services.Locator;
using ZEngine.Engine.Services.Provider;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Core
{

	public class Engine : IEngineServiceProvider
    {

		public static Engine Instance { get; } = new Engine();

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
        private IWindowManager WindowManager { get; set; }
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

            AssetManager = GetService<IAssetManager>();
            AssetManager.Init();
            LevelManager = GetService<ILevelManager>();
            WindowManager = GetService<IWindowManager>();
            MessageBus = GetService<IEngineMessageBus>();
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
            EngineInitialized = true;
            WindowManager.RunWindow();
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
			
            EngineWindowWidth = s.X;
			EngineWindowHeight = s.Y;
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
