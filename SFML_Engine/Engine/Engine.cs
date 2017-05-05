using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine
{

	public class Engine
    {

		private static Engine instance;
		public static Engine Instance => instance ?? (instance = new Engine());

		private RenderWindow engineWindow;
        public RenderWindow EngineWindow
        {
            get => engineWindow;
	        private set => engineWindow = value;
        }


		// Frame and Physics
		public EngineClock EngineCoreClock;
	    public float FrameDelta { get; set; } = 0.0f;
		public float Timestep { get; set; } = 1.0f / 100.0f;
		private double Accumulator { get; set; } = 0.0;
	    public float FramesPerSecond { get; private set; } = 0.0f;
		private float FrameAccumulator { get; set; } = 0.0f;


		// Core Engine
		public GameInfo GameInfo { get; set; } = new GameInfo();
		public List<Level> Levels { get; private set; } = new List<Level>();
	    public Level ActiveLevel { get; internal set; }
		public uint LevelIDCounter { get; private set; } = 0;


		// Engine Managers
		public PhysicsEngine PhysicsEngine { get; private set; }
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
	    public ContextSettings.Attribute OpenGLVersion = ContextSettings.Attribute.Default;


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
	        ContextSettings settings = new ContextSettings(DepthBufferSize, StencilBufferSize, AntiAliasingLevel, MajorOpenGLVersion, MinorOpenGLVersion, OpenGLVersion);
            engineWindow = new RenderWindow(new VideoMode(EngineWindowWidth, EngineWindowHeight), GameInfo.GenerateFullGameName() , Styles.Titlebar | Styles.Close , settings);
            engineWindow.Closed += OnEngineWindowClose;
            engineWindow.SetVerticalSyncEnabled(VSyncEnabled);
			engineWindow.SetFramerateLimit(FPSLimit);
			engineWindow.SetKeyRepeatEnabled(true);

			EngineCoreClock = new EngineClock();
            PhysicsEngine = new PhysicsEngine();
			InputManager = new InputManager();
			InputManager.RegisterEngineInput(ref engineWindow);

        }

		public void StartEngine()
		{
			Console.WriteLine("Starting Engine!");
			InitEngineLoop();
		}

		private void InitEngineLoop()
        {
            engineWindow.SetActive();
			if (ActiveLevel == null)
	        {
		        if (Levels.Count == 0)
		        {
					Console.WriteLine("FATAL ERROR: NO ACTIVE LEVEL FOUND!");
					return;
				}
		        LoadLevel(Levels[0]);

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
					engineWindow.SetTitle(GameInfo.GameFullName + " FPS: " + FramesPerSecond + " | Frame: " + FrameDelta + " | Render: " + EngineCoreClock.RenderAverage + " | Update: " + EngineCoreClock.UpdateAverage + " | Physics: " + EngineCoreClock.PhysicsAverage);

					FrameAccumulator = 0.0f;
					EngineCoreClock.Reset();
				}

				if (EngineCoreClock != null)
				{
					//clock.StartPhysics();
					//velcroWorld.Step(FrameDelta);
					//clock.StopPhysics();
				}
	            var t = 1;
	            Accumulator += FrameDelta;
				while (Accumulator >= Timestep)
                {
	                if (!ActiveLevel.LevelTicking)
	                {
		                continue;
	                }
					var actors = ActiveLevel.Actors;

					EngineCoreClock.StartPhysics();
					PhysicsEngine.PhysicsTick(Timestep, ref actors);
					EngineCoreClock.StopPhysics();

					EngineCoreClock.StartUpdate();
					ActiveLevel.LevelTick(FrameDelta);
	                EngineCoreClock.StopUpdate();
	                Console.WriteLine(t);
                    Accumulator -= Timestep;
	                ++t;
                }

                engineWindow.Clear();
                engineWindow.DispatchEvents();

	            EngineCoreClock.StartRender();
	            ActiveLevel.LevelDraw(ref engineWindow);
				EngineCoreClock.StopRender();

				engineWindow.Display();

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


			foreach (var level in Levels)
	        {
		        level.OnGameEnd();
	        }
			ShutdownEngine();
        }

	    private void ShutdownEngine()
        {
            Console.WriteLine("Shutting down Engine!");
	        PhysicsEngine.Shutdown();
			foreach (var level in Levels)
			{
				level.CollisionCircle.Dispose();
				level.CollisionRectangle.Dispose();
				foreach (var actor in level.Actors)
				{
					actor.CollisionShape.Dispose();
					actor.Dispose();
				}
				level.Actors.Clear();
				
			}
	        Levels.Clear();
			engineWindow.Dispose();
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
			engineWindow.Close();
			RequestTermination = true;
		}

		public void ShutdownLevel(uint levelID)
	    {
		    FindLevel(levelID).OnGameEnd();
	    }



        public void RegisterLevel(Level level)
        {
	        if (Levels.Contains(level)) return;
            level.EngineReference = this;
			level.LevelID = ++LevelIDCounter;
			Levels.Add(level);
        }

	    public bool UnregisterLevel(Level level)
	    {
		    return Levels.Remove(level);
	    }

		public bool UnregisterLevel(uint levelID)
		{
			Console.WriteLine("Trying to remove Level with LevelID: #" + levelID);
			var level = FindLevel(levelID);
			return UnregisterLevel(level);
		}

	    public bool LoadLevel(uint levelID)
	    {
		    return LoadLevel(FindLevel(levelID));
	    }

		public bool LoadLevel(Level level)
		{
			if (level == null) return false;
			if (level.LevelID > 0 && level != ActiveLevel)
			{
				ActiveLevel?.OnGameEnd();
				ActiveLevel = level;
				level.OnLevelLoad();
				level.LevelTicking = true;
				return true;
			}
			if (level == ActiveLevel)
			{
				return false;
			}
			RegisterLevel(level);
			return LoadLevel(level);
		}

		public void RegisterEvent(EngineEvent e)
		{
			if (EngineEvents.Contains(e)) return;
			e.EventID = ++EventIDCounter;
			EngineEvents.Enqueue(e);
	    }

		public Level FindLevel(uint id)
		{
			return Levels.Find(x => x.LevelID == id);
		}
	}
}
