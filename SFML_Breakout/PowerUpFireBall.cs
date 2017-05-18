using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Breakout
{
	class PowerUpFireBall : PowerUp
	{
		public PowerUpFireBall()
		{
			CollisionShape.CollisionShapeColor = new SFML.Graphics.Color(255, 128, 0);
		}

		public PowerUpFireBall(PowerUpFireBall p)
		{
			Velocity = p.Velocity;
			CollisionShape = new SphereShape(p.CollisionShape.CollisionBounds.X);
			CollisionShape.CollisionShapeColor = p.CollisionShape.CollisionShapeColor;
		}

		public override void Apply()
		{
			
			foreach (SpriteActor sa  in LevelReference.FindActorsInLevel("Ball"))
			{
				if (sa is BreakoutBall)
				{
					BreakoutBall ball = (BreakoutBall)sa;
					ball.fire = true;
				}
			}
		}

		public override object Clone()
		{
			return new PowerUpFireBall(this);
		}

	}
}
