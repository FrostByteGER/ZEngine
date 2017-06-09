using System;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Dynamics;

namespace SFML_SpaceSEM.Game.Actors
{
	public abstract class SpaceShipEnemy : SpaceShipActor
	{

		public uint Score { get; set; } = 10;

		protected SpaceShipEnemy(Sprite sprite, Level level) : base(sprite, level)
		{
			CollisionCallbacksEnabled = true;
		}

		public abstract void OnDeath();

		public override void OnCollide(Fixture self, Fixture other, Contact contactInfo)
		{
			var otherComp = other.Body.UserData as ActorComponent;
			var otherActor = otherComp?.ParentActor as SpaceBullet;
			if (otherActor != null)
			{
				var hp = Healthpoints - otherActor.Damage;
				Healthpoints = hp.Clamp<uint>(0, MaxHealthpoints);
				if (Healthpoints <= 0)
				{
					var spaceShipPlayer = otherActor.Instigator as SpaceShipPlayer;
					if (spaceShipPlayer != null)
					{
						spaceShipPlayer.ControllerRef.Score += Score;
					}
					OnDeath();
				}

			}
		}
	}
}