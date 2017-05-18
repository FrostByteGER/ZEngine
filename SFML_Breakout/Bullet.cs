using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;

namespace SFML_Breakout
{
	class Bullet : SpriteActor
	{	

		public Bullet()
		{
		}

		public override void AfterCollision(Actor actor)
		{
		}

		public override void IsOverlapping(Actor actor)
		{
			base.IsOverlapping(actor);

			// why no Border Actor Kevin ??? (sadface)
			if (actor.ActorName == "Top Border" || actor.ActorName == "Bottom Border" || actor.ActorName == "Left Border" || actor.ActorName == "Right Border")
			{
				LevelReference.EngineReference.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, this)));
			}
			
		}
	}
}
