using SFML_Engine.Engine;
using SFML_Engine.Engine.Utility;
using System;
using System.Collections.Generic;

namespace SFML_Breakout
{
	public class BreakoutGameMode : GameMode
	{
		private List<PowerUp> PowerUps { get; set; } = new List<PowerUp>();

		public BreakoutPlayerController Player { get; set; }

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			Player = (BreakoutPlayerController) LevelReference.FindPlayer(0);
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
