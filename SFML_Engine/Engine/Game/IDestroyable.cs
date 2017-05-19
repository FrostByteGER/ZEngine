using System;

namespace SFML_Engine.Engine.Game
{
	public interface IDestroyable : IDisposable
	{
		void Destroy(bool disposing);
	}
}