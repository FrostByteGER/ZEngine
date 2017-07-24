using System.Collections.Generic;
using SFML_Engine.Engine.Physics;
using SFML_TowerDefense.Source.Game.Units;
using System;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using SFML_TowerDefense.Source.Game.Core;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Collision.ContactSystem;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public abstract class TDTowerWeaponComponent : TDWeaponComponent
	{
		public TDTower ParentTower { get; set; }
		public TDUnit CurrentTarget { get; private set; }
		public List<TDUnit> EnemiesInRange { get; private set; } = new List<TDUnit>();

		private OverlapComponent AttackArea { get; set; }

		public virtual void OnOverlapBegin(Fixture self, Fixture other, Contact contactInfo)
		{
			if(other.Body.UserData != null && other.Body.UserData is ActorComponent)
			{

				ActorComponent acomp = (ActorComponent)other.Body.UserData;

				if (acomp.ParentActor is TDUnit )
				{
					OnEnemyEntersRange((TDUnit)acomp.ParentActor);
					RotateWaponTo((TDUnit)acomp.ParentActor);
				}
			}
		}

		public virtual void OnOverlapEnd(Fixture self, Fixture other, Contact contactInfo)
		{
			if (other.Body.UserData != null && other.Body.UserData is ActorComponent)
			{
				ActorComponent acomp = (ActorComponent)other.Body.UserData;

				if (acomp.ParentActor is TDUnit)
				{
					OnEnemyLeavesRange((TDUnit)acomp.ParentActor);
					RotateWaponTo((TDUnit)acomp.ParentActor);
				}
			}
			
		}

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
				CurrentRechargeTime = RechargeTime;
				WeaponState = TDWeaponState.Charging;
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

			if (CurrentTarget != null)
			{
				RotateWaponTo(CurrentTarget);
			}
		}

		public void RotateWaponTo(TDUnit target)
		{

			//Need Testing

			// TVector2f selfPoint = new TVector2f(WorldPosition.X / (Math.Abs(WorldPosition.X) + Math.Abs(WorldPosition.Y)), WorldPosition.Y / (Math.Abs(WorldPosition.X) + Math.Abs(WorldPosition.Y)));

			// TVector2f otherPoint = new TVector2f(target.Body.Position.X / (Math.Abs(target.Body.Position.X) + Math.Abs(target.Body.Position.Y)), target.Body.Position.Y / (Math.Abs(target.Body.Position.X) + Math.Abs(target.Body.Position.Y))); ;

			TVector2f dic = WorldPosition - target.Position;

			dic = new TVector2f(dic.X / (Math.Abs(dic.X) + Math.Abs(dic.Y)), dic.Y / (Math.Abs(dic.X) + Math.Abs(dic.Y)));

			//RotateLocal((float)(Math.Atan2(dic.X, dic.Y) * 180 / Math.PI));
			SetLocalRotation((float)(Math.Atan2(dic.X, -dic.Y) * 180 / Math.PI));

		}

		protected TDTowerWeaponComponent(Sprite sprite) : base(sprite)
		{
		}
		
		
	}
}