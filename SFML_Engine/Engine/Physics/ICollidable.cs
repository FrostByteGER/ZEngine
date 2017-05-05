namespace SFML_Engine.Engine.Physics
{
    public interface ICollidable
    {
		void AfterCollision(Actor actor);
		void BeforeCollision(Actor actor);
		void IsOverlapping(Actor actor);
	}

	
}
