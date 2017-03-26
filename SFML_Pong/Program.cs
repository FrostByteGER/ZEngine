using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML_Engine.Engine;

namespace SFML_Pong
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine(800, 600, "Pong");
            var Level = new Level();
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            Texture t = new Texture("Assets/SFML_Pong/Goku.png");
            var actor = new SpriteActor(t);
            var pc = new PlayerController(actor);
            Level.RegisterActor(actor);
            engine.RegisterLevel(ref Level);
            engine.RegisterPlayer(ref pc);
            engine.StartEngine();
            Console.ReadLine();
        }
    }
}
