using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public class BoxShape : CollisionShape
    {

		/// <summary>
		/// Collision Bounds of this collision shape. X axis is the Box X Extent. Y axis is the Box Y Extent.
		/// </summary>
		public override Vector2f CollisionBounds { get; set; }

		public BoxShape()
        {
        }

        public BoxShape(Vector2f boxExtent)
        {
	        CollisionBounds = boxExtent;
        }

        public BoxShape(float boxExtentX, float boxExtentY)
        {
			CollisionBounds = new Vector2f(boxExtentX, boxExtentY);
		}

		public Vector2f GetMid(Vector2f position)
		{
			return new Vector2f(position.X + CollisionBounds.X/2.0f, position.Y + CollisionBounds.Y / 2.0f);
		}

		/*
	    public override void ScaleActor(float x, float y)
	    {
		    base.ScaleActor(x, y);
			CollisionBounds = new Vector2f(CollisionBounds.X * Scale.X, CollisionBounds.Y * Scale.Y);
	    }

		public override void ScaleActor(Vector2f scale)
	    {
		    base.ScaleActor(scale);
			CollisionBounds = new Vector2f(CollisionBounds.X * Scale.X, CollisionBounds.Y * Scale.Y);
		}

		public override void ScaleAbsolute(float x, float y)
	    {
		    base.ScaleAbsolute(x, y);
			CollisionBounds = new Vector2f(CollisionBounds.X * x, CollisionBounds.Y * y);

		}

		public override void ScaleAbsolute(Vector2f scale)
	    {
		    base.ScaleAbsolute(scale);
			CollisionBounds = new Vector2f(CollisionBounds.X * scale.X, CollisionBounds.Y * scale.Y);
		}
		*/
	}
}