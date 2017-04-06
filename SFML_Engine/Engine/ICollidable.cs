namespace SFML_Engine.Engine
{
    public interface ICollidable
    {
        CollisionShape CollisionShape { get; set; }

		void AfterCollision(Actor actor);
		void BeforeCollision(Actor actor);
		void IsOverlapping(Actor actor);

	}

	
}
