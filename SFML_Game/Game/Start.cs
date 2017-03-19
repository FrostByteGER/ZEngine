using System;
using SFML.Graphics;
using SFML.Window;
using SFML_Game.Game;

namespace SFML_Game
{
    class Start
    {
        public static void Main(string[] args)
        {
            Engine engine = new Engine(800, 600, "Engine");
            engine.StartEngine();
        }
    }
}
