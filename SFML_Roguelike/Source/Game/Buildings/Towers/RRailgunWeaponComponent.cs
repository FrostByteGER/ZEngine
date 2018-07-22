using System;
using SFML.Graphics;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;
using SFML_Roguelike.Source.Game.Core;
using SFML_Roguelike.Source.Game.Units;

namespace SFML_Roguelike.Source.Game.Buildings.Towers
{
	public class RRailgunWeaponComponent : RTowerWeaponComponent
	{

		public RRailgunWeaponComponent(Sprite sprite) : base(sprite)
		{
			DamageType = RDamageType.Kinetic;
			WeaponDamage = 5f;
			RechargeTime = .5f;
		}

		protected override void OnInitializeActorComponent()
		{
			base.OnInitializeActorComponent();
			FireSound = ParentActor.LevelReference.EngineReference.AssetManager.LoadSound("RailgunFire");
			FireSound.Volume = ParentActor.LevelReference.EngineReference.GlobalSoundVolume;
		}

		protected override void OnEnemyLeavesRange(RUnit enemyOutOfRange)
		{
			base.OnEnemyLeavesRange(enemyOutOfRange);
			if (WeaponState == RWeaponState.Firing) EndFire();
		}

		protected override void OnFire()
		{
			CurrentTarget.ApplyDamage((RActor) ParentActor, WeaponDamage, DamageType);

			var projectile = ParentActor.LevelReference.SpawnActor<TDRailgunProjectile>();
			projectile.Target = CurrentTarget;
			projectile.Position = ParentActor.Position;
			EndFire();
		}
	}
}