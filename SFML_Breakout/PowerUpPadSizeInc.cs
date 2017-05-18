using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Breakout
{
	class PowerUpPadSizeInc : PowerUp
	{

		float maxSize = 350;

		public PowerUpPadSizeInc()
		{
			CollisionShape.CollisionShapeColor = new SFML.Graphics.Color(0, 255, 0);
		}

		public PowerUpPadSizeInc(PowerUpPadSizeInc p)
		{
			Velocity = p.Velocity;
			CollisionShape = new SphereShape(p.CollisionShape.CollisionBounds.X);
			CollisionShape.CollisionShapeColor = p.CollisionShape.CollisionShapeColor;
		}

		public override void Apply()
		{
			base.Apply();

			SpriteActor pad = (SpriteActor)LevelReference.FindActorInLevel("Player Pad 1");

			BoxShape box = (BoxShape)pad.CollisionShape;

			if (box.CollisionBounds.X+10 <= maxSize)
			{
				box.CollisionBounds = new Vector2f(box.CollisionBounds.X + 10, box.CollisionBounds.Y);
				pad.Position = new Vector2f(pad.Position.X - 5, pad.Position.Y);
			}

		}

		public override object Clone()
		{
			return new PowerUpPadSizeInc(this);
		}
	}
}
