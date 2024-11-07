using SFML.System;

namespace ZEngine.Engine.Physics
{
    public interface IPhysicsProperties
    {
		float Mass { get; set; }
		bool HasGravity { get; set; }

		Vector2 Velocity { get; set; }
		Vector2 Acceleration { get; set; }
	}
}
