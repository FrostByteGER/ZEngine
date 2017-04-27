using System;
using SFML_Engine.Engine;

namespace SFML_Pong
{
	public class PongGameLevel : Level
	{
		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
		}

		public override void OnLevelLoad()
		{
			Console.WriteLine("Pong Game Level #" + LevelID + " Loaded");
			OnGameStart();
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}