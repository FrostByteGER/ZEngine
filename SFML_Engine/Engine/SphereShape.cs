using SFML.System;

namespace SFML_Engine.Engine
{
    public class SphereShape : CollisionShape
    {
        
        public float SphereRadius { get; set; }

        public SphereShape()
        {
            SphereRadius = 1.0f;
        }

        public SphereShape(float sphereRadius)
        {
            SphereRadius = sphereRadius;
        }

		public Vector2f getMid(Vector2f actorPosition)
		{
			return new Vector2f(actorPosition.X + SphereRadius, actorPosition.Y + SphereRadius);
		}

    }
}