using System;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine
{
    public class SpriteActor : Sprite, IActorable
    {

	    public bool SnapOriginToCenter { get; set; } = true;

        public SpriteActor()
        {
	        if (SnapOriginToCenter)
	        {
		        FloatRect bounds = GetGlobalBounds();
				Origin = new Vector2f(bounds.Width / 2.0f, bounds.Height / 2.0f);
	        }
        }

        public SpriteActor(Texture texture) : base(texture)
        {
			if (SnapOriginToCenter)
			{
				FloatRect bounds = GetGlobalBounds();
				Origin = new Vector2f(bounds.Width / 2.0f, bounds.Height / 2.0f);
			}
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
            //Console.WriteLine("Actor Tick!");
            //Position = new Vector2f(Position.X + 10.0f * deltaTime, Position.Y);
        }

	    public void Move(float x, float y)
	    {
		    Position = new Vector2f(x,y);
	    }

	    public void Move(Vector2f position)
	    {
		    Position = position;
	    }
    }
}
