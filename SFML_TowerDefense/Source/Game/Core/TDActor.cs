using System;
using SFML_Engine.Engine.Game;

namespace SFML_TowerDefense.Source.Game.Core
{
	public class TDActor : Actor
	{

		public TDLevel TDLevelRef { get; private set; } = null;

		public TDActor(Level level) : base(level)
		{
			TDLevelRef = level as TDLevel;
		}

		public virtual void ApplyDamage(int damage)
		{
			Console.WriteLine("APPLYING " + damage + " DAMAGE TO: " + GenerateFullName());

		}

		public virtual void ApplyDamage(TDActor instigator, int damage)
		{
			Console.WriteLine("ACTOR: " + instigator.GenerateFullName() + " APPLYING " + damage + " DAMAGE TO: " + GenerateFullName());
		}

		public virtual void ApplyDamage(TDActor instigator, int damage, TDDamageType damageType)
		{
			Console.WriteLine("ACTOR: " + instigator.GenerateFullName() + " APPLYING " + damage + " " + damageType + " DAMAGE TO: " + GenerateFullName());
		}
	}
}