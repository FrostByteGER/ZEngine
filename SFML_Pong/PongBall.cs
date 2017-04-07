using System;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Graphics;

namespace SFML_Pong
{
	public class PongBall : SpriteActor
	{

		public Actor LastPlayerCollision { get; set; }
		public PongBall()
		{
		}

		public PongBall(Texture texture) : base(texture)
		{
		}

		public PongBall(Texture texture, IntRect rectangle) : base(texture, rectangle)
		{
		}

		public PongBall(Sprite copy) : base(copy)
		{
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			//Rotation += 25.0f * deltaTime;

		}

		public override void AfterCollision(Actor actor)
		{
			base.AfterCollision(actor);
			if (actor.ActorName == "Left Border" || actor.ActorName == "Right Border")
			{				
				
				
			}
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