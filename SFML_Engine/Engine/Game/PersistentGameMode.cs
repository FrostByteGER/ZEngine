namespace SFML_Engine.Engine.Game
{
	public class PersistentGameMode : GameMode
	{
		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
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