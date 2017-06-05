namespace SFML_Engine.Engine.Events
{
	public class PauseActorEvent<T> : EngineEvent<T> where T : PauseActorParams
	{
		public PauseActorEvent(T parameters) : base(parameters)
		{
		}

		public override void ExecuteEvent()
		{
			Parameters.PausableActor.OnGamePause();
		}


	}
}