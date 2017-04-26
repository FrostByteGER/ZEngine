using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML_Engine.Engine;
using SFML.System;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Physics;

namespace SFML_Pong
{
	class PowerUP : SpriteActor
	{

		public PowerUP()
		{
			
		}

		public override void IsOverlapping(Actor actor)
		{
			if (!MarkedForRemoval && actor.GetType() == typeof(PongBall))
			{
				PongBall ball = (PongBall)actor;

				SphereShape ss = (SphereShape)ball.CollisionShape;
				Texture t = ball.Texture;

				Random r = new Random();

				if (ss.SphereDiameter < 30)
				{
					ss.SphereDiameter += 10;
					ball.Position = new Vector2f(ball.Position.X - 5, ball.Position.Y - 5);
				}
				else if(ss.SphereDiameter > 100)
				{
					ss.SphereDiameter -= 10;
					ball.Position = new Vector2f(ball.Position.X + 5, ball.Position.Y + 5);
				}
				else if(r.NextDouble() > 0.5f)
				{
					ss.SphereDiameter += 10;
					ball.Position = new Vector2f(ball.Position.X - 5, ball.Position.Y - 5);
				}
				else
				{
					ss.SphereDiameter -= 10;
					ball.Position = new Vector2f(ball.Position.X + 5, ball.Position.Y + 5);
				}

				Console.WriteLine("Overlap");
				Engine.Instance.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, this)));
			}
		}
	}
}
