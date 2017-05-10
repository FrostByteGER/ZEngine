using System;

namespace SFML_Engine.Engine
{
	public interface IDestroyable : IDisposable
	{
		void Destroy(bool disposing);
	}
}