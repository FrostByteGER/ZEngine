using SFML_TowerDefense.Source.Game.Core;
using SFML_TowerDefense.Source.Game.Units;
using System;
using SFML.Graphics;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public class TDLaserWeaponComponent : TDTowerWeaponComponent
	{

		public SpriteComponent Laser;

		public TDLaserWeaponComponent(Sprite sprite) : base(sprite)
		{
			WeaponDamage = 2;
			RechargeTime = 0.01f;
		}

		protected override void OnEnemyLeavesRange(TDUnit enemyOutOfRange)
		{
			base.OnEnemyLeavesRange(enemyOutOfRange);
			if (WeaponState == TDWeaponState.Firing) EndFire();
		}

		protected override void StartFire()
		{
			base.StartFire();
			ParentActor.AddComponent(Laser);
		}

		protected override void OnInitializeActorComponent()
		{
			base.OnInitializeActorComponent();

		}

		protected override void OnFire()
		{
			Console.WriteLine("OnFire");
			CurrentTarget.ApplyDamage((TDActor)this.ParentActor, WeaponDamage, DamageType);

			Laser.LocalRotation = LocalRotation;
			Laser.Origin = new TVector2f(16,0);
			Laser.LocalScale = new TVector2f(1,4);

			Laser.Visible = true;
		}

		protected override void EndFire()
		{
			base.EndFire();
			ParentActor.RemoveComponent(Laser);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}