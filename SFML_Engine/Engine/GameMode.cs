using System;
using SFML.Graphics;

namespace SFML_Engine.Engine
{
    public class GameMode : ITickable
    {

	    public virtual void StartGame()
	    {
			Console.WriteLine("Game Started!");
		}
        public virtual void Tick(float deltaTime)
        {
	        //Console.WriteLine("GameMode Tick");
        }

	    public virtual void EndGame()
	    {
			Console.WriteLine("Game Ended!");
		}
    }
}