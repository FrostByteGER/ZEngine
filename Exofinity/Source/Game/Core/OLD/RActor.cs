using System;
using ZEngine.Engine.Game;

namespace Exofinity.Source.Game.Core
{
	public class RActor : Actor
	{

		public RLevel RLevelRef { get; private set; } = null;
		public RGameMode RGameModeRef { get; private set; }

		public RActor(Level level) : base(level)
		{
			RLevelRef = level as RLevel;
			RGameModeRef = RLevelRef.GameMode as RGameMode;
		}

		public virtual void ApplyDamage(float damage)
		{
			Console.WriteLine("APPLYING " + damage + " DAMAGE TO: " + GenerateFullName());

		}

		public virtual void ApplyDamage(RActor instigator, float damage)
		{
			Console.WriteLine("ACTOR: " + instigator.GenerateFullName() + " APPLYING " + damage + " DAMAGE TO: " + GenerateFullName());
		}

		public virtual void ApplyDamage(RActor instigator, float damage, RDamageType damageType)
		{
			Console.WriteLine("ACTOR: " + instigator.GenerateFullName() + " APPLYING " + damage + " " + damageType + " DAMAGE TO: " + GenerateFullName());
		}
	}
}