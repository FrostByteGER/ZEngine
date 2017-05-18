using System;

namespace SFML_Engine.Engine.Events
{
	public class SpawnActorParams : EngineEventParams
	{
		public Actor SpawnableActor { get; set; }
		public uint LevelID { get; set; }

		public SpawnActorParams(object instigator, Actor spawnableActor, uint levelId) : base(instigator)
		{
			SpawnableActor = spawnableActor ?? throw new ArgumentNullException(nameof(spawnableActor));
			LevelID = levelId;
		}
	}
}