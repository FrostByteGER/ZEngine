using System;

namespace SFML_Engine.Engine.Game
{
    public class GameMode : ITickable, IGameInterface
    {

		public Level LevelReference { get; set; }
        public virtual void Tick(float deltaTime)
        {
	        //Console.WriteLine("GameMode Tick");
        }

	    public bool CanTick { get; set; } = true;

	    public virtual void OnGameStart()
	    {
			Console.WriteLine("Game Started!");
		}

	    public virtual void OnGamePause()
	    {
		    CanTick = false;
	    }

	    public virtual void OnGameResume()
	    {
		    CanTick = true;
	    }

	    public virtual void OnGameEnd()
	    {
			Console.WriteLine("Game Ended!");
		}
    }
}