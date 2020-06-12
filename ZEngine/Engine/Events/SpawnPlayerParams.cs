using System;
using ZEngine.Engine.Game;
using ZEngine.Engine.Game.Level;

namespace ZEngine.Engine.Events
{
	public class SpawnPlayerParams : EngineEventParams
	{
		public PlayerController SpawnablePlayer { get; set; }
		public Level LevelRef { get; set; }

		public SpawnPlayerParams(object instigator, PlayerController spawnablePlayer, Level level) : base(instigator)
		{
			SpawnablePlayer = spawnablePlayer ?? throw new ArgumentNullException(nameof(spawnablePlayer));
			LevelRef = level;
		}
	}
}