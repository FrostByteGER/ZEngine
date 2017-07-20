using System.Collections.Generic;
using SFML_Engine.Engine.Physics;
using SFML_TowerDefense.Source.Game.Units;
using System;
using SFML_TowerDefense.Source.Game.Core;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public abstract class TDTowerWeaponComponent : TDWeaponComponent
	{
		public TDTower ParentTower { get; set; }
		public TDUnit CurrentTarget { get; private set; }
		public List<TDUnit> EnemiesInRange { get; private set; } = new List<TDUnit>();
		private OverlapComponent AttackArea { get; set; }

		protected virtual void OnEnemyEntersRange(TDUnit enemyInRange)
		{
			EnemiesInRange.Add(enemyInRange);
			if (CurrentTarget != null) return;
			CurrentTarget = enemyInRange;
		}
		protected virtual void OnEnemyLeavesRange(TDUnit enemyOutOfRange)
		{
			EnemiesInRange.Remove(enemyOutOfRange);
			//TODO: Sort Remaining Enemies in Range by Distance and pick the furthest one as next target.

			float rang = float.MaxValue; // range ^ 2 

			foreach (TDUnit enemy in EnemiesInRange)
			{
				if ( Math.Pow(enemy.Position.X - ParentTower.Position.X,2) + Math.Pow(enemy.Position.Y - ParentTower.Position.Y, 2) < rang)
				{
					CurrentTarget = enemy;
				}
			}

			if (EnemiesInRange.Count > 0) return; 
			CurrentTarget = null;
		}

		protected virtual void StartFire()
		{
			WeaponState = TDWeaponState.Firing;
			
			

		}

		protected abstract void OnFire();
		
		

		protected virtual void EndFire()
		{
			WeaponState = TDWeaponState.Charging;
			ParentTower.TowerState = TDTowerState.Charging;
			CurrentRechargeTime = RechargeTime;
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (WeaponState == TDWeaponState.Firing)
			{
				OnFire();
			}
			else if (CurrentTarget != null && WeaponState == TDWeaponState.ReadyToFire)
			{
				StartFire();
			}
			else if (WeaponState == TDWeaponState.Charging)
			{
				CurrentRechargeTime -= deltaTime;
				if (CurrentRechargeTime <= 0.0f)
				{
					CurrentRechargeTime = 0.0f;
					WeaponState = TDWeaponState.ReadyToFire;
					ParentTower.TowerState = TDTowerState.ReadyToFire;
				}
			}
		}
	}
}