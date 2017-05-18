using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Breakout
{
	class PowerUpPunchThrow : PowerUp
	{

		public PowerUpPunchThrow()
		{
			CollisionShape.CollisionShapeColor = new SFML.Graphics.Color(255, 255, 255);

		}

		public PowerUpPunchThrow(PowerUpPunchThrow p)
		{
			Velocity = p.Velocity;
			CollisionShape = new SphereShape(p.CollisionShape.CollisionBounds.X);
			CollisionShape.CollisionShapeColor = p.CollisionShape.CollisionShapeColor;
		}

		public override void Apply()
		{	

			// Must be reactivated by Level reset
			LevelReference.EngineReference.PhysicsEngine.RemoveCollidablePartner("Blocks", "Balls");
			LevelReference.EngineReference.PhysicsEngine.RemoveCollidablePartner("Balls", "Blocks");
		}

		public override object Clone()
		{
			return new PowerUpPunchThrow(this);
		}
	}
}
