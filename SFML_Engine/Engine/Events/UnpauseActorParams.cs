using System;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Events
{
	public class UnpauseActorParams : EngineEventParams
	{
		public Actor UnpausableActor { get; set; }

		public UnpauseActorParams(object instigator, Actor unpausableActor) : base(instigator)
		{
			UnpausableActor = unpausableActor ?? throw new ArgumentNullException(nameof(unpausableActor));
		}
	}
}