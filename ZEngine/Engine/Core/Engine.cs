using System;
using System.Drawing;
using Newtonsoft.Json;
using Silk.NET.Windowing.Common;
using ZEngine.Engine.Game;
using ZEngine.Engine.IO;
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
		public EngineClock EngineCoreClock;


        // Core Engine
        public EngineServiceLocator EngineServiceLocator { get; } = new EngineServiceLocator();
		public Bootstrap Bootstrapper { get; set; }
	    public GameInstance GameInstance { get; set; } = new GameInstance();
		public GameInfo GameInfo { get; set; } = new GameInfo();
	    public Level ActiveLevel { get; internal set; }
		public uint LevelIDCounter { get; private set; } = 0;


		// Engine Managers
        public IAssetManager AssetManager { get; set; }

        // Engine Settings
        public int EngineWindowHeight { get; set; }        = 800;
	    public int EngineWindowWidth { get; set; }         = 600;
        public bool EngineInitialized { get; private set; } = false;
		private bool _pauseEngineOnWindowFocusLose          = false;
		public bool PauseEngineOnWindowFocusLose
	    {
		    get => _pauseEngineOnWindowFocusLose;
		    set
		    {
				if (!value && EngineInitialized)
				{
					//_engineWindow.LostFocus -= OnEngineWindowFocusLost;
					//_engineWindow.GainedFocus -= OnEngineWindowFocusGained;
				}
				else if (!_pauseEngineOnWindowFocusLose && EngineInitialized)
				{
					//_engineWindow.LostFocus += OnEngineWindowFocusLost;
					//_engineWindow.GainedFocus += OnEngineWindowFocusGained;
				}
				_pauseEngineOnWindowFocusLose = value;
		    }
	    }


        private Engine()
	    {
		    
	    }

        public void InitEngine()
        {
            var settings = new WindowOptions(true, true, new Point(50, 50), new Size(1280, 720), 0, 0,
                GraphicsAPI.Default, GameInfo.GenerateFullGameName(), WindowState.Normal, WindowBorder.Resizable, VSyncMode.Off, 5, true,
                VideoMode.Default);
            Window = Silk.NET.Windowing.Window.Create(settings);
			Window.Load += OnEngineWindowLoad;
            Window.Closing += OnEngineWindowClose;
            Window.Resize += OnEngineWindowResized;
			Window.Render += WindowOnRender;
			Window.Update += WindowOnUpdate;

            // Bootstrap before everything else so we have the log and all other services initialized!
            Bootstrapper.SetupInternal(EngineServiceLocator);

            EngineCoreClock = new EngineClock();
	        AssetManager = GetService<IAssetManager>();
			/*
			AssetManager.TextureFolderName = GameInfo.GameTextureFolderName;
			AssetManager.SoundFolderName = GameInfo.GameSoundFolderName;
			AssetManager.ConfigFolderName = GameInfo.GameConfigFolderName;
			AssetManager.FontFolderName = GameInfo.GameFontFolderName;
			AssetManager.ShaderFolderName = GameInfo.GameShaderFolderName;
			AssetManager.LevelFolderName = GameInfo.GameLevelFolderName;
			AssetManager.InitPackages();
			*/
			EngineInitialized = true;
        }

        private void OnEngineWindowLoad()
        {
            GetService<IEngineMessageBus>().Publish(new EngineWindowLoadedMessage(this));
        }

        private void WindowOnUpdate(double deltaTime)
        {

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
			if (ActiveLevel.LevelTicking)
                ActiveLevel.LevelTick((float) deltaTime);

            Debug.FlushQueue();
		}

        private void WindowOnRender(double deltaTime)
        {
			//ActiveLevel.LevelDraw(ref _engineWindow);
		}

		private void OnEngineWindowResized(Size s)
		{
			
            EngineWindowWidth = s.Width;
			EngineWindowHeight = s.Height;
			foreach (var p in ActiveLevel.Players)
			{
				//p.PlayerCamera.Center = new Vector2();
				//p.PlayerCamera.Size = new Vector2(s.Width, s.Height);
			}
			//EngineWindow.SetView(new View(new Vector2(s.Width/2f, s.Height/2f), new Vector2(s.Width, s.Height)));
			
		}

		private void OnEngineWindowFocusGained(object sender, EventArgs e)
		{
			ActiveLevel.OnGameResume();
		}

		private void OnEngineWindowFocusLost(object sender, EventArgs e)
	    {
			ActiveLevel.OnGamePause();
		}

		public void StartEngine()
		{
			Debug.Log("Starting Engine!");
			InitEngineLoop();
		}

		private void InitEngineLoop()
        {
            
			if (ActiveLevel == null)
	        {
				Console.WriteLine("FATAL ERROR: NO ACTIVE LEVEL FOUND!");
				return;
	        }

            Window.Run();
		}

	    private void ShutdownEngine()
        {
            Debug.Log("Shutting down Engine!");

			//Level.CollisionCircle.Dispose();
	        //Level.CollisionRectangle.Dispose();
	        ActiveLevel.ShutdownLevel();
        }

        private void OnEngineWindowClose()
        {
            ActiveLevel.OnGameEnd();
            ShutdownEngine();
		}

		/// <summary>
		/// Loads the given level and either destroys the previous one or pauses and unloads it.
		/// </summary>
		/// <param name="level"></param>
		/// <param name="destroyPrevious"></param>
		/// <returns></returns>
		public bool LoadLevel(Level level, bool destroyPrevious = true)
		{
			if (level == null) 
                return false;

            ActiveLevel?.OnGameEnd();
            ActiveLevel?.ShutdownLevel();
            //AssetManager.ClearPools();

            ActiveLevel = level;

			if (level.LevelID == 0)
			{
				level.EngineReference = this;
				level.LevelID = ++LevelIDCounter;
				level.LevelLoaded = true;
				level.OnLevelLoad();
				level.LevelTicking = true;
			}
			return true;
		}


		public bool LoadLevel(string levelName, bool destroyPrevious = true)
		{
			if (string.IsNullOrWhiteSpace(levelName)) return false;

			var level = JsonConvert.DeserializeObject<Level>(levelName);
			//TODO: Implement everything
			return LoadLevel(level, destroyPrevious);
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
}
