using System;
using System.Linq;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;
using SFML_TowerDefense.Source.Game.Buildings;
using SFML_TowerDefense.Source.Game.Core;

namespace SFML_TowerDefense.Source.Game.Units
{
	public class TDUnit : TDActor, ICloneable
	{

		public int HP { get; set; } = 100;
		public float MovmentSpeed { get; set; } = 1;
		public TDWaypoint CurrentWaypoint { get; set; }
		public uint Damage { get; set; } = 30;

		public TDDamageType ElementResistances { get; set; } = TDDamageType.None;
		public float WaypointThreshold { get; set; } = 2.0f;
		public TDUnitState UnitState { get; set; } = TDUnitState.Walking;

		public TDUnit(Level level) : base(level)
		{
			SetRootComponent(
				new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("TowerBase2"))));
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
		}

		public override void ApplyDamage(TDActor instigator, int damage, TDDamageType damageType)
		{
			
			if (UnitState == TDUnitState.Dead) return;
			var resistanceMultiplier = 1.0f;
			if (ElementResistances != TDDamageType.None && (ElementResistances & damageType) == damageType) resistanceMultiplier = 0.5f;
			HP -= (int)(damage * resistanceMultiplier);
			if (HP <= 0)
			{
				HP = 0;
				UnitState = TDUnitState.Dead;
				OnDeath();
				LevelReference.DestroyActor(this, this);
			}
		}

		public void Explode()
		{
			var target = LevelReference.FindActorsInLevel<TDNexus>().FirstOrDefault(nexus => nexus.NexusID == CurrentWaypoint.TargetNexus);
			target?.ApplyDamage(this, (int) Damage);
			OnDeath();
			LevelReference.DestroyActor(this, this);
		}

		public void OnDeath()
		{
			--TDGameModeRef.EnemiesLeftInCurrentWave;
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (CurrentWaypoint != null)
			{
				if ((Position - CurrentWaypoint.Position).LengthSquared > WaypointThreshold * WaypointThreshold)
				{
					Position = EngineMath.VInterpToConstant(Position, CurrentWaypoint.Position, deltaTime, 50.0f);
				}
				else
				{
					if (CurrentWaypoint.NextWaypoint == null)
					{
						Explode();
						return;
					}
					CurrentWaypoint = CurrentWaypoint.NextWaypoint;
					
				}
			}
			else
			{
				Explode();
			}
		}

		/// <summary>
		/// NOT WORKING CURRENTLY!
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}