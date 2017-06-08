using System;
using System.Collections.Generic;
using SFML_Engine.Engine.Game;
using SFML_SpaceSEM.IO;

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