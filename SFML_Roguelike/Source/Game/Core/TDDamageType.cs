using System;

namespace SFML_TowerDefense.Source.Game.Core
{
	[Flags]
	public enum TDDamageType
	{
		None = 0,
		Normal = 1,
		Laser = 2,
		Plasma = 4,
		Fire = 8,
		Ice = 16,
		Shock = 32,
		Explosive = 64,
		Kinetic = 128
	}
}