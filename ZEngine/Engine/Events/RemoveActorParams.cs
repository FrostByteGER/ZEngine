using System;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.Events
{
	public class RemoveActorParams : EngineEventParams
	{
		public Actor_OLD RemovableActorOld { get; }

		public RemoveActorParams(object instigator, Actor_OLD removableActorOld) : base(instigator)
		{
			RemovableActorOld = removableActorOld ?? throw new ArgumentNullException(nameof(removableActorOld));
		}
	}
}