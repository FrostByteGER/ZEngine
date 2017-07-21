using System;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Events
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