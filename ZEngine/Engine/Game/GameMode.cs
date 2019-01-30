using System;

namespace ZEngine.Engine.Game
{
    public class GameMode : ITickable
    {

		public Level LevelReference { get; set; }
        protected internal virtual void Tick(float deltaTime)
        {
	        //Console.WriteLine("GameMode Tick");
        }

	    public bool CanTick { get; set; } = true;

	    protected internal virtual void OnGameStart()
	    {
			Console.WriteLine("Game Started!");
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
			Console.WriteLine("Game Ended!");
		}
    }
}