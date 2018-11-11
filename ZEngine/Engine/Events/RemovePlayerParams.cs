using System;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.Events
{
	public class RemovePlayerParams : EngineEventParams
	{
		public PlayerController RemovablePlayer { get; }

		public RemovePlayerParams(object instigator, PlayerController removablePlayer) : base(instigator)
		{
			RemovablePlayer = removablePlayer ?? throw new ArgumentNullException(nameof(removablePlayer));
		}
	}
}