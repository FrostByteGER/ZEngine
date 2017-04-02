using SFML.System;

namespace SFML_Engine.Engine
{
    public class BoxCollisionShape : CollisionShape
    {
        public Vector2f BoxExtent{ get; set; }

        public BoxCollisionShape()
        {
        }

        public BoxCollisionShape(Vector2f boxExtent)
        {
            BoxExtent = boxExtent;
        }

        public BoxCollisionShape(float boxExtentX, float boxExtentY)
        {
            BoxExtent = new Vector2f(boxExtentX, boxExtentY);
        }
    }
}