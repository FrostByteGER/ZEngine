using System;

namespace SFML.Graphics.Engine
{
	public class ActorComponent : Transformable, ITickable
	{
		public virtual void Tick(float deltaTime)
		{
			Console.WriteLine("Component Tick");
		}
	}
}