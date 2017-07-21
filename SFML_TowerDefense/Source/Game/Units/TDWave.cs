using System;
using System.Collections.Generic;
using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.Core;

namespace SFML_TowerDefense.Source.Game.Units
{
	public class TDWave : TDActor
	{
		public float SpawnSpeed { get; set; } = 1;
		public uint Amount { get; set; } = 1;
		public uint AmountLeft { get; set; } = 1;
		public List<Type> UnitTypes { get; set; }

		public TDWave(Level level) : base(level)
		{
			
		}
		
	}
}