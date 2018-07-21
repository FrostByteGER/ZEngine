using System.Collections.Generic;
using System.Linq;
using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.TileMap;

namespace SFML_TowerDefense.Source.Game.Units
{
	public class TDSpawner : TDFieldActor
	{

		public List<TDWave> Waves { get; set; }
		public TDWave ActiveWave { get; private set; }
		public int WaveIndex { get; private set; } = 0;
		public int ActiveWaveIndex { get; private set; } = 0;
		public TDWaypoint SpawnPoint { get; set; }
		public bool WaveActive { get; set; } = false;
		public bool SpawnerActive { get; set; } = true;
		public float Cooldown { get; set; } = 0;
		public float WaveCooldown { get; set; } = 0;

		public TDSpawner(Level level) : base(level)
		{
			Waves = new List<TDWave>();
			SetRootComponent(new ActorComponent());
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			ActiveWave = Waves[WaveIndex];
			SpawnPoint = TDLevelRef.GetTileByTileCoords(TilePosition).FieldActors.OfType<TDWaypoint>().FirstOrDefault();
			if (SpawnPoint == null) WaveActive = false;
		}

		public override void Tick(float deltaTime)
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
			var spawnedUnit = LevelReference.SpawnActor(ActiveWave.UnitTypes[ActiveWaveIndex]) as TDUnit;
			++ActiveWaveIndex;
			if (ActiveWaveIndex >= ActiveWave.Amount) WaveActive = false;
			if (spawnedUnit == null) return;
			spawnedUnit.CurrentWaypoint = SpawnPoint;
			spawnedUnit.Position = SpawnPoint.Position;
			spawnedUnit.CurrentWaypoint = SpawnPoint;
		}
	}
}