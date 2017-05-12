using SFML.Graphics;
using SFML.System;
using Sprite = SFML_Engine.Engine.SFML.Graphics.Sprite;

namespace SFML_Engine.Engine
{
    public class SpriteActor : Actor
    {

	    public bool SnapOriginToCenter { get; set; } = true;

	    public SpriteActor()
        {
	        if (SnapOriginToCenter)
	        {
				//Origin = new Vector2f(bounds.Width / 2.0f, bounds.Height / 2.0f);
	        }
        }

	    public SpriteActor(SpriteComponent spriteComp)
	    {
		    SetRootComponent(spriteComp);
	    }

	    public SpriteActor(Sprite sprite)
	    {
		    SetRootComponent(new SpriteComponent(sprite));
	    }

	    public SpriteActor(Texture t)
	    {
			SetRootComponent(new SpriteComponent(new Sprite(t)));
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
