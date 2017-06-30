﻿using System;
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
	    public List<Level> LevelStack { get; private set; } = new List<Level>();
		public uint LevelIDCounter { get; private set; } = 0;


		// Engine Managers
		public PhysicsEngine GUIPhysicsEngine { get; private set; }
		public InputManager InputManager { get; set; }
		public AssetManager AssetManager { get; set; }


		// Events
		public Queue<EngineEvent> EngineEvents { get; private set; } = new Queue<EngineEvent>();
		public uint EventIDCounter { get; private set; } = 0;


		// Engine OpenGL Settings
		public uint DepthBufferSize { get; internal set; }     = 24;
	    public uint StencilBufferSize { get; internal set; }   = 8;
	    public uint AntiAliasingLevel { get; internal set; }   = 4;
	    public uint MajorOpenGLVersion { get; internal set; }  = 4;
	    public uint MinorOpenGLVersion { get; internal set; }  = 5;
	    private ContextSettings.Attribute OpenGLContextType    = ContextSettings.Attribute.Default;
	    public bool SRGBCompatible { get; internal set; }      = false;


		// Engine Settings
	    public uint EngineWindowHeight { get; set; }        = 800;
	    public uint EngineWindowWidth { get; set; }         = 600;
		public bool VSyncEnabled { get; set; }              = false;
		public uint FPSLimit { get; set; }                  = 120;
	    public bool EngineInitialized { get; private set; } = false;
		private bool _pauseEngineOnWindowFocusLose          = false;
		public bool PauseEngineOnWindowFocusLose
	    {
		    get => _pauseEngineOnWindowFocusLose;
		    set
		    {
				if (!value && EngineInitialized)
				{
					_engineWindow.LostFocus -= OnEngineWindowFocusLost;
					_engineWindow.GainedFocus -= OnEngineWindowFocusGained;
				}
				else if (!_pauseEngineOnWindowFocusLose && EngineInitialized)
				{
					_engineWindow.LostFocus += OnEngineWindowFocusLost;
					_engineWindow.GainedFocus += OnEngineWindowFocusGained;
				}
				_pauseEngineOnWindowFocusLose = value;
		    }
	    }


	    // Other Settings
	    public uint GlobalVolume { get; set; }      = 100;
	    public uint GlobalMusicVolume { get; set; } = 100;
	    public uint GlobalSoundVolume { get; set; } = 100;

	    public bool RequestTermination { get; set; } = false;
	    public bool EngineTicking { get; set; }      = true;


		private Engine()
	    {
		    
	    }

        public void InitEngine()
        {
	        var settings = new ContextSettings(DepthBufferSize, StencilBufferSize, AntiAliasingLevel, MajorOpenGLVersion, MinorOpenGLVersion, OpenGLContextType, SRGBCompatible);
            _engineWindow = new RenderWindow(new VideoMode(EngineWindowWidth, EngineWindowHeight), GameInfo.GenerateFullGameName() , Styles.Titlebar | Styles.Close | Styles.Resize , settings);
            _engineWindow.Closed += OnEngineWindowClose;
			_engineWindow.Resized += OnEngineWindowResized;
            _engineWindow.SetVerticalSyncEnabled(VSyncEnabled);
			_engineWindow.SetFramerateLimit(FPSLimit);
			_engineWindow.SetKeyRepeatEnabled(false);

			EngineCoreClock = new EngineClock();
            GUIPhysicsEngine = new PhysicsEngine();
	        AssetManager = new AssetManager();
			AssetManager.TextureFolderName = GameInfo.GameTextureFolderName;
			AssetManager.SoundFolderName = GameInfo.GameSoundFolderName;
			AssetManager.ConfigFolderName = GameInfo.GameConfigFolderName;
			AssetManager.FontFolderName = GameInfo.GameFontFolderName;
			AssetManager.ShaderFolderName = GameInfo.GameShaderFolderName;
			AssetManager.LevelFolderName = GameInfo.GameLevelFolderName;
			AssetManager.InitPackages();
			InputManager = new InputManager();
			InputManager.RegisterEngineInput(ref _engineWindow);
	        EngineInitialized = true;

        }

		private void OnEngineWindowResized(object sender, SizeEventArgs e)
		{
			Console.WriteLine("---");
			Console.WriteLine("1: "+EngineWindow.GetView());
			EngineWindowWidth = e.Width;
			EngineWindowHeight = e.Height;
			foreach (var p in ActiveLevel.Players)
			{
				p.PlayerCamera.Center = new TVector2f();
				p.PlayerCamera.Size = new TVector2f(e.Width, e.Height);
			}
			Console.WriteLine("2: "+EngineWindow.GetView());
			EngineWindow.SetView(new View(new TVector2f(e.Width/2f, e.Height/2f), new TVector2f(e.Width, e.Height)));
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
	            if (!EngineTicking)
	            {
					// Only Handle Events like Gain Focus or Lost Focus
					_engineWindow.DispatchEvents();
		            continue;
	            }



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
	            if (ActiveLevel.PhysicsEngine.CanTick)
	            {
					EngineCoreClock.StartPhysics();
					ActiveLevel.PhysicsEngine?.PhysicsTick(FrameDelta);
					EngineCoreClock.StopPhysics();
				}


				// Tick Level and Actors
				EngineCoreClock.StartUpdate();
				if (InputManager.CanTick) InputManager.Tick(FrameDelta);
	            if (ActiveLevel.LevelTicking) ActiveLevel.LevelTick(FrameDelta);
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
				AssetManager.TextureManager.ClearPool();
				AssetManager.AudioManager.ClearPool();
			}
			else
			{
				if (ActiveLevel != null)
				{
					ActiveLevel.OnGamePause();
					ActiveLevel.LevelLoaded = false;
					ActiveLevel.OnUnloadLevel();
					LevelStack.Add(ActiveLevel);
				}
			}

			ActiveLevel = level;

			if (level.LevelID == 0)
			{
				level.EngineReference = this;
				level.LevelID = ++LevelIDCounter;
				level.LevelLoaded = true;
				level.OnLevelLoad();
				level.LevelTicking = true;
			}
			if (level.LevelPaused)
			{
				level.LevelLoaded = true;
				level.OnGameResume();
			}


			Console.WriteLine("Levelstack Size: " + LevelStack.Count);
			return true;
		}

	    public bool LoadLevel(uint levelID)
	    {
		    return LoadLevel(levelID, true);
	    }

	    public bool LoadLevel(uint levelID, bool destroyPrevious)
	    {
		    var index = LevelStack.FindIndex(l => l.LevelID == levelID);
		    if (index == -1) return false;
		    var level = LevelStack[index];
			if (index < LevelStack.Count - 1)
		    {
			    for (var i = index + 1; i < LevelStack.Count; ++i)
			    {
				    LevelStack[i].OnGameEnd();
				    LevelStack[i].ShutdownLevel();

			    }
		    }
			LevelStack.RemoveRange(index, LevelStack.Count - index);
			return LoadLevel(level, destroyPrevious);
	    }

	    public bool LoadPreviousLevel()
	    {
		    var level = LevelStack[LevelStack.Count - 1];
		    LevelStack.RemoveAt(LevelStack.Count - 1);
			return LoadLevel(level, true);
	    }

		public bool LoadPreviousLevel(bool destroyPrevious)
		{
			var level = LevelStack[LevelStack.Count - 1];
			LevelStack.RemoveAt(LevelStack.Count - 1);
			return LoadLevel(level, destroyPrevious);
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

		// TODO: Add Compiled Lambda Dictionary 
	    public static void ConstructActor()
	    {
		    
	    }
	}
}
