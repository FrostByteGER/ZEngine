using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Dynamics;

namespace SFML_SpaceSEM.Game.Actors.Enemies
{
	public abstract class SpaceShipEnemy : SpaceShipActor
	{

		public uint Score { get; set; } = 10;

		protected SpaceShipEnemy(Sprite sprite, Level level) : base(sprite, level)
		{
			CollisionCallbacksEnabled = true;
		}

		public virtual void OnDeath()
		{
			--(LevelReference.GameMode as SpaceGameMode).EnemiesRemaining;
		}

		public override void OnCollide(Fixture self, Fixture other, Contact contactInfo)
		{
			
			var otherComp = other.Body.UserData as ActorComponent;
			if (otherComp?.ParentActor.ActorName == "Bottom Border") OnDeath();
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