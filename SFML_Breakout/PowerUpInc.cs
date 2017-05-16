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
	class PowerUpInc : PowerUp
	{

		public override void Apply()
		{
			base.Apply();

			SpriteActor pad = (SpriteActor)LevelReference.FindActorInLevel("Player Pad 1");
			if (EngineMath.EngineRandom.NextDouble() > 0.5)
			{
				((BoxShape)pad.CollisionShape).CollisionBounds = new SFML.System.Vector2f(((BoxShape)pad.CollisionShape).CollisionBounds.X + 10, ((BoxShape)pad.CollisionShape).CollisionBounds.Y);
			}
			else
			{
				((BoxShape)pad.CollisionShape).CollisionBounds = new SFML.System.Vector2f(((BoxShape)pad.CollisionShape).CollisionBounds.X - 10, ((BoxShape)pad.CollisionShape).CollisionBounds.Y);
			}
		}

	}
}
