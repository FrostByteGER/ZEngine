using System;
using ZEngine.Engine.Game.Level;

namespace ZEngine.Engine.Events
{
	public class SpawnActorParams : EngineEventParams
	{
		public Type SpawnableActorType { get; set; }
		public Level_OLD LevelOldRef { get; set; }

		public SpawnActorParams(object instigator, Type spawnableActorType, Level_OLD levelOld) : base(instigator)
		{
			SpawnableActorType = spawnableActorType ?? throw new ArgumentNullException(nameof(spawnableActorType));
			LevelOldRef = levelOld;
		}
	}
}