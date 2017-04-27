using SFML.Graphics;
using SFML.System;
using Sprite = SFML_Engine.Engine.SFML.Graphics.Sprite;

namespace SFML_Engine.Engine
{
    public class SpriteActor : Sprite
    {

	    public bool SnapOriginToCenter { get; set; } = true;

        public SpriteActor()
        {
	        if (SnapOriginToCenter)
	        {
		        FloatRect bounds = GetGlobalBounds();
				//Origin = new Vector2f(bounds.Width / 2.0f, bounds.Height / 2.0f);
	        }
        }

        public SpriteActor(Texture texture) : base(texture)
        {
			if (SnapOriginToCenter)
			{
				FloatRect bounds = GetGlobalBounds();
				//Origin = new Vector2f(bounds.Width / 2.0f, bounds.Height / 2.0f);
			}
		}

        public SpriteActor(Texture texture, IntRect rectangle) : base(texture, rectangle)
        {
        }

        public SpriteActor(Sprite copy) : base(copy)
        {
        }

        public override void Tick(float deltaTime)
        {
			base.Tick(deltaTime);
			//Console.WriteLine("Actor Tick!");
            //Position = new Vector2f(Position.X + 10.0f * deltaTime, Position.Y);
        }

	    public override void Move(float x, float y)
	    {
		    Position = new Vector2f(x,y);
	    }

	    public override void Move(Vector2f position)
	    {
		    Position = position;
	    }
    }
}
