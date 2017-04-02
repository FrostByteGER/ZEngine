namespace SFML_Engine.Engine
{
    public class SphereCollisionShape : CollisionShape
    {
        
        public float SphereRadius { get; set; }

        public SphereCollisionShape()
        {
            SphereRadius = 1.0f;
        }

        public SphereCollisionShape(float sphereRadius)
        {
            SphereRadius = sphereRadius;
        }
    }
}