using System;
using System.Collections.Generic;
using SFML_Engine.Engine.Game;
using SFML_Roguelike.Source.Game.Core;

namespace SFML_Roguelike.Source.Game.Units
{
	public class RWave : RActor
	{
		public float SpawnSpeed { get; set; } = 1;
		public uint Amount { get; set; } = 1;
		public uint AmountLeft { get; set; } = 1;
		public List<Type> UnitTypes { get; set; }

		public RWave(Level level) : base(level)
		{
			
		}
		
	}
}