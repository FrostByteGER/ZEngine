using SFML_Engine.Engine;
using SFML_Engine.Engine.Utility;
using System;
using System.Collections.Generic;
using SFML_Engine.Engine.Events;

namespace SFML_Breakout
{
	public class BreakoutGameMode : GameMode
	{
		public List<PowerUp> PowerUps { get; set; } = new List<PowerUp>();

		public List<Block> Blocks { get; set; } = null;

		public List<BreakoutBall> Balls { get; set; } = new List<BreakoutBall>();

		public BreakoutPlayerController Player { get; set; }

		public bool GameEnded { get; set; } = false;
		public bool GameWon { get; set; } = false;
		public bool GameOver { get; set; } = false;

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (Blocks.Count <= 0)
			{
				GameWon = true;
				OnGameEnd();
			}

			if (Balls.Count <= 0)
			{
				GameOver = true;
				OnGameEnd();
			}
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			GameEnded = false;
			GameWon = false;
			GameOver = false;
			Player = (BreakoutPlayerController) LevelReference.FindPlayer(0);
			var gameMode = (BreakoutPersistentGameMode)LevelReference.EngineReference.PersistentGameMode;
			if (gameMode.CurrentLevel == gameMode.MaxLevels)
			{
				BreakoutPersistentGameMode.BGM_MLG_Current = BreakoutPersistentGameMode.BGM_MLG_Final;
			}
			BreakoutPersistentGameMode.SwitchMusic();
		}

		public void LoadNextLevel()
		{
			var gameMode = (BreakoutPersistentGameMode)LevelReference.EngineReference.PersistentGameMode;
			if (gameMode.CurrentLevel < gameMode.MaxLevels)
			{
				++gameMode.CurrentLevel;
				LevelReference.EngineReference.RegisterEvent(new SwitchLevelEvent<SwitchLevelParams>(new SwitchLevelParams(this,LevelReference.EngineReference.Levels[(int)gameMode.CurrentLevel])));
			}
			else
			{
				gameMode.CurrentLevel = 0;
				LevelReference.EngineReference.RegisterEvent(new SwitchLevelEvent<SwitchLevelParams>(new SwitchLevelParams(this, LevelReference.EngineReference.Levels[0])));
			}
		}

		public override void OnGameEnd()
		{
			if (GameEnded) return;
			BreakoutPersistentGameMode.BGM_Main.Stop();
			var gameMode = (BreakoutPersistentGameMode)LevelReference.EngineReference.PersistentGameMode;

			BreakoutPersistentGameMode.BGM_MLG_Current = gameMode.HighScore > gameMode.SecretThreshold ? BreakoutPersistentGameMode.BGM_MLG_Secret : BreakoutPersistentGameMode.BGM_MLG_Main;
			base.OnGameEnd();
			if (GameWon)
			{
				gameMode.HighScore += Player.Score;
				if (gameMode.HighScore > gameMode.AlltimeHighScore)
				{
					gameMode.AlltimeHighScore = gameMode.HighScore;
				}
				if (gameMode.CurrentLevel == gameMode.MaxLevels)
				{
					gameMode.HighScore = 0;
				}

				Console.WriteLine("NEW HIGHSCORE: " + gameMode.HighScore);
				LoadNextLevel();

			}else if (GameOver)
			{
				Console.WriteLine("GAME OVER!");
				gameMode.HighScore += Player.Score;
				if (gameMode.HighScore > gameMode.AlltimeHighScore)
				{
					gameMode.AlltimeHighScore = gameMode.HighScore;
				}
				gameMode.HighScore = 0;
				LevelReference.EngineReference.RegisterEvent(new SwitchLevelEvent<SwitchLevelParams>(new SwitchLevelParams(this, LevelReference.EngineReference.Levels[0])));
			}
			GameEnded = true;
		}

		public void AddPowerUp(PowerUp pu)
		{
			if (!PowerUps.Contains(pu))
			{
				PowerUps.Add(pu);
			}
		}

		public void SubPowerUp(PowerUp pu)
		{
			if (PowerUps.Contains(pu))
			{
				PowerUps.Remove(pu);
			}
		}

		public PowerUp GetRandomPowerUp()
		{
			if (PowerUps.Count == 0)
			{
				return null;
			}

			return (PowerUp)PowerUps[(int)(EngineMath.EngineRandom.NextDouble() * PowerUps.Count)].Clone();
		}
	}
}
