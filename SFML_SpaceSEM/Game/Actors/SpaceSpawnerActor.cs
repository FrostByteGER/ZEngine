using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;
using SFML_SpaceSEM.IO;
using VelcroPhysics.Collision.Filtering;

namespace SFML_SpaceSEM.Game.Actors
{
	public class SpaceSpawnerActor : Actor
	{
		public float ActivationTime { get; set; } = 1.0f;
		public List<SpaceLevelShipDataWrapper> Ships { get; set; } = new List<SpaceLevelShipDataWrapper>();

		public SpaceSpawnerActor(Level level) : base(level)
		{
			SetRootComponent(new ActorComponent());
		}

		internal void OnActivate(float activationTime)
		{
			// Spawn Ships here.
			Console.WriteLine("SPAWNER ACTIVATED!");
			foreach (var ship in Ships)
			{
				if (ship.ShipType == typeof(SpaceShipEnemyFighter))
				{
					var spawned = new SpaceShipEnemyFighter(new Sprite(new Texture(AssetManager.AssetsPath + "Enemy_01.png")), LevelReference);
					var spawnedRoot = spawned.GetRootComponent<PhysicsComponent>();
					spawnedRoot.CollisionType = Category.Cat3;
					spawnedRoot.CollisionResponseChannels &= ~Category.Cat2;
					spawnedRoot.CollisionResponseChannels &= ~spawnedRoot.CollisionType;
					spawned.ActorName = "Enemy Fighter";
					spawned.Position = ship.Position;
					spawned.Velocity = ship.Velocity;
					foreach (var weapon in spawned.WeaponSystems)
					{
						weapon.BulletSpeed = ship.BulletSpeed * -1;
						weapon.BulletsPerShot = ship.BulletsPerShot;
						weapon.BulletSpread = ship.BulletSpread;
						weapon.Damage = ship.BulletDamage;
						weapon.CooldownTime = ship.CooldownTime;
					}
					LevelReference.SpawnActor(this, spawned);
				}
			}
		}

		protected override void InitializeActor()
		{
			base.InitializeActor();
		}

		protected override void InitializeComponents()
		{
			base.InitializeComponents();
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (((SpaceGameLevel) LevelReference).LevelTime >= ActivationTime)
			{
				OnActivate(((SpaceGameLevel) LevelReference).LevelTime);
				((SpaceGameLevel) LevelReference).Spawners.Remove(this);
				LevelReference.DestroyActor(this);
			}
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameResume()
		{
			base.OnGameResume();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}