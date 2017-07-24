using SFML_TowerDefense.Source.Game.Core;
using SFML_TowerDefense.Source.Game.Units;
using System;
using SFML.Graphics;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public class TDLaserWeaponComponent : TDTowerWeaponComponent
	{

		public TDLaserWeaponComponent(Sprite sprite) : base(sprite)
		{
			WeaponDamage = 25;
		}

		protected override void OnEnemyLeavesRange(TDUnit enemyOutOfRange)
		{
			base.OnEnemyLeavesRange(enemyOutOfRange);
			if (WeaponState == TDWeaponState.Firing) EndFire();
		}

		protected override void StartFire()
		{
			base.StartFire();
		}

		protected override void OnFire()
		{
			Console.WriteLine("OnFire");
			CurrentTarget.ApplyDamage((TDActor)this.ParentActor, WeaponDamage, DamageType);
		}

		protected override void EndFire()
		{
			base.EndFire();
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}