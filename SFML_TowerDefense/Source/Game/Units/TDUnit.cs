using System;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;
using SFML_TowerDefense.Source.Game.AI;
using SFML_TowerDefense.Source.Game.Core;

namespace SFML_TowerDefense.Source.Game.Units
{
	public class TDUnit : TDActor
	{

		public int HP { get; set; } = 100;
		public float MovmentSpeed { get; set; } = 1;
		public TDWaypoint CurrentWaypoint { get; set; }
		public float WaypointThreshold { get; set; } = 0.0f;
		public TDUnitState UnitState { get; set; } = TDUnitState.Walking;

		public TDUnit(Level level) : base(level)
		{
			SetRootComponent(
				new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("TowerBase2"))));
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
					Position = EngineMath.VInterpTo(Position, CurrentWaypoint.Position, deltaTime, 1.0f);
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