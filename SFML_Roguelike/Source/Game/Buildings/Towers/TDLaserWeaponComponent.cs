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

		public SpriteComponent LaserSprite;

		public float FiringTime { get; set; }
		public float CurrentFiringTime { get; set; }
		public bool ContinuousBeam { get; set; } = false;
		public float DeltaTime { get; set; } = 0.0f;

		public TDLaserWeaponComponent(Sprite sprite) : base(sprite)
		{
			DamageType = TDDamageType.Laser;
			WeaponDamageBase = .5f;
			WeaponDamage = WeaponDamageBase;
			RechargeTime = .5f;
			FiringTime = .5f;
			CurrentFiringTime = FiringTime;

		}

		protected override void OnInitializeActorComponent()
		{
			base.OnInitializeActorComponent();
			LaserSprite.Sprite.Color = new Color(0, 162, 232);
			FireSound = ParentActor.LevelReference.EngineReference.AssetManager.LoadSound("LaserFire");
			FireSound.Volume = ParentActor.LevelReference.EngineReference.GlobalSoundVolume;
			FireSound.Loop = true;
		}

		protected override void OnEnemyLeavesRange(TDUnit enemyOutOfRange)
		{
			base.OnEnemyLeavesRange(enemyOutOfRange);
			if (WeaponState == TDWeaponState.Firing) EndFire();
		}

		protected override void StartFire()
		{
			base.StartFire();
			ParentActor.AddComponent(LaserSprite);
		}


		protected override void OnFire()
		{
			CurrentTarget.ApplyDamage((TDActor)ParentActor, WeaponDamage, DamageType);

			LaserSprite.LocalRotation = LocalRotation;
			LaserSprite.Origin = new TVector2f(LaserSprite.Origin.X, 0);


			var dicVec = new TVector2f(WorldPosition.X - CurrentTarget.Position.X, WorldPosition.Y - CurrentTarget.Position.Y);

			double dicFloat = dicVec.X * dicVec.X + dicVec.Y * dicVec.Y;

			LaserSprite.LocalScale = new TVector2f(1, (float)Math.Sqrt(dicFloat) / 32f);

			LaserSprite.Visible = true;

			if (ContinuousBeam) return;
			
			CurrentFiringTime -= DeltaTime;
			if (CurrentFiringTime <= 0.0f)
			{
				CurrentFiringTime = FiringTime;
				EndFire();
			}
		}

		protected override void EndFire()
		{
			base.EndFire();
			LaserSprite.Visible = false;
			ParentActor.RemoveComponent(LaserSprite);
			FireSound.Stop();
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			DeltaTime = deltaTime;
		}
	}
}