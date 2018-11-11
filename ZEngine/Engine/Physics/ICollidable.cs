
using VelcroPhysics.Dynamics;

namespace ZEngine.Engine.Physics
{
	public interface ICollidable
	{

		bool CollisionCallbacksEnabled { get; set; }
		bool CanOverlap { get; set; }
		void OnCollide(Fixture self, Fixture other, VelcroPhysics.Collision.ContactSystem.Contact contactInfo);
		void OnCollideEnd(Fixture self, Fixture other, VelcroPhysics.Collision.ContactSystem.Contact contactInfo);
		void OnOverlapBegin(Fixture self, Fixture other, VelcroPhysics.Collision.ContactSystem.Contact contactInfo);
		void OnOverlapEnd(Fixture self, Fixture other, VelcroPhysics.Collision.ContactSystem.Contact contactInfo);

	}
}