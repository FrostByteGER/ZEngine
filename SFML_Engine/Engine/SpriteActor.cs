using System;

namespace SFML_Engine.Engine
{
    class SpriteActor : ITickableInterface
    {

        public void Tick(double deltaTime)
        {
            Console.WriteLine("Actor Tick!");
        }
    }
}
