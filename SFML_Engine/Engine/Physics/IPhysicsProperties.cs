using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public interface IPhysicsProperties
    {
		float Mass { get; set; }
		bool HasGravity { get; set; }

		Vector2f Velocity { get; set; }
		Vector2f Acceleration { get; set; }
	}
}
