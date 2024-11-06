using System;
using ZEngine.Engine.Game;
using ZEngine.Engine.Game.Level;

namespace ZEngine.Engine.Events
{
	public class SpawnPlayerParams : EngineEventParams
	{
		public PlayerController_OLD SpawnablePlayer { get; set; }
		public Level_OLD LevelOldRef { get; set; }

		public SpawnPlayerParams(object instigator, PlayerController_OLD spawnablePlayer, Level_OLD levelOld) : base(instigator)
		{
			SpawnablePlayer = spawnablePlayer ?? throw new ArgumentNullException(nameof(spawnablePlayer));
			LevelOldRef = levelOld;
		}
	}
}