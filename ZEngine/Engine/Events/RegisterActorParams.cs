using System;
using ZEngine.Engine.Game;
using ZEngine.Engine.Game.Level;

namespace ZEngine.Engine.Events
{
	public class RegisterActorParams : EngineEventParams
	{
		public Actor_OLD RegisterableActorOld { get; set; }
		public Level_OLD LevelOldRef { get; set; }

		public RegisterActorParams(object instigator, Actor_OLD registerableActorOld, Level_OLD levelOld) : base(instigator)
		{
			RegisterableActorOld = registerableActorOld ?? throw new ArgumentNullException(nameof(registerableActorOld));
			LevelOldRef = levelOld;
		}
	}
}