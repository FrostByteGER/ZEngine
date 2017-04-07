﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Physics;

namespace SFML_Engine.Engine
{

	public class Engine
    {

		private static Engine instance;

		public static Engine Instance => instance ?? (instance = new Engine());


	    public uint EngineWindowHeight { get; set; }
        public uint EngineWindowWidth { get; set; }
        public string GameName { get; set; }
        private RenderWindow engineWindow;

        public RenderWindow EngineWindow
        {
            get { return engineWindow; }
            private set { engineWindow = value; }
        }

        public Clock EngineClock { get; private set; }
        public Clock EngineLoopClock { get; private set; }
        public Clock FPSClock { get; private set; }
        public Time DeltaTime { get; private set; }
        private Time FPSStartTime { get; set; }
        public Time FPSPassedTime { get; set; }
        private uint FramesRendered { get; set; }
        public double FPS { get; private set; }

	    public uint FPSLimit { get; private set; } = 120;

        public float FPSUpdateValue { get; } = 1f;

        public bool RequestTermination { get; set; }
	    public bool VSyncEnabled { get; set; } = false;

        public List<Level> Levels { get; private set; } = new List<Level>();

        public List<PlayerController> Players { get; private set; } = new List<PlayerController>();
        public PhysicsEngine PhysicsEngine { get; private set; }
		public InputManager InputManager { get; set; }

	    public Queue<Event> EngineEvents { get; private set; } = new Queue<Event>();

	    public uint ActorIDCounter { get; set; } = 0;
		public uint LevelIDCounter { get; set; } = 0;




		private Engine()
	    {
		    
	    }

        public void StartEngine()
        {
            Console.WriteLine("Starting Engine!");
            InitEngineLoop();
        }

        public void InitEngine()
        {
            EngineClock = new Clock();
            engineWindow = new RenderWindow(new VideoMode(EngineWindowWidth, EngineWindowHeight), GameName);
            engineWindow.Closed += OnEngineWindowClose;
            engineWindow.SetVerticalSyncEnabled(VSyncEnabled);
			engineWindow.SetFramerateLimit(FPSLimit);
			engineWindow.SetKeyRepeatEnabled(true);

            PhysicsEngine = new PhysicsEngine();
			InputManager = new InputManager();
			InputManager.RegisterEngineInput(ref engineWindow);

        }

        private void InitEngineLoop()
        {
            engineWindow.SetActive();

            while (engineWindow.IsOpen)
            {
                break;
            }
            EngineTick();
        }

        private void EngineTick()
        {
            FPSClock = new Clock();
            EngineLoopClock = new Clock();

            
            double accumulator = 0.0;
            double timestep = 1.0/100.0;
            double time = 0.0;
            Time currentTime = Time.Zero;
            FPSStartTime = Time.Zero;
            FPSPassedTime = Time.Zero;
	        foreach (var level in Levels)
	        {
		        level.OnGameStart();
		        level.LevelTicking = true;
	        }
	        FPSClock.Restart();
            EngineLoopClock.Restart();
            while (!RequestTermination)
            {

                Time newTime = EngineLoopClock.ElapsedTime;
                DeltaTime = newTime - currentTime;
                currentTime = newTime;



                accumulator += DeltaTime.AsSeconds();

                FPSPassedTime = FPSClock.ElapsedTime;

				//Console.WriteLine("Delta: " + DeltaTime.AsSeconds() + " Current Time: " + newTime.AsSeconds() + " Previous Time: " + currentTime.AsSeconds() + " Frames Rendered: " + FramesRendered + " FPS: " + Convert.ToUInt32(1.0f / DeltaTime.AsSeconds()));
				FPS = 1.0f / DeltaTime.AsSeconds();

				if ((FPSPassedTime - FPSStartTime).AsSeconds() > FPSUpdateValue && FramesRendered > 10)
                {
                    FPSStartTime = FPSPassedTime;
					engineWindow.SetTitle("Delta: " + DeltaTime.AsSeconds() + " Current Time: " + newTime.AsSeconds() + " Previous Time: " + currentTime.AsSeconds() + " Frames Rendered: " + FramesRendered + " FPS: " + Convert.ToUInt32(FPS));

					FramesRendered = 0;
                }

				while (accumulator >= timestep)
                {
                    //Console.WriteLine("Engine Tick!");
                    foreach (var level in Levels)
                    {
	                    if (!level.LevelTicking)
	                    {
		                    continue;
	                    }
                        var actors = level.Actors;
                        PhysicsEngine.PhysicsTick(DeltaTime.AsSeconds(), ref actors);
                        level.LevelTick(DeltaTime.AsSeconds());
                    }
                    time += timestep;
                    accumulator -= timestep;
                }

                engineWindow.Clear();
                engineWindow.DispatchEvents();
                foreach (var level in Levels)
                {
                    level.LevelDraw(ref engineWindow);
                }
                engineWindow.Display();

				// Execute all pending events in the Queue
	            while (EngineEvents.Count > 0)
	            {
		            EngineEvents.Dequeue().ExecuteEvent();
	            }
                FramesRendered++;
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
        }

        private void OnEngineWindowClose(object sender, EventArgs args)
        {
            // Close the window when OnClose event is received
            var window = (RenderWindow)sender;
            window.Close();
            RequestTermination = true;

        }

	    public void ShutdownLevel(uint levelID)
	    {
		    FindLevel(levelID).OnGameEnd();
	    }



        public void RegisterLevel(Level level)
        {
            level.Engine = this;
			level.LevelID = LevelIDCounter;
			++LevelIDCounter;
			Levels.Add(level);
        }

        public void RegisterPlayer(PlayerController pc)
        {
            Players.Add(pc);
            pc.ID = (uint) Players.Count - 1;
			pc.RegisterInput(this);
			if (pc.ID == 0) //TODO: Remove
            {
                //pc.RegisterInput(this);
            }
        }

	    public void RegisterEvent(Event e)
	    {
		    EngineEvents.Enqueue(e);
	    }

		public Level FindLevel(uint id)
		{
			return Levels.Find(x => x.LevelID == id);
		}
	}
}
