using System;

namespace SFML_Engine.Engine.Events
{
	public class RemovePlayerEvent<T> : EngineEvent<T> where  T : RemovePlayerParams
	{
		public RemovePlayerEvent(T parameters) : base(parameters)
		{
			parameters.RemovablePlayer.IsActive = false;
			parameters.RemovablePlayer.CanTick = false;
		}

		public override void ExecuteEvent()
		{
			if (Parameters.RemovablePlayer != null)
			{
				var player = Parameters.RemovablePlayer;
				player.OnGameEnd();
				Core.Engine.Instance.ActiveLevel.UnregisterPlayer(player);
				return;
			}
			Console.WriteLine("Failed to Remove Player");
		}
	}
}