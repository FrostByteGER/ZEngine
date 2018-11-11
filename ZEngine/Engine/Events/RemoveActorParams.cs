using System;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.Events
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