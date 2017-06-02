using System;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Events
{
	public class SpawnPlayerParams : EngineEventParams
	{
		public PlayerController SpawnablePlayer { get; set; }
		public uint LevelID { get; set; }

		public SpawnPlayerParams(object instigator, PlayerController spawnablePlayer, uint levelId) : base(instigator)
		{
			SpawnablePlayer = spawnablePlayer ?? throw new ArgumentNullException(nameof(spawnablePlayer));
			LevelID = levelId;
		}
	}
}