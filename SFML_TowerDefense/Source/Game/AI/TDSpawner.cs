using System.Collections.Generic;
using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.Units;

namespace SFML_TowerDefense.Source.Game.AI
{
	public class TDSpawner : TDFieldActor
	{

		public List<TDWave> Waves { get; set; }
		public TDWave ActiveWave { get; private set; }
		public TDWaypoint SpawnPoint { get; set; }
		public bool Active { get; private set; } = false;
		public float Cooldown { get; set; } = 0;

		public TDSpawner(Level level) : base(level)
		{
			Waves = new List<TDWave>();
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (Active)
			{
				if (Cooldown < 0)
				{
					ActiveWave.Amount--;
					if (ActiveWave.Amount == 0)
					{
						Active = false;
					}

					SpawnUnit();
					Cooldown = ActiveWave.SpawnSpeed;
				}
				Cooldown -= deltaTime;
			}
		}

		public bool SpawnNextWave()
		{
			if (Active || Waves.Count == 0) return false;
			Active = true;
			ActiveWave = Waves[0];
			Waves.RemoveAt(0);
			return true; ;
		}

		private void SpawnUnit()
		{
			var spawnedUnit = LevelReference.SpawnActor<TDUnit>();
			spawnedUnit.Position = SpawnPoint.Position;
			spawnedUnit.CurrentWaypoint = SpawnPoint;
		}
	}
}