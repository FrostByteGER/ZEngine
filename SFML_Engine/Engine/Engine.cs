using System;
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

        public uint EngineWindowHeight { get; private set; }
        public uint EngineWindowWidth { get; private set; }
        public string GameName { get; private set; }
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

	    public uint FPSLimit { get; private set; } = 1000;

        public float FPSUpdateValue { get; } = 1f;

        public bool RequestTermination { get; set; }
	    public bool VSyncEnabled { get; set; } = true;

        public List<Level> Levels { get; private set; } = new List<Level>();

        public List<PlayerController> Players { get; private set; } = new List<PlayerController>();
        public PhysicsEngine PhysicsEngine { get; private set; }
		public InputManager InputManager { get; set; }




        public Engine(uint engineWindowWidth, uint engineWindowHeight, string gameName)
        {
            EngineWindowWidth = engineWindowWidth;
            EngineWindowHeight = engineWindowHeight;
            GameName = gameName;
            InitEngine();
        }

        public void StartEngine()
        {
            Console.WriteLine("Starting Engine!");
            InitEngineLoop();
        }

        private void InitEngine()
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
            FPSClock.Restart();
            EngineLoopClock.Restart();
            while (!RequestTermination)
            {

                Time newTime = EngineLoopClock.ElapsedTime;
                DeltaTime = newTime - currentTime;
                currentTime = newTime;



                accumulator += DeltaTime.AsSeconds();

                FPSPassedTime = FPSClock.ElapsedTime;

				Console.WriteLine("Delta: " + DeltaTime.AsSeconds() + " Current Time: " + newTime.AsSeconds() + " Previous Time: " + currentTime.AsSeconds() + " Frames Rendered: " + FramesRendered + " FPS: " + 1.0f / DeltaTime.AsSeconds());
				FPS = VSyncEnabled ? FramesRendered : 1.0f / DeltaTime.AsSeconds();

				if ((FPSPassedTime - FPSStartTime).AsSeconds() > FPSUpdateValue && FramesRendered > 10)
                {
                    FPSStartTime = FPSPassedTime;
					engineWindow.SetTitle("Delta: " + DeltaTime.AsSeconds() + " Current Time: " + newTime.AsSeconds() + " Previous Time: " + currentTime.AsSeconds() + " Frames Rendered: " + FramesRendered + " FPS: " + FPS);

					FramesRendered = 0;
                }

				while (accumulator >= timestep)
                {
                    //Console.WriteLine("Engine Tick!");
                    foreach (var level in Levels)
                    {
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
                FramesRendered++;
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



        public void RegisterLevel(Level level)
        {
            level.Engine = this;
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
    }
}
