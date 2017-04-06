using SFML.System;

namespace SFML_Engine.Engine
{
    public class BoxShape : CollisionShape
    {
        public Vector2f BoxExtent{ get; set; }

        public BoxShape()
        {
        }

        public BoxShape(Vector2f boxExtent)
        {
            BoxExtent = boxExtent;
        }

        public BoxShape(float boxExtentX, float boxExtentY)
        {
            BoxExtent = new Vector2f(boxExtentX, boxExtentY);
        }

		public Vector2f getMid(Vector2f position)
		{
			return position + new Vector2f(BoxExtent.X/2f, BoxExtent.Y/2f);
		}
    }
}