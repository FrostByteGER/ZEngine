using System;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Events
{
	public class SpawnActorParams : EngineEventParams
	{
		public Actor SpawnableActor { get; set; }
		public Level LevelRef { get; set; }

		public SpawnActorParams(object instigator, Actor spawnableActor, Level level) : base(instigator)
		{
			SpawnableActor = spawnableActor ?? throw new ArgumentNullException(nameof(spawnableActor));
			LevelRef = level;
		}
	}
}