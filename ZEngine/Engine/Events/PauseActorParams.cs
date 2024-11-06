using System;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.Events
{
	public class PauseActorParams : EngineEventParams
	{
		public Actor_OLD PausableActorOld { get; set; }

		public PauseActorParams(object instigator, Actor_OLD pausableActorOld) : base(instigator)
		{
			PausableActorOld = pausableActorOld ?? throw new ArgumentNullException(nameof(pausableActorOld));
		}
	}
}