using System;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.Events
{
	public class RemovePlayerParams : EngineEventParams
	{
		public PlayerController_OLD RemovablePlayer { get; }

		public RemovePlayerParams(object instigator, PlayerController_OLD removablePlayer) : base(instigator)
		{
			RemovablePlayer = removablePlayer ?? throw new ArgumentNullException(nameof(removablePlayer));
		}
	}
}