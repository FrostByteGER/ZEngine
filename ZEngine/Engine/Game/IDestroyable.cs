using System;

namespace ZEngine.Engine.Game
{
	public interface IDestroyable : IDisposable
	{
		void Destroy(bool disposing);
	}
}