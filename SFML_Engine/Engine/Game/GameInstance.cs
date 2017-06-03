namespace SFML_Engine.Engine.Game
{
	public class GameInstance : ITickable, IGameInterface
	{

		public bool CanTick { get; set; } = true;

		public virtual void Tick(float deltaTime)
		{

		}

		public virtual void OnGameStart()
		{
		}

		public virtual void OnGamePause()
		{
		}

		public virtual void OnGameResume()
		{
		}

		public virtual void OnGameEnd()
		{
		}
	}
}