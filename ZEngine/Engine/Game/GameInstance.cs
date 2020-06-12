namespace ZEngine.Engine.Game
{
	public class GameInstance : ITickable
	{

		public bool CanTick { get; set; } = true;

		public virtual void Tick(float deltaTime)
		{

		}

        protected internal virtual void OnGameStart()
		{
		}

        protected internal virtual void OnGamePause()
		{
		}

        protected internal virtual void OnGameResume()
		{
		}

        protected internal virtual void OnGameEnd()
		{
		}
	}
}