using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.Window;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Core
{

	public class Engine
    {

		private static Engine _instance;
		public static Engine Instance => _instance ?? (_instance = new Engine());

		private RenderWindow _engineWindow;
        public RenderWindow EngineWindow
        {
            get => _engineWindow;
	        private set => _engineWindow = value;
        }


		// Frame and Physics
		public EngineClock EngineCoreClock;
	    public float FrameDelta { get; set; } = 0.0f;
		public float Timestep { get; set; } = 1.0f / 100.0f;
		private double Accumulator { get; set; } = 0.0;
	    public float FramesPerSecond { get; private set; } = 0.0f;
		private float FrameAccumulator { get; set; } = 0.0f;


		// Core Engine
	    public GameInstance GameInstance { get; set; } = new GameInstance();
		public GameInfo GameInfo { get; set; } = new GameInfo();
	    public Level ActiveLevel { get; internal set; }
		public uint LevelIDCounter { get; private set; } = 0;


		// Engine Managers
		public PhysicsEngine GUIPhysicsEngine { get; private set; }
		public InputManager InputManager { get; set; }
		public AssetManager AssetManager { get; set; }


		// Events
		public Queue<EngineEvent> EngineEvents { get; private set; } = new Queue<EngineEvent>();
		public uint EventIDCounter { get; private set; } = 0;


		// Engine OpenGL Settings
		public uint DepthBufferSize { get; internal set; }    = 24;
	    public uint StencilBufferSize { get; internal set; }  = 8;
	    public uint AntiAliasingLevel { get; internal set; }  = 4;
	    public uint MajorOpenGLVersion { get; internal set; } = 4;
	    public uint MinorOpenGLVersion { get; internal set; } = 5;
	    public ContextSettings.Attribute OpenGLContextType    = ContextSettings.Attribute.Default;
	    public bool SRGBCompatible { get; internal set; }     = false;


		// Engine Settings
	    public uint EngineWindowHeight { get; set; } = 800;
	    public uint EngineWindowWidth { get; set; }  = 600;
		public bool VSyncEnabled { get; set; }       = false;
		public uint FPSLimit { get; set; }           = 120;


		// Other Settings
		public uint GlobalVolume { get; set; } = 5;
		public bool RequestTermination { get; set; } = false;


		private Engine()
	    {
		    
	    }

        public void InitEngine()
        {
	        ContextSettings settings = new ContextSettings(DepthBufferSize, StencilBufferSize, AntiAliasingLevel, MajorOpenGLVersion, MinorOpenGLVersion, OpenGLContextType, SRGBCompatible);
            _engineWindow = new RenderWindow(new VideoMode(EngineWindowWidth, EngineWindowHeight), GameInfo.GenerateFullGameName() , Styles.Titlebar | Styles.Close , settings);
            _engineWindow.Closed += OnEngineWindowClose;
            _engineWindow.SetVerticalSyncEnabled(VSyncEnabled);
			_engineWindow.SetFramerateLimit(FPSLimit);
			_engineWindow.SetKeyRepeatEnabled(false);

			EngineCoreClock = new EngineClock();
            GUIPhysicsEngine = new PhysicsEngine();
			InputManager = new InputManager();
			InputManager.RegisterEngineInput(ref _engineWindow);

        }

		public void StartEngine()
		{
			Console.WriteLine("Starting Engine!");
			InitEngineLoop();
		}

		private void InitEngineLoop()
        {
            _engineWindow.SetActive();
			if (ActiveLevel == null)
	        {
				Console.WriteLine("FATAL ERROR: NO ACTIVE LEVEL FOUND!");
				return;
	        }
            EngineTick();
        }

        private void EngineTick()
        {
            while (!RequestTermination)
            {
				FrameDelta = EngineCoreClock.GetFrameDelta();
				FrameAccumulator += FrameDelta;
				if (FrameAccumulator >= 1.0f)
				{
					FramesPerSecond = EngineCoreClock.FrameCount / FrameAccumulator;
					_engineWindow.SetTitle(GameInfo.GameFullName + " FPS: " + FramesPerSecond + " | Frame: " + FrameDelta + " | Render: " + EngineCoreClock.RenderAverage + " | Update: " + EngineCoreClock.UpdateAverage + " | Physics: " + EngineCoreClock.PhysicsAverage);

					FrameAccumulator = 0.0f;
					EngineCoreClock.Reset();
				}

				// Tick Physics
				EngineCoreClock.StartPhysics();
				ActiveLevel.PhysicsEngine?.PhysicsTick(FrameDelta);
				EngineCoreClock.StopPhysics();

				// Tick Level and Actors
				EngineCoreClock.StartUpdate();
	            InputManager.Tick(FrameDelta);
				ActiveLevel.LevelTick(FrameDelta);
				EngineCoreClock.StopUpdate();

				_engineWindow.Clear();
                _engineWindow.DispatchEvents();

				// Render Level and Actors
	            EngineCoreClock.StartRender();
	            ActiveLevel.LevelDraw(ref _engineWindow);
				EngineCoreClock.StopRender();

				_engineWindow.Display();

				// Execute all pending events in the Queue
				while (EngineEvents.Count > 0)
	            {
		            var engineEvent = EngineEvents.Dequeue();
		            if (!engineEvent.Revoked)
					{
						engineEvent.ExecuteEvent();
					}
				}
            }
	        ActiveLevel.OnGameEnd();
			ShutdownEngine();
        }

	    private void ShutdownEngine()
        {
            Console.WriteLine("Shutting down Engine!");
	        GUIPhysicsEngine.Shutdown();

			Level.CollisionCircle.Dispose();
	        Level.CollisionRectangle.Dispose();
	        ActiveLevel.ShutdownLevel();

			_engineWindow.Dispose();
        }

        private void OnEngineWindowClose(object sender, EventArgs args)
        {
            // Close the window when OnClose event is received
            var window = (RenderWindow)sender;
            window.Close();
            RequestTermination = true;
        }

		public void CloseEngineWindow()
		{
			_engineWindow.Close();
			RequestTermination = true;
		}

		/// <summary>
		/// Loads the given level and destroys the previous one if there was one.
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		public bool LoadLevel(Level level)
		{
			return LoadLevel(level, true);
		}

		/// <summary>
		/// Loads the given level and either destroys the previous one or pauses and unloads it.
		/// </summary>
		/// <param name="level"></param>
		/// <param name="destroyPrevious"></param>
		/// <returns></returns>
		public bool LoadLevel(Level level, bool destroyPrevious)
		{
			if (level == null || level == ActiveLevel) return false;

			if (destroyPrevious)
			{
				ActiveLevel?.OnGameEnd();
				ActiveLevel?.ShutdownLevel();
			}
			else
			{
				if (ActiveLevel != null)
				{
					ActiveLevel.OnGamePause();
					ActiveLevel.LevelLoaded = false;
					ActiveLevel.OnUnloadLevel();
				}
			}

			ActiveLevel = level;
			level.EngineReference = this;
			level.LevelLoaded = true;
			level.OnLevelLoad();
			level.LevelTicking = true;
			return true;
		}

		public bool LoadLevel(string levelName)
		{
			return LoadLevel(levelName, true);
		}

		public bool LoadLevel(string levelName, bool destroyPrevious)
		{
			if (string.IsNullOrWhiteSpace(levelName)) return false;

			var level = JsonConvert.DeserializeObject<Level>(levelName);
			//TODO: Implement everything
			return LoadLevel(level, destroyPrevious);
		}

		public void RegisterEvent(EngineEvent e)
		{
			if (EngineEvents.Contains(e)) return;
			e.EventID = ++EventIDCounter;
			EngineEvents.Enqueue(e);
	    }
	}
}
