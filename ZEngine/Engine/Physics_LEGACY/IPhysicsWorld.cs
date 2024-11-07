using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Dynamics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Physics
{
    public interface IPhysicsWorld
    {
        bool CanTick { get; set; }
        float GameToPhysicsUnitsRatio { get; set; }
        Vector2 Gravity { get; set; }
        World World { get; }

        CollisionComponent ConstructCircleCollisionComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, float circleRadius, BodyType bodyType, bool forceStayAwake = false);
        CollisionComponent ConstructCircleCollisionComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, float circleRadius, BodyType bodyType, Category collisionType, Category collisionResponseChannels, bool forceStayAwake = false);
        OverlapComponent ConstructCircleOverlapComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, float circleRadius, BodyType bodyType, bool forceStayAwake = true);
        OverlapComponent ConstructCircleOverlapComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, float circleRadius, BodyType bodyType, Category collisionType, Category collisionResponseChannels, bool forceStayAwake = true);
        CollisionComponent ConstructRectangleCollisionComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, Vector2 rectHalfExtents, BodyType bodyType, bool forceStayAwake = false);
        CollisionComponent ConstructRectangleCollisionComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, Vector2 rectHalfExtents, BodyType bodyType, Category collisionType, Category collisionResponseChannels, bool forceStayAwake = false);
        OverlapComponent ConstructRectangleOverlapComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, Vector2 rectHalfExtents, BodyType bodyType, bool forceStayAwake = true);
        OverlapComponent ConstructRectangleOverlapComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, Vector2 rectHalfExtents, BodyType bodyType, Category collisionType, Category collisionResponseChannels, bool forceStayAwake = true);
        void PhysicsTick(float deltaTime);
        void UnregisterPhysicsComponent(PhysicsComponent comp);
    }
}