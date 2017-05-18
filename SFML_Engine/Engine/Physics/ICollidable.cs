namespace SFML_Engine.Engine.Physics
{
    public interface ICollidable
    {
        CollisionShape CollisionShape { get; set; }

		void AfterCollision(Actor actor);
		void BeforeCollision(Actor actor);
		void IsOverlapping(Actor actor);

	}

	
}
