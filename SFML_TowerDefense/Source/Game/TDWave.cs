using SFML_Engine.Engine.Game;
using System.Collections.Generic;

namespace SFML_TowerDefense.Source.Game
{
	public class TDWave : TDActor
	{

		public List<TDUnit> Units { get; set; }

		public TDWave(Level level) : base(level)
		{
			Units = new List<TDUnit>();
		}
	}
}