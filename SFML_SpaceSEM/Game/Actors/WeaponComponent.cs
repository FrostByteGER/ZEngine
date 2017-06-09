using System;
using SFML.Audio;
using SFML.Graphics;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_SpaceSEM.Game.Actors
{
	public class WeaponComponent : SpriteComponent
	{
		public uint Damage { get; set; } = 1;
		public float BulletSpread { get; set; } = 0.0f;
		public uint BulletsPerShot { get; set; } = 1;
		public float BulletSpeed { get; set; } = 400.0f;

		public float CooldownTime { get; set; } = 0.1f;

		public float CurrentCooldownTime { get; set; } = 0.0f;
		//public SpaceBullet Bullet { get; set; }
		public WeaponComponent(Sprite sprite) : base(sprite)
		{
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);

			if (CurrentCooldownTime > 0.0f)
			{
				var cooldown = CurrentCooldownTime - deltaTime;
				CurrentCooldownTime = cooldown.Clamp(0.0f, CooldownTime);
			}

		}

		public void OnFire()
		{
			if (CurrentCooldownTime > 0.0f) return;

			//var fireSound = new Sound(new SoundBuffer(AssetManager.AssetsPath + "SFX_Laser_01.ogg"));
			//fireSound.Volume = ParentActor.LevelReference.EngineReference.GlobalVolume;
			//fireSound.Play();

			for (int i = 0; i < BulletsPerShot; ++i)
			{
				var bullet = new SpaceBullet(new Sprite(new Texture(AssetManager.AssetsPath + "Bullet_01.png")), ParentActor.LevelReference);
				bullet.Instigator = ParentActor;
				var root = bullet.GetRootComponent<PhysicsComponent>();
				root.CanOverlap = true;
				root.CollisionResponseChannels &= ~ParentActor.GetRootComponent<PhysicsComponent>().CollisionType;
				bullet.CollisionCallbacksEnabled = true;
				bullet.Damage = Damage;
				bullet.Position = WorldPosition;
				bullet.Rotation = -BulletSpread + (float)EngineMath.EngineRandom.NextDouble() * (BulletSpread - -BulletSpread);
				root.MaxVelocity = new TVector2f(Math.Abs(BulletSpeed), Math.Abs(BulletSpeed));
				root.Velocity = bullet.Position.Up(bullet.Rotation) * BulletSpeed;
				ParentActor.LevelReference.SpawnActor(bullet);
			}
			CurrentCooldownTime = CooldownTime;
		}
	}
}