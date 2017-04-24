using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public class SphereShape : CollisionShape
    {

		public float SphereDiameter { get; set; } = 1f;

        public SphereShape()
        {
		}

        public SphereShape(float sphereRadius)
        {
            SphereDiameter = sphereRadius;
        }

		public Vector2f getMid(Vector2f actorPosition)
		{
			return new Vector2f(actorPosition.X + SphereDiameter/2f, actorPosition.Y + SphereDiameter/2f);
		}

    }
}