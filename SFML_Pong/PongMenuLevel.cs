using System;
using SFML_Engine.Engine;

namespace SFML_Pong
{
	public class PongMenuLevel : Level
	{
		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
		}

		public override void OnLevelLoad()
		{
			Console.WriteLine("Pong Menu Level #" + LevelID + " Loaded");
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