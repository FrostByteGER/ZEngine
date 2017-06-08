using System;
using System.Timers;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_SpaceSEM
{
	public class SpaceSEMEnemy : SpriteActor
	{

		public uint Hitpoints { get; set; } = 3;
		public uint MaxHitpoints { get; set; } = 3;
		public bool Invincible { get; set; } = false;
		public uint ScorePoints{ get; set; } = 10;

		public Timer RespawnTimer { get; set; } = new Timer();

		public SpaceSEMEnemy(Sprite sprite, Level level) : base(sprite, level)
		{
		}

		
		public void OnHit(Actor actor)
		{
			if (actor is SpaceSEMPlayer || actor is SpaceSEMBullet)
			{
				var hitSound = new Sound(SpaceSEMPersistentGameMode.BlockHitBuffer);
				hitSound.Volume = LevelReference.EngineReference.GlobalVolume;
				hitSound.Play();


				if (actor is SpaceSEMBullet)
				{
					LevelReference.EngineReference.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, actor)));
					(--Hitpoints).Clamp<uint>(0, MaxHitpoints);
				}

				((SpaceSEMGameMode)LevelReference.GameMode).Player.Score += ScorePoints;
				((SpaceSEMGameLevel)LevelReference).UpdateHighscoreText(((SpaceSEMGameMode)LevelReference.GameMode).Player.Score);


				if (Hitpoints <= 0)
				{
					LevelReference.EngineReference.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, this)));
					RespawnTimer.Elapsed += OnDeathRespawn;
					RespawnTimer.AutoReset = false;
					RespawnTimer.Interval = 1000;
					RespawnTimer.Enabled = true;
				}
			}
		}

		public override void AfterCollision(Actor actor)
		{
			OnHit(actor);
		}

		public override void BeforeCollision(Actor actor)
		{
		}

		public override void IsOverlapping(Actor actor)
		{
			OnHit(actor);
		}

		private void OnDeathRespawn(object source, ElapsedEventArgs e)
		{
			var enemyShip = new SpaceSEMEnemy(SpaceSEMPersistentGameMode.EnemyTexture);
			enemyShip.ActorName = "Enemy Ship 1";
			enemyShip.CollisionShape = new BoxShape(SpaceSEMPersistentGameMode.EnemyTexture.Size.X, SpaceSEMPersistentGameMode.EnemyTexture.Size.Y);
			enemyShip.Position = new Vector2f(LevelReference.EngineReference.EngineWindowWidth / 2.0f - enemyShip.CollisionShape.CollisionBounds.X / 2.0f + EngineMath.EngineRandom.Next(-200,200), enemyShip.CollisionShape.CollisionBounds.Y + 5.0f);
			enemyShip.CollisionShape.ShowCollisionShape = true;
			enemyShip.CollisionShape.Position = enemyShip.Position;
			enemyShip.Friction = 0.01f;

			LevelReference.EngineReference.PhysicsEngine.AddActorToGroup("Enemies", enemyShip);

			((SpaceSEMGameLevel)LevelReference).Enemies.Add(enemyShip);
			LevelReference.EngineReference.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, enemyShip, LevelReference.LevelID)));

		}
	}
}