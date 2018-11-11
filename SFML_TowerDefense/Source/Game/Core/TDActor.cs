using System;
using ZEngine.Engine.Game;

namespace Exofinity.Source.Game.Core
{
	public class TDActor : Actor
	{

		public TDLevel TDLevelRef { get; private set; } = null;
		public TDGameMode TDGameModeRef { get; private set; }

		public TDActor(Level level) : base(level)
		{
			TDLevelRef = level as TDLevel;
			TDGameModeRef = TDLevelRef.GameMode as TDGameMode;
		}

		public virtual void ApplyDamage(float damage)
		{
			Console.WriteLine("APPLYING " + damage + " DAMAGE TO: " + GenerateFullName());

		}

		public virtual void ApplyDamage(TDActor instigator, float damage)
		{
			Console.WriteLine("ACTOR: " + instigator.GenerateFullName() + " APPLYING " + damage + " DAMAGE TO: " + GenerateFullName());
		}

		public virtual void ApplyDamage(TDActor instigator, float damage, TDDamageType damageType)
		{
			Console.WriteLine("ACTOR: " + instigator.GenerateFullName() + " APPLYING " + damage + " " + damageType + " DAMAGE TO: " + GenerateFullName());
		}
	}
}