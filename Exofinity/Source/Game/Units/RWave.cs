using System;
using System.Collections.Generic;
using Exofinity.Source.Game.Core;
using ZEngine.Engine.Game;

namespace Exofinity.Source.Game.Units
{
	public class RWave : RActor
	{
		public float SpawnSpeed { get; set; } = 1;
		public uint Amount { get; set; } = 1;
		public uint AmountLeft { get; set; } = 1;
		public List<Type> UnitTypes { get; set; }

		public RWave()
		{
			
		}
		
	}
}