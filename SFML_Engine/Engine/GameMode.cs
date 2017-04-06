using System;
using SFML.Graphics;

namespace SFML_Engine.Engine
{
    public class GameMode : ITickable, IGameInterface
    {

		public Level LevelReference { get; set; }
        public virtual void Tick(float deltaTime)
        {
	        //Console.WriteLine("GameMode Tick");
        }

	    public virtual void OnGameStart()
	    {
			Console.WriteLine("Game Started!");
		}

	    public virtual void OnGamePause()
	    {
		    throw new NotImplementedException();
	    }

	    public virtual void OnGameEnd()
	    {
			Console.WriteLine("Game Ended!");
		}
    }
}