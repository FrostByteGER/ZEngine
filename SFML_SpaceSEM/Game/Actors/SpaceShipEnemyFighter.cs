using System;
using SFML.Audio;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;
using System;

namespace SFML_SpaceSEM.Game.Actors
{
	class SpaceShipEnemyFighter : SpaceShipEnemy
	{

		private float timer = 0.0f;

		public SpaceShipEnemyFighter(Sprite sprite, Level level) : base(sprite, level)
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
			if(Position.X < 400) FireWeapons();

			//FireWeapons();
			if (Position.X + ActorBounds.X > 400)
			{
				Velocity += new TVector2f(-100, 0);
			}
			else if(Position.X - ActorBounds.X < -400)
			{
				Velocity += new TVector2f(100, 0);
			}

			if (Position.Y + ActorBounds.Y > 250)
			{
				Velocity += new TVector2f(0, -100);
				Console.WriteLine(Position);

			}
			else if (Position.Y - ActorBounds.Y < -400)
			{
				Velocity += new TVector2f(0, 100);
				
			}

			if (timer > 90)
			{
				timer -= 90;
			}
			else
			{
				timer += deltaTime;
			//if (Position.X + ActorBounds.X / 2f > 400)
			{
				//Velocity = new TVector2f(-Velocity.X, -Velocity.Y);
				//Console.WriteLine(Position.X);
			}
			if(Position.X - ActorBounds.X/2f < -400)
			{
				Velocity = new TVector2f(-Velocity.X, -Velocity.Y);
				Console.WriteLine(Position.X);
			}
		}

		public override void OnDeath()
		{

			var killSound = new Sound(new SoundBuffer(AssetManager.AssetsPath + "SFX_Explosion_01.ogg"));
			killSound.Volume = LevelReference.EngineReference.GlobalVolume;
			killSound.Play();
			LevelReference.DestroyActor(this);
		}

		public override void FireWeapons()
		{
			base.FireWeapons();
		}
	}
}