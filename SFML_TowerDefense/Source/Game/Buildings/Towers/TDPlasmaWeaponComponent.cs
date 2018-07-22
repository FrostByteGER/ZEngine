using System;
using SFML.Audio;
using SFML.Graphics;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;
using SFML_Roguelike.Source.Game.Core;
using SFML_Roguelike.Source.Game.Units;

namespace SFML_Roguelike.Source.Game.Buildings.Towers
{
	public class TDPlasmaWeaponComponent : TDTowerWeaponComponent
	{

		public TDPlasmaWeaponComponent(Sprite sprite) : base(sprite)
		{
			DamageType = TDDamageType.Plasma;
			WeaponDamage = 5f;
			RechargeTime = 1.5f;
		}

		protected override void OnInitializeActorComponent()
		{
			base.OnInitializeActorComponent();
			FireSound = ParentActor.LevelReference.EngineReference.AssetManager.LoadSound("PlasmaFire");
			FireSound.Volume = ParentActor.LevelReference.EngineReference.GlobalSoundVolume;
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
			var projectile = ParentActor.LevelReference.SpawnActor<TDPlasmaProjectile>();
			projectile.Target = CurrentTarget;
			projectile.Position = ParentActor.Position;

			EndFire();
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