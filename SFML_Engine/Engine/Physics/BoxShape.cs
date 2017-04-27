using SFML.System;

namespace SFML_Engine.Engine.Physics
{
    public class BoxShape : CollisionShape
    {
		/// <summary>
		/// BoxExtent of this Shape. Used for Scale Calculations, never change after constructor assignment!
		/// TODO. Make custom set that updates BoxExtentHalf
		/// </summary>
		public Vector2f BoxExtent { get; private set; } = new Vector2f(1.0f, 1.0f);

		/// <summary>
		/// Used for Scale Calculations, never change after constructor assignment!
		/// </summary>
	    public Vector2f BoxExtentHalf { get; } = new Vector2f(.5f, .5f);

		public BoxShape()
        {
        }

        public BoxShape(Vector2f boxExtent)
        {
            BoxExtent = boxExtent;
	        BoxExtentHalf = boxExtent / 2.0f;

        }

        public BoxShape(float boxExtentX, float boxExtentY)
        {
            BoxExtent = new Vector2f(boxExtentX, boxExtentY);
	        BoxExtentHalf = BoxExtent / 2.0f;
		}

		public Vector2f GetMid(Vector2f position)
		{
			return new Vector2f(position.X + BoxExtentHalf.X, position.Y + BoxExtentHalf.Y);
		}

	    public override void ScaleActor(float x, float y)
	    {
		    base.ScaleActor(x, y);
		    BoxExtent = new Vector2f(BoxExtentHalf.X * 2.0f * Scale.X, BoxExtentHalf.Y * 2.0f * Scale.Y);
	    }

		public override void ScaleActor(Vector2f scale)
	    {
		    base.ScaleActor(scale);
			BoxExtent = new Vector2f(BoxExtentHalf.X * 2.0f * Scale.X, BoxExtentHalf.Y * 2.0f * Scale.Y);
		}

		public override void ScaleAbsolute(float x, float y)
	    {
		    base.ScaleAbsolute(x, y);
			BoxExtent = new Vector2f(BoxExtentHalf.X * 2.0f * x, BoxExtentHalf.Y * 2.0f * y);

		}

		public override void ScaleAbsolute(Vector2f scale)
	    {
		    base.ScaleAbsolute(scale);
			BoxExtent = new Vector2f(BoxExtentHalf.X * 2.0f * scale.X, BoxExtentHalf.Y * 2.0f * scale.Y);
		}
	}
}