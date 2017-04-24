using System;

namespace SFML_Engine.Engine.Events
{
	public class RemoveActorParams : EngineEventParams
	{
		public Actor RemovableActor { get; }

		public RemoveActorParams(object instigator, Actor removableActor) : base(instigator)
		{
			RemovableActor = removableActor ?? throw new ArgumentNullException(nameof(removableActor));
		}
	}
}