using System;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine
{
    public class SpriteActor : Sprite, IActorable
    {

        public SpriteActor()
        {
        }

        public SpriteActor(Texture texture) : base(texture)
        {
        }

        public SpriteActor(Texture texture, IntRect rectangle) : base(texture, rectangle)
        {
        }

        public SpriteActor(Sprite copy) : base(copy)
        {
        }

        public Shape CollisionShape { get; set; }
        public bool Movable { get; set; }

        public Vector2f Velocity { get; set; }

        public Vector2f Acceleration { get; set; }

        public float Mass { get; set; }

        

        public void Tick(float deltaTime)
        {
            Console.WriteLine("Actor Tick!");
            //Position = new Vector2f(Position.X + 10.0f * deltaTime, Position.Y);
        }
    }
}
