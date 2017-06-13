namespace SFML_Engine.Engine.Events
{
	public class SpawnPlayerEvent<T> : EngineEvent<T> where T : SpawnPlayerParams
	{
		public SpawnPlayerEvent(T parameters) : base(parameters)
		{
		}

		public override void ExecuteEvent()
		{
			var player = Parameters.SpawnablePlayer;
			Parameters.LevelRef.RegisterPlayer(player);
			player.IsActive = player.MarkedForInputRegistering;
			player.MarkedForInputRegistering = false;
			player.OnGameStart();
		}
	}
}