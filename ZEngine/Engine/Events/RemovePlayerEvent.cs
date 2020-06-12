using System;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Events
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
			throw new NotImplementedException();
			if (Parameters.RemovablePlayer != null)
			{
				var player = Parameters.RemovablePlayer;
				player.OnGameEnd();
				//Core.Engine.Instance.ActiveLevel.UnregisterPlayer(player);
				return;
			}
			Debug.LogError("Failed to Remove Player", DebugLogCategories.Engine);
		}
	}
}