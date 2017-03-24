using System;
using SFML.System;

namespace SFML_Engine.Engine
{
    class SpriteActor : ITickableInterface ,IMovable ,IPhysicsProperties
    {

        public bool Movable { get; set; }

        public Vector2f Velocity { get; set; }

        public Vector2f Acceleration { get; set; }

        public float Mass { get; set; }

        public void Tick(double deltaTime)
        {
            Console.WriteLine("Actor Tick!");
        }


        
    }
}
