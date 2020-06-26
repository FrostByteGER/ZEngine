using System;
using ZEngine.Engine.Game.Level;

namespace ZEngine.Engine.Events
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