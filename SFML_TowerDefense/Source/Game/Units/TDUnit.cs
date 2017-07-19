using System;
using SFML_Engine.Engine.Game;

namespace SFML_TowerDefense.Source.Game.Units
{
	public class TDUnit : TDActor
	{

		public int HP { get; set; } = 100;
		public float MovmentSpeed { get; set; } = 1;
		public TDWaypoint Waypoint { get; set; }
		public TDUnitState UnitState { get; set; } = TDUnitState.Walking;

		public TDUnit(Level level) : base(level)
		{
		}

		public void ApplyDamage(TDActor instigator, int damage, TDDamageType damageType)
		{
			Console.WriteLine("ACTOR: " + instigator.GenerateFullName() + " APPLYING "+ damage + " " + damageType + " TO: " + GenerateFullName());
			if (UnitState == TDUnitState.Dead) return;
			HP -= damage;
			if (HP <= 0)
			{
				HP = 0;
				UnitState = TDUnitState.Dead;
			}
		}
	}
}