using SFML_Engine.Engine;

namespace SFML_SpaceSEM
{
	public class SpaceSEMGameMode : GameMode
	{

		public SpaceSEMPlayerController Player { get; set; }

		public bool GameEnded { get; set; } = false;
		public bool GameWon { get; set; } = false;
		public bool GameOver { get; set; } = false;

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			GameEnded = false;
			GameWon = false;
			GameOver = false;
			Player = (SpaceSEMPlayerController) LevelReference.FindPlayer(0);
		}

		public void LoadNextLevel()
		{

		}

		public override void OnGameEnd()
		{

		}
	}
}
