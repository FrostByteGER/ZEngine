using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;

namespace SFML_Breakout
{
	public class PowerUp : SpriteActor ,ICloneable
	{
		public PowerUp()
		{
			Velocity = new Vector2f(0f, 75f);
			CollisionShape = new SphereShape(10);
		}

		public PowerUp(PowerUp p)
		{
			Velocity = p.Velocity;
			CollisionShape = new SphereShape(p.CollisionShape.CollisionBounds.X);
		}

		public override void IsOverlapping(Actor actor)
		{
			base.IsOverlapping(actor);

			if (actor.ActorName == "Player Pad 1")
			{
				Apply();
				var powerUpSound = new Sound(BreakoutPersistentGameMode.PowerUpBuffer);
				powerUpSound.Volume = LevelReference.EngineReference.GlobalVolume;
				powerUpSound.Play();
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

		public virtual object Clone()
		{
			return new PowerUp(this);
		}
	}
}
