using System;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Events
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