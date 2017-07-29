using System;
using SFML.Graphics;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;
using SFML_TowerDefense.Source.Game.Core;
using SFML_TowerDefense.Source.Game.Units;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public class TDRailgunWeaponComponent : TDTowerWeaponComponent
	{

		public TDRailgunWeaponComponent(Sprite sprite) : base(sprite)
		{
			DamageType = TDDamageType.Kinetic;
			WeaponDamage = 5f;
			RechargeTime = .5f;
		}

		protected override void OnInitializeActorComponent()
		{
			base.OnInitializeActorComponent();
			FireSound = ParentActor.LevelReference.EngineReference.AssetManager.LoadSound("RailgunFire");
			FireSound.Volume = ParentActor.LevelReference.EngineReference.GlobalSoundVolume;
		}

		protected override void OnEnemyLeavesRange(TDUnit enemyOutOfRange)
		{
			base.OnEnemyLeavesRange(enemyOutOfRange);
			if (WeaponState == TDWeaponState.Firing) EndFire();
		}

		protected override void OnFire()
		{
			CurrentTarget.ApplyDamage((TDActor) ParentActor, WeaponDamage, DamageType);

			var projectile = ParentActor.LevelReference.SpawnActor<TDRailgunProjectile>();
			projectile.Target = CurrentTarget;
			projectile.Position = ParentActor.Position;
			EndFire();
		}
	}
}