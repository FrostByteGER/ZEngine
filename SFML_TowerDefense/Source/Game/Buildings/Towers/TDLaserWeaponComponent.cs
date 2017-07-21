using SFML_TowerDefense.Source.Game.Core;
using SFML_TowerDefense.Source.Game.Units;
using System;
using SFML.Graphics;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public class TDLaserWeaponComponent : TDTowerWeaponComponent
	{


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
			CurrentTarget.ApplyDamage(ParentTower, (int) WeaponDamage, DamageType);
			RotateLocal((float)(Math.Atan2(0.5, 0.5) * 180 / Math.PI));
		}

		protected override void EndFire()
		{
			base.EndFire();
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public TDLaserWeaponComponent(Sprite sprite) : base(sprite)
		{
		}
	}
}