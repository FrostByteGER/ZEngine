using System;
using SFML_Engine.Engine;

namespace SFML_Pong
{
	public class PongGameMode : GameMode
	{

		public PongPlayerController Player1 { get; set; }
		public PongPlayerController Player2 { get; set; }
		public uint WinScore { get; set; } = 3;

		public override void OnGameStart()
		{
			base.OnGameStart();
			Player1 = LevelReference.Engine.Players[0] as PongPlayerController;
			Player2 = LevelReference.Engine.Players[1] as PongPlayerController;
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (Player1.Score == WinScore ^ Player2.Score == WinScore)
			{
				Console.WriteLine("GAME OVER!");
			}
		}

	}
}