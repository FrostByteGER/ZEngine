using System;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Events
{
	public class SpawnActorParams : EngineEventParams
	{
		public Type SpawnableActorType { get; set; }
		public Level LevelRef { get; set; }

		public SpawnActorParams(object instigator, Type spawnableActorType, Level level) : base(instigator)
		{
			SpawnableActorType = spawnableActorType ?? throw new ArgumentNullException(nameof(spawnableActorType));
			LevelRef = level;
		}
	}
}