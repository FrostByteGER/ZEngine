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
		public float FireRate { get; set; } = 1.0f;
		public float BulletSpread { get; set; } = 0.0f;
		public uint BulletsPerShot { get; set; } = 1;
		public float BulletSpeed { get; set; } = 5.0f;
		//public SpaceBullet Bullet { get; set; }
		public WeaponComponent(Sprite sprite) : base(sprite)
		{
		}

		public void OnFire()
		{
			for (int i = 0; i < BulletsPerShot; ++i)
			{
				var bullet = new SpaceBullet(new Sprite(new Texture(AssetManager.AssetsPath + "Bullet_01.png")), ParentActor.LevelReference);
				bullet.GetRootComponent<PhysicsComponent>().CollisionResponseChannels &=
					~ParentActor.GetRootComponent<PhysicsComponent>().CollisionType;
				bullet.Damage = Damage;
				bullet.Position = WorldPosition;
				bullet.Rotation = -BulletsPerShot + (float)EngineMath.EngineRandom.NextDouble() * (BulletsPerShot - -BulletsPerShot);
				bullet.GetRootComponent<PhysicsComponent>().CollisionBody.ApplyLinearImpulse(new TVector2f(0.0f, VelcroPhysicsEngine.ToPhysicsUnits(-BulletSpeed)));
				ParentActor.LevelReference.SpawnActor(bullet);
			}
		}
	}
}