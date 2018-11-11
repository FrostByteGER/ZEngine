using System.Collections.Generic;
using System;
using Exofinity.Source.Game.Core;
using Exofinity.Source.Game.Units;
using SFML.Audio;
using SFML.Graphics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Collision.ContactSystem;

namespace Exofinity.Source.Game.Buildings.Towers
{
	public abstract class RTowerWeaponComponent : RWeaponComponent
	{
		public RTower ParentTower { get; set; }
		public RUnit CurrentTarget { get; private set; }
		public List<RUnit> EnemiesInRange { get; private set; } = new List<RUnit>();
		public Sound FireSound { get; set; }

		protected RTowerWeaponComponent(Sprite sprite) : base(sprite)
		{
		}

		public virtual void OnOverlapBegin(Fixture self, Fixture other, Contact contactInfo)
		{
			ActorComponent acomp = other.Body.UserData as ActorComponent;

			if (acomp?.ParentActor is RUnit )
			{
				OnEnemyEntersRange((RUnit)acomp.ParentActor);
				RotateWaponTo((RUnit)acomp.ParentActor);
			}
		}

		public virtual void OnOverlapEnd(Fixture self, Fixture other, Contact contactInfo)
		{
			ActorComponent acomp = other.Body.UserData as ActorComponent;

			if (acomp?.ParentActor is RUnit)
			{
				OnEnemyLeavesRange((RUnit)acomp.ParentActor);
				RotateWaponTo((RUnit)acomp.ParentActor);
			}
		}

		protected virtual void OnEnemyEntersRange(RUnit enemyInRange)
		{
			EnemiesInRange.Add(enemyInRange);
			if (CurrentTarget != null) return;
			CurrentTarget = enemyInRange;
		}

		protected virtual void OnEnemyLeavesRange(RUnit enemyOutOfRange)
		{
			EnemiesInRange.Remove(enemyOutOfRange);

			var rang = float.MaxValue; // range ^ 2 

			foreach (var enemy in EnemiesInRange)
			{
				if ( Math.Pow(enemy.Position.X - ParentTower.Position.X,2) + Math.Pow(enemy.Position.Y - ParentTower.Position.Y, 2) < rang)
				{
					OnCurrentTargetSwitched(CurrentTarget, enemy);
					CurrentTarget = enemy;
				}
			}

			if (EnemiesInRange.Count > 0) return; 
			CurrentTarget = null;
		}

		public virtual void OnCurrentTargetSwitched(RUnit oldTarget, RUnit newTarget)
		{
			
		}

		protected virtual void StartFire()
		{
			WeaponState = RWeaponState.Firing;
			FireSound?.Play();
		}

		protected abstract void OnFire();

		protected virtual void EndFire()
		{
			WeaponState = RWeaponState.Charging;
			ParentTower.TowerState = TDTowerState.Charging;
			CurrentRechargeTime = RechargeTime;
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (CurrentTarget != null)
			{
				RotateWaponTo(CurrentTarget);
			}
			
			if (WeaponState == RWeaponState.Firing)
			{
				OnFire();
			}
			else if (WeaponState == RWeaponState.ReadyToFire)
			{
				if (CurrentTarget != null)
				{
					StartFire();
				}
				else
				{
					EndFire();
				}

				
			}
			else if (WeaponState == RWeaponState.Charging)
			{
				CurrentRechargeTime -= deltaTime;
				if (CurrentRechargeTime <= 0.0f)
				{
					CurrentRechargeTime = 0.0f;
					WeaponState = RWeaponState.ReadyToFire;
					ParentTower.TowerState = TDTowerState.ReadyToFire;
				}
			}
		}

		public void RotateWaponTo(RUnit target)
		{

			//Need Testing

			// TVector2f selfPoint = new TVector2f(WorldPosition.X / (Math.Abs(WorldPosition.X) + Math.Abs(WorldPosition.Y)), WorldPosition.Y / (Math.Abs(WorldPosition.X) + Math.Abs(WorldPosition.Y)));

			// TVector2f otherPoint = new TVector2f(target.Body.Position.X / (Math.Abs(target.Body.Position.X) + Math.Abs(target.Body.Position.Y)), target.Body.Position.Y / (Math.Abs(target.Body.Position.X) + Math.Abs(target.Body.Position.Y))); ;

			TVector2f dic = WorldPosition - target.Position;

			dic = new TVector2f(dic.X / (Math.Abs(dic.X) + Math.Abs(dic.Y)), dic.Y / (Math.Abs(dic.X) + Math.Abs(dic.Y)));

			//RotateLocal((float)(Math.Atan2(dic.X, dic.Y) * 180 / Math.PI));
			SetLocalRotation((float)(Math.Atan2(dic.X, -dic.Y) * 180 / Math.PI));

		}

		public override void OnActorComponentDestroy()
		{
			base.OnActorComponentDestroy();
			FireSound.Stop();
		}
	}
}