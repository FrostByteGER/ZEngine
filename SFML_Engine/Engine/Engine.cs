using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

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
        private Time DeltaTime;

        private bool RequestTermination;


        public Engine(uint engineWindowWidth, uint engineWindowHeight, string gameName)
        {
            EngineWindowWidth = engineWindowWidth;
            EngineWindowHeight = engineWindowHeight;
            GameName = gameName;

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
            EngineLoopClock = new Clock();
            CircleShape cs = new CircleShape(100.0f);
            cs.FillColor = Color.Magenta;

            Time currentTime = EngineLoopClock.Restart();
            double accumulator = 0.0;
            double dt = 0.01;
            double t = 0.0;
            while (!RequestTermination)
            {
                Time newTime = EngineLoopClock.ElapsedTime;
                Time frameTime = newTime - currentTime;
                if (frameTime.AsSeconds() > 0.25f)
                {
                    //frameTime = 0.25;
                }
                currentTime = newTime;

                accumulator += frameTime.AsSeconds();
                while (accumulator >= dt)
                {
                    //Physics
                    t += dt;
                    accumulator -= dt;
                }

                EngineWindow.Clear();
                EngineWindow.DispatchEvents();
                EngineWindow.Draw(cs);
                EngineWindow.Display();

                DeltaTime = EngineLoopClock.ElapsedTime;
            }

            ShutdownEngine();
        }

        private void ShutdownEngine()
        {
            Console.WriteLine("Shutting down Engine!");
        }

    }
}
