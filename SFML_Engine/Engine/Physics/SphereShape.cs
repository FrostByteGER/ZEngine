using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public class SphereShape : CollisionShape
    {
		/// <summary>
		/// Collision Bounds of this collision shape. X axis is the Sphere Diameter. Y Axis is not used.
		/// </summary>
	    public override Vector2f CollisionBounds { get; set; }

	    public SphereShape()
        {
		}

        public SphereShape(float sphereDiameter)
        {
			CollisionBounds = new Vector2f(sphereDiameter, 0.0f);
        }

		public Vector2f GetMid(Vector2f actorPosition)
		{
			return new Vector2f(actorPosition.X + CollisionBounds.X/ 2f, actorPosition.Y + CollisionBounds.X/ 2f);
		}

	    public override void ScaleActor(float x, float y)
	    {
		    base.ScaleActor(x, y);
		    CollisionBounds = new Vector2f(CollisionBounds.X * Scale.X, 0.0f);
	    }

	    public override void ScaleActor(Vector2f scale)
	    {
		    base.ScaleActor(scale);
			CollisionBounds = new Vector2f(CollisionBounds.X * Scale.X, 0.0f);
		}

	    public override void ScaleAbsolute(float x, float y)
	    {
		    base.ScaleAbsolute(x, y);
			CollisionBounds = new Vector2f(CollisionBounds.X * x, 0.0f);
		}

	    public override void ScaleAbsolute(Vector2f scale)
	    {
		    base.ScaleAbsolute(scale);
			CollisionBounds = new Vector2f(CollisionBounds.X * scale.X, 0.0f);
		}


    }
}