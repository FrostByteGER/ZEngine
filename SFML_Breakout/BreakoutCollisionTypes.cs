using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Breakout
{
	[Flags]
	public enum BreakoutCollisionTypes : short
	{
		All = -1,
		None = 0,
		Default = 1,
		Static = 2,
		Kinematic = 4,
		Dynamic = Default,
		Borders = 8,
		Balls = 16,
		Pads = 32,
		PowerUps = 64,
		Blocks = 128
	}
}
