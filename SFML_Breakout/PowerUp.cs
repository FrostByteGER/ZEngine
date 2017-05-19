using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Game;

namespace SFML_Breakout
{
	class PowerUp : SpriteActor
	{


		public PowerUp()
		{
			Velocity = new Vector2f(0f, 75f);
			//CollisionShape = new SphereShape(10);
		}

		public override void IsOverlapping(Actor actor)
		{
			base.IsOverlapping(actor);

			if (actor.ActorName == "Player Pad 1")
			{
				Apply();
				LevelReference.EngineReference.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, this)));
			}

			if (actor.ActorName == "Bottom Border")
			{
				LevelReference.EngineReference.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, this)));
			}
		}

		public virtual void Apply()
		{

		}
	}
}
