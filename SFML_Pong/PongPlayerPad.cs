using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using Sprite = SFML_Engine.Engine.SFML.Graphics.Sprite;

namespace SFML_Pong
{
	public class PongPlayerPad : SpriteActor
	{
		public PongPlayerPad()
		{
		}

		public PongPlayerPad(Texture texture) : base(texture)
		{
		}

		public PongPlayerPad(Texture texture, IntRect rectangle) : base(texture, rectangle)
		{
		}

		public PongPlayerPad(Sprite copy) : base(copy)
		{
		}

		public override void AfterCollision(Actor actor)
		{
			base.AfterCollision(actor);
			//Velocity = new Vector2f(0.0f, Velocity.Y / Velocity.Y);
			//Acceleration = new Vector2f(0.0f, 0.0f);
		}

		public override void BeforeCollision(Actor actor)
		{
			base.BeforeCollision(actor);
		}

		public override void IsOverlapping(Actor actor)
		{
			base.IsOverlapping(actor);
		}
	}
}