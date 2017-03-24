using System;

namespace SFML_Engine.Engine
{
    class SpriteActor : ITickableInterface ,IMovable
    {

        public void Tick(double deltaTime)
        {
            Console.WriteLine("Actor Tick!");
        }
    }
}
