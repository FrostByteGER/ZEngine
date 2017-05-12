using System;

namespace SFML_Engine.Engine.Physics
{
	[Flags]
	public enum CollisionTypes : short
	{
		All = -1,
		None = 0,
		Default = 1,
		Static = 2,
		Kinematic = 4,
		Dynamic = Default
	}
}