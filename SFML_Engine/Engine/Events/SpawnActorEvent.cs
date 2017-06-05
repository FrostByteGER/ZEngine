﻿namespace SFML_Engine.Engine.Events
{
	public class SpawnActorEvent<T> : EngineEvent<T> where T : SpawnActorParams
	{
		public SpawnActorEvent(T parameters) : base(parameters)
		{
		}

		public override void ExecuteEvent()
		{
			Core.Engine.Instance.ActiveLevel.RegisterActor(Parameters.SpawnableActor);
			Parameters.SpawnableActor.OnGameStart();
		}
	}
}