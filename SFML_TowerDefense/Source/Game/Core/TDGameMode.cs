using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.Player;

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
		public uint WaveCount { get; set; } = 1;
		public uint EnemiesLeftInCurrentWave { get; set; } = 0;
		public uint PlayerHealth => Player.Health;

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			Player = LevelReference.FindPlayer<TDPlayerController>(0);
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