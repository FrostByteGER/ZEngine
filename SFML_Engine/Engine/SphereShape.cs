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
    }
}