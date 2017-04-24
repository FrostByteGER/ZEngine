using System;

namespace SFML_Engine.Engine.Events
{
	public class SpawnActorEventParams : EngineEventParams
	{
		public Actor SpawnableActor { get; set; }
		public uint LevelID { get; set; }

		public SpawnActorEventParams(object instigator, Actor spawnableActor, uint levelId) : base(instigator)
		{
			SpawnableActor = spawnableActor ?? throw new ArgumentNullException(nameof(spawnableActor));
			LevelID = levelId;
		}
	}
}