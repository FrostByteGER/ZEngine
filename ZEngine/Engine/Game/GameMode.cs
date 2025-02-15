using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Game
{
    public class GameMode : ITickable
    {
        public virtual void Tick(float deltaTime)
        {
        }

	    public bool CanTick { get; set; } = true;

	    protected internal virtual void OnGameStart()
	    {
			Log.Info("Game Started!", DebugLogCategories.Engine);
		}

        protected internal virtual void OnGamePause()
	    {
		    CanTick = false;
	    }

        protected internal virtual void OnGameResume()
	    {
		    CanTick = true;
	    }

        protected internal virtual void OnGameEnd()
	    {
            Log.Info("Game Ended!", DebugLogCategories.Engine);
		}
    }
}