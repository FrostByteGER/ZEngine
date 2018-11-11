using System;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.Events
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