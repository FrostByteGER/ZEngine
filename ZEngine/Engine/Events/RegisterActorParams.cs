using System;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.Events
{
	public class RegisterActorParams : EngineEventParams
	{
		public Actor RegisterableActor { get; set; }
		public Level LevelRef { get; set; }

		public RegisterActorParams(object instigator, Actor registerableActor, Level level) : base(instigator)
		{
			RegisterableActor = registerableActor ?? throw new ArgumentNullException(nameof(registerableActor));
			LevelRef = level;
		}
	}
}