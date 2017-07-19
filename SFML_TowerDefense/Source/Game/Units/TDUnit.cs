using System;
using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.AI;

namespace SFML_TowerDefense.Source.Game.Units
{
	public class TDUnit : TDActor
	{

		public int HP { get; set; } = 100;
		public float MovmentSpeed { get; set; } = 1;
		public TDWaypoint CurrentWaypoint { get; set; }
		public float WaypointThreshold { get; set; } = 5.0f;
		public TDUnitState UnitState { get; set; } = TDUnitState.Walking;

		public TDUnit(Level level) : base(level)
		{
		}

		public override void ApplyDamage(TDActor instigator, int damage, TDDamageType damageType)
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

		public void Explode()
		{
			Console.WriteLine("ACTOR: " + GenerateFullName() + " MAKES BOOM!");
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (CurrentWaypoint != null)
			{
				if ((Position - CurrentWaypoint.Position).LengthSquared > WaypointThreshold * WaypointThreshold)
				{

				}
				else
				{
					CurrentWaypoint = CurrentWaypoint.NextWaypoint;
					if (CurrentWaypoint != null) Explode();
				}
			}
			else
			{
				Explode();
			}
		}
	}
}