using System;
using System.Collections.Generic;
using System.Threading;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.Physics;

namespace SFML_Engine.Engine
{
    public class Engine
    {

        private uint EngineWindowHeight;
        private uint EngineWindowWidth;
        private string GameName;

        private RenderWindow EngineWindow;
        private Clock EngineClock;
        private Clock EngineLoopClock;
        private Clock FPSClock;
        private Time DeltaTime;
        private Time FPSStartTime;
        private Time FPSPassedTime;
        private uint FramesRendered;
        private double FPS;

        private const float FPSUpdateValue = 1.0f;

        private bool RequestTermination;
        private bool VSyncEnabled;

        private List<Level> Levels;
        private PhysicsEngine PhysicsEngine;




        public Engine(uint engineWindowWidth, uint engineWindowHeight, string gameName)
        {
            EngineWindowWidth = engineWindowWidth;
            EngineWindowHeight = engineWindowHeight;
            GameName = gameName;
            Levels = new List<Level>();
            VSyncEnabled = true;
        }

        public void StartEngine()
        {
            Console.WriteLine("Starting Engine!");
            InitEngine();
            InitEngineLoop();
        }

        private void InitEngine()
        {
            EngineClock = new Clock();
            EngineWindow = new RenderWindow(new VideoMode(EngineWindowWidth, EngineWindowHeight), GameName);
            EngineWindow.SetVerticalSyncEnabled(VSyncEnabled);

            PhysicsEngine = new PhysicsEngine();

        }

        private void InitEngineLoop()
        {
            EngineWindow.SetActive();

            while (EngineWindow.IsOpen)
            {
                break;
            }
            EngineTick();
        }

        private void EngineTick()
        {
            FPSClock = new Clock();
            EngineLoopClock = new Clock();
            CircleShape cs = new CircleShape(100.0f);
            cs.FillColor = Color.Magenta;

            
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


                Console.WriteLine(DeltaTime.AsSeconds() + " " + newTime.AsSeconds() + " " + currentTime.AsSeconds() + " " + FramesRendered);

                accumulator += DeltaTime.AsSeconds();

                FPSPassedTime = FPSClock.ElapsedTime;
                if ((FPSPassedTime - FPSStartTime).AsSeconds() > FPSUpdateValue && FramesRendered > 10)
                {
                    FPSStartTime = FPSPassedTime;
                    EngineWindow.SetTitle(VSyncEnabled ? FramesRendered.ToString() : (FramesRendered / DeltaTime.AsSeconds()).ToString());
                    FramesRendered = 0;
                }

                while (accumulator >= timestep)
                {
                    Console.WriteLine("Engine Tick!");
                    PhysicsEngine.PhysicsTick(DeltaTime.AsSeconds());

                    foreach (Level level in Levels)
                    {
                        level.LevelTick(DeltaTime.AsSeconds());
                    }
                    time += timestep;
                    accumulator -= timestep;
                }

                EngineWindow.Clear();
                EngineWindow.DispatchEvents();
                EngineWindow.Draw(cs);
                EngineWindow.Display();
                FramesRendered++;
            }

            ShutdownEngine();
        }

        private void ShutdownEngine()
        {
            Console.WriteLine("Shutting down Engine!");
        }

        public void RegisterLevel(ref Level level)
        {
            Levels.Add(level);
        }
    }
}
