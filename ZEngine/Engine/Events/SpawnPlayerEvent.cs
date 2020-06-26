using System;

namespace ZEngine.Engine.Events
{
	public class SpawnPlayerEvent<T> : EngineEvent<T> where T : SpawnPlayerParams
	{
		public SpawnPlayerEvent(T parameters) : base(parameters)
		{
		}

		public override void ExecuteEvent()
		{
            throw new NotImplementedException();
			var player = Parameters.SpawnablePlayer;
			//Parameters.LevelRef.RegisterPlayer(player);
            player.OnGameStart();
		}
	}
}