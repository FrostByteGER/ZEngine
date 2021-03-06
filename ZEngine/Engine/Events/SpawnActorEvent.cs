﻿namespace ZEngine.Engine.Events
{
	public class SpawnActorEvent<T> : EngineEvent<T> where T : SpawnActorParams
	{
		public SpawnActorEvent(T parameters) : base(parameters)
		{
		}

		public override void ExecuteEvent()
		{
			var actor = Parameters.LevelRef.SpawnActorInternal(Parameters.SpawnableActorType);
		}
	}
}