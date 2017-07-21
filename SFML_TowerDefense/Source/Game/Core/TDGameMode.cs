using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Audio;
using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.Player;
using SFML_TowerDefense.Source.Game.Units;

namespace SFML_TowerDefense.Source.Game.Core
{
	public class TDGameMode : GameMode
	{

		public TDPlayerController Player { get; set; }
		public uint PlayerGold
		{
			get => Player.Gold;
			set => Player.Gold = value;
		}

		public uint PlayerScore
		{
			get => Player.Score;
			set => Player.Score = value;
		}

		public uint CurrentWave { get; set; } = 0;
		public uint WaveCount { get; set; } = 2;
		public float WaveCountdown { get; set; } = 5.0f;
		public float WaveCountdownCurrent { get; set; } = 5.0f;
		public uint EnemiesLeftInCurrentWave { get; set; } = 0;
		public uint PlayerHealth => Player.Health;

		public TDGameModeState GameState { get; set; } = TDGameModeState.CountingDown;
		public List<TDSpawner> Spawners { get; set; } = new List<TDSpawner>();
		
		
		// Sound Effects
		public Sound BuildingCancelled { get; private set; }
		public Sound CannotDeployHere { get; private set; }
		public Sound ConstructionComplete { get; private set; }
		public Sound NexusLost { get; private set; }
		public Sound NexusUnderAttack { get; private set; }
		public Sound EnemiesDetected { get; private set; }
		public Sound InsufficientFunds { get; private set; }
		public Sound MissionAccomplished { get; private set; }
		public Sound MissionFailed { get; private set; }
		

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			switch (GameState)
			{
				case TDGameModeState.NotStarted:
					break;
				case TDGameModeState.CountingDown:
					WaveCountdownCurrent -= deltaTime;
					if (WaveCountdownCurrent <= 0.0f)
					{
						WaveCountdownCurrent = 0.0f;
						GameState = TDGameModeState.WaveStarted;
						++CurrentWave;
						foreach (var spawner in Spawners)
						{
							spawner.SpawnNextWave();
							EnemiesLeftInCurrentWave += spawner.ActiveWave.Amount;
						}
					}
					break;
				case TDGameModeState.WaveStarted:
					if (EnemiesLeftInCurrentWave == 0)
					{
						if (CurrentWave >= WaveCount)
						{
							foreach (var spawner in Spawners)
							{
								spawner.WaveActive = false;
							}
							GameState = TDGameModeState.GameEnded;
							break;
						}
						GameState = TDGameModeState.CountingDown;
						WaveCountdownCurrent = WaveCountdown;
					}
					break;
				case TDGameModeState.GameEnded:
					break;
				default:
					break;
			}
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			Player = LevelReference.FindPlayer<TDPlayerController>(0);
			var assetManager = LevelReference.EngineReference.AssetManager;
			BuildingCancelled = assetManager.LoadSound("BuildingCancelled");
			CannotDeployHere = assetManager.LoadSound("CannotDeployHere");
			ConstructionComplete = assetManager.LoadSound("ConstructionComplete");
			NexusLost = assetManager.LoadSound("NexusLost");
			NexusUnderAttack = assetManager.LoadSound("NexusUnderAttack");
			EnemiesDetected = assetManager.LoadSound("EnemiesDetected");
			InsufficientFunds = assetManager.LoadSound("InsufficientFunds");
			MissionAccomplished = assetManager.LoadSound("MissionAccomplished");
			MissionFailed = assetManager.LoadSound("MissionFailed");
			
			BuildingCancelled.Volume = LevelReference.EngineReference.GlobalSoundVolume;
			CannotDeployHere.Volume = LevelReference.EngineReference.GlobalSoundVolume;
			ConstructionComplete.Volume = LevelReference.EngineReference.GlobalSoundVolume;
			NexusLost.Volume = LevelReference.EngineReference.GlobalSoundVolume;
			NexusUnderAttack.Volume = LevelReference.EngineReference.GlobalSoundVolume;
			EnemiesDetected.Volume = LevelReference.EngineReference.GlobalSoundVolume;
			InsufficientFunds.Volume = LevelReference.EngineReference.GlobalSoundVolume;
			MissionAccomplished.Volume = LevelReference.EngineReference.GlobalSoundVolume;
			MissionFailed.Volume = LevelReference.EngineReference.GlobalSoundVolume;

			Spawners = LevelReference.FindActorsInLevel<TDSpawner>().ToList();
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

	public enum TDGameModeState
	{
		NotStarted,
		CountingDown,
		WaveStarted,
		GameEnded
	}
}