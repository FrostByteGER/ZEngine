using System;
using SFML_Engine.Engine;

namespace SFML_Pong
{
	public class PongGameMode : GameMode
	{

		public uint PlayerOneScore { get; set; } = 0;
		public uint PlayerTwoScore { get; set; } = 0;
		public uint WinScore { get; set; } = 3;

		public override void StartGame()
		{
			base.StartGame();
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (PlayerOneScore == WinScore ^ PlayerTwoScore == WinScore)
			{
				EndGame();
			}
		}

		public override void EndGame()
		{
			base.EndGame();
		}
	}
}