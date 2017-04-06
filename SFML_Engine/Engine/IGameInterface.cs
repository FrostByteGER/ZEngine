using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine
{
	public interface IGameInterface
	{

		void OnGameStart();
		void OnGamePause();
		void OnGameEnd();
		
	}
}
