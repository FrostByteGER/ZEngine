
using VelcroPhysics.Dynamics;

namespace SFML_Engine.Engine.Physics
{
	public interface ICollidable
	{

		bool CollisionCallbacksEnabled { get; set; }
		bool CanOverlap { get; set; }
		void OnCollide(Fixture otherActor, Fixture self, VelcroPhysics.Collision.ContactSystem.Contact contactInfo);
		void OnCollideEnd(Fixture otherActor, Fixture self, VelcroPhysics.Collision.ContactSystem.Contact contactInfo);
		void OnOverlapBegin(Fixture otherActor, Fixture self, VelcroPhysics.Collision.ContactSystem.Contact contactInfo);
		void OnOverlapEnd(Fixture otherActor, Fixture self, VelcroPhysics.Collision.ContactSystem.Contact contactInfo);

	}
}