using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Game.Game
{
    class Engine
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
            cs.FillColor = Color.Green;
            while (!RequestTermination)
            {
                EngineLoopClock.Restart();


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
