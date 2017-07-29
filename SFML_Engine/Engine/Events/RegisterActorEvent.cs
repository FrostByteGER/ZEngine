namespace SFML_Engine.Engine.Events
{
	public class RegisterActorEvent<T> : EngineEvent<T> where T : RegisterActorParams
	{
		public RegisterActorEvent(T parameters) : base(parameters)
		{
		}

		public override void ExecuteEvent()
		{
			//var actor = Parameters.LevelRef.SpawnActorInternal(Parameters.SpawnableActorType);
			Parameters.LevelRef.RegisterActor(Parameters.RegisterableActor);
			Parameters.RegisterableActor.InitializeActor();
			Parameters.RegisterableActor.OnGameStart();
		}
	}
}