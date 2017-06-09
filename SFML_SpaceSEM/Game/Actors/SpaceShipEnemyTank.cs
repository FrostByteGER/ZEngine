using System;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML.Audio;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;

namespace SFML_SpaceSEM.Game.Actors
{
	public class SpaceShipEnemyTank : SpaceShipEnemy
	{
		public SpaceShipEnemyTank(Sprite sprite, Level level) : base(sprite, level)
		{
			Healthpoints = MaxHealthpoints;
			var weapon1 = new WeaponComponent(new Sprite());
			weapon1.BulletSpeed = weapon1.BulletSpeed * -1;
			weapon1.LocalPosition += new TVector2f(0.0f, 30.0f);
			AddComponent(weapon1);
			WeaponSystems.Add(weapon1);

		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);

			if (Position.X + ActorBounds.X > 400)
			{
				Velocity += new TVector2f(-50, 0);
			}
			else if (Position.X - ActorBounds.X < -400)
			{
				Velocity += new TVector2f(50, 0);
			}

			if (Position.Y + ActorBounds.Y > 250)
			{
				Velocity += new TVector2f(0, -50);

			}
			else if (Position.Y - ActorBounds.Y < -400)
			{
				Velocity += new TVector2f(0, 50);
				Console.WriteLine(Position);
			}
		}

		public override void OnDeath()
		{
			var killSound = new Sound(new SoundBuffer(AssetManager.AssetsPath + "SFX_Explosion_01.ogg"));
			killSound.Volume = LevelReference.EngineReference.GlobalVolume;
			killSound.Play();
			LevelReference.DestroyActor(this);
		}
	}
}
