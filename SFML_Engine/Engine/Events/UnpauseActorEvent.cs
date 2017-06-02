namespace SFML_Engine.Engine.Events
{
	public class UnpauseActorEvent<T> : EngineEvent<T> where T : UnpauseActorParams
	{
		public UnpauseActorEvent(T parameters) : base(parameters)
		{
		}

		public override void ExecuteEvent()
		{
			Parameters.UnpausableActor.OnGameResume();
		}


	}
}