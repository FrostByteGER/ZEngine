using System;
using SFML.Graphics;

namespace SFML_Engine.Engine
{
	public class ActorComponent : Transformable, ITickable
	{
		public virtual void Tick(float deltaTime)
		{
			Console.WriteLine("Component Tick");
		}
	}
}