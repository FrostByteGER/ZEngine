using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_Breakout
{
	class PowerUpBullets : PowerUp
	{
		public PowerUpBullets()
		{
			CollisionShape.CollisionShapeColor = new SFML.Graphics.Color(255, 255, 0);
		}

		public PowerUpBullets(PowerUpBullets p)
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

			for (int i = 0; i < 3; i++)
			{
				Bullet bullet = new Bullet();

				bullet.ActorName = "Bullet";
				bullet.CollisionShape = new SphereShape(5f);
				bullet.CollisionShape.CollisionShapeColor = new Color(255, 255, 0);
				bullet.CollisionShape.ShowCollisionShape = true;
				bullet.Position = box.GetMid(pad.Position);
				bullet.Velocity = new Vector2f((float) (-150f+(EngineMath.EngineRandom.NextDouble()*300)) , -150f);
				LevelReference.EngineReference.PhysicsEngine.AddActorToGroup("Bullets", bullet);

				LevelReference.EngineReference.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, bullet, LevelReference.LevelID)));
			}
		}

		public override object Clone()
		{
			return new PowerUpBullets(this);
		}
	}
}
