using System;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Events
{
	public class PauseActorParams : EngineEventParams
	{
		public Actor PausableActor { get; set; }

		public PauseActorParams(object instigator, Actor pausableActor) : base(instigator)
		{
			PausableActor = pausableActor ?? throw new ArgumentNullException(nameof(pausableActor));
		}
	}
}