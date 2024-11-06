namespace ZEngine.Engine.Events
{
	public class RegisterActorEvent<T> : EngineEvent<T> where T : RegisterActorParams
	{
		public RegisterActorEvent(T parameters) : base(parameters)
		{
		}

		public override void ExecuteEvent()
		{
			//var actor = Parameters.LevelRef.SpawnActorInternal(Parameters.SpawnableActorType);
			Parameters.LevelOldRef.RegisterActor(Parameters.RegisterableActorOld);
			Parameters.RegisterableActorOld.InitializeActor();
			Parameters.RegisterableActorOld.OnGameStart();
		}
	}
}