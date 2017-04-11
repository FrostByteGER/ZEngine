using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML_Engine.Engine;

namespace SFML_Pong
{
	class PowerUP : SpriteActor
	{

		public PowerUP()
		{
			
		}

		public override void IsOverlapping(Actor actor)
		{
			if (actor.GetType() == typeof(PongBall))
			{
				PongBall ball = (PongBall)actor;

				SphereShape ss = (SphereShape)ball.CollisionShape;

				Random r = new Random();

				if (ss.SphereDiameter < 30)
				{
					ss.SphereDiameter += 10;
				}
				else if(ss.SphereDiameter > 100)
				{
					ss.SphereDiameter -= 10;
				}
				else if(r.NextDouble() > 0.5f)
				{
					ss.SphereDiameter += (float)r.NextDouble()*10;
				}
				else
				{
					ss.SphereDiameter -= (float)r.NextDouble() * 10;
				}

				Console.WriteLine("Overlap");
			
			}
		}
	}
}
