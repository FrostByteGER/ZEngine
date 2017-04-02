using SFML.System;

namespace SFML.Graphics.Engine
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
    }
}