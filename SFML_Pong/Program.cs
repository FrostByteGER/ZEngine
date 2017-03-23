using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML_Engine.Engine;

namespace SFML_Pong
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine(800, 600, "Pong");
            engine.StartEngine();
        }
    }
}
