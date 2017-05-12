using BulletSharp;

namespace SFML_Engine.Engine.Physics
{
	public interface ICollidable
	{

		bool CollisionCallbacksEnabled { get; set; }
		void OnCollide(ManifoldPoint cp, CollisionObjectWrapper collider1, int partId1, int index1, CollisionObjectWrapper collider2, int partId2, int index2);

	}
}