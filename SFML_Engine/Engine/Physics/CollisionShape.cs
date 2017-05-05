using System;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
    public class CollisionShape
    {
		public bool ShowCollisionShape { get; set; } = false;
	    public Color CollisionShapeColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));

		public virtual Vector2f CollisionBounds { get; set; }
	    public bool Movable { get; set; }

		public Vector2f Velocity { get; set; }

	    public Vector2f Acceleration { get; set; }
    }
}