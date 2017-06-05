using System;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Events
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