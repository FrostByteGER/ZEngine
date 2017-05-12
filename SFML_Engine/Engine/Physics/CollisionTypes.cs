using System;
using System.Collections;
using BulletSharp;

namespace SFML_Engine.Engine.Physics
{
	[Flags]
	public enum CollisionTypes
	{
		AllFilter = -1,
		None = 0,
		DefaultFilter = 1,
		StaticFilter = 2,
		KinematicFilter = 4
	}
}