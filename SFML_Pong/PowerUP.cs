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

		public override void IsOverlapping(Actor actor)
		{
			if (actor.GetType() == typeof(PongBall))
			{
				PongBall ball = (PongBall)actor;

				ball.Velocity *= -1;
			
			}
		}

	}
}
