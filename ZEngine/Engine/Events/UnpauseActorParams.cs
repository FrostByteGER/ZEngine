using System;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.Events
{
	public class UnpauseActorParams : EngineEventParams
	{
		public Actor_OLD UnpausableActorOld { get; set; }

		public UnpauseActorParams(object instigator, Actor_OLD unpausableActorOld) : base(instigator)
		{
			UnpausableActorOld = unpausableActorOld ?? throw new ArgumentNullException(nameof(unpausableActorOld));
		}
	}
}