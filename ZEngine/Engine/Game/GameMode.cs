using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Game
{
    public class GameMode : ITickable
    {
        public virtual void Tick(float deltaTime)
        {
	        //Debug.LogDebug("GameMode Tick", DebugLogCategories.Engine);
        }

	    public bool CanTick { get; set; } = true;

	    protected internal virtual void OnGameStart()
	    {
			Debug.Log("Game Started!", DebugLogCategories.Engine);
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
            Debug.Log("Game Ended!", DebugLogCategories.Engine);
		}
    }
}