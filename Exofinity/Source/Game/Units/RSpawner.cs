using System.Collections.Generic;
using System.Linq;
using Exofinity.Source.Game.TileMap;
using ZEngine.Engine.Game;

namespace Exofinity.Source.Game.Units
{
	public class RSpawner : RFieldActor
	{

		public List<RWave> Waves { get; set; }
		public RWave ActiveWave { get; private set; }
		public int WaveIndex { get; private set; } = 0;
		public int ActiveWaveIndex { get; private set; } = 0;
		public RWaypoint SpawnPoint { get; set; }
		public bool WaveActive { get; set; } = false;
		public bool SpawnerActive { get; set; } = true;
		public float Cooldown { get; set; } = 0;
		public float WaveCooldown { get; set; } = 0;

		public RSpawner()
		{
			Waves = new List<RWave>();
			SetRootComponent(new ActorComponent());
		}

        protected override void OnGameStart()
		{
			base.OnGameStart();
			ActiveWave = Waves[WaveIndex];
			SpawnPoint = RLevelRef.GetTileByTileCoords(TilePosition).FieldActors.OfType<RWaypoint>().FirstOrDefault();
			if (SpawnPoint == null) WaveActive = false;
		}

        protected override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			
			if (SpawnerActive && WaveActive)
			{
				if (Cooldown <= 0)
				{
					SpawnUnit();
					--ActiveWave.AmountLeft;
					Cooldown = ActiveWave.SpawnSpeed;

				}
				else
				{
					Cooldown -= deltaTime;
				}
			}
		}

		public bool SpawnNextWave()
		{
			if (!SpawnerActive || WaveActive || Waves.Count == 0) return false;
			WaveActive = true;
			ActiveWave = Waves[WaveIndex];
			++WaveIndex;
			ActiveWaveIndex = 0;
			ActiveWave.AmountLeft = ActiveWave.Amount;
			Cooldown = ActiveWave.SpawnSpeed;
			return true;
		}

		private void SpawnUnit()
		{
			var spawnedUnit = LevelReference.SpawnActor(ActiveWave.UnitTypes[ActiveWaveIndex]) as RUnit;
			++ActiveWaveIndex;
			if (ActiveWaveIndex >= ActiveWave.Amount) WaveActive = false;
			if (spawnedUnit == null) return;
			spawnedUnit.CurrentWaypoint = SpawnPoint;
			spawnedUnit.Position = SpawnPoint.Position;
			spawnedUnit.CurrentWaypoint = SpawnPoint;
		}
	}
}