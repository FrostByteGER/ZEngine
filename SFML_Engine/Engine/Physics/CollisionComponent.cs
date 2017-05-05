using System;

namespace SFML_Engine.Engine.Physics
{
	public class CollisionComponent : ActorComponent
	{
		
		public CollisionShape CollisionShape { get; set; }

		public CollisionComponent(CollisionShape collisionShape)
		{
			if (collisionShape == null) throw new ArgumentNullException(nameof(collisionShape));
			CollisionShape = collisionShape;
		}
	}
}