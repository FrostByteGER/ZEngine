using SFML.Graphics;
using SFML_Roguelike.Source.Game.Units;

namespace SFML_Roguelike.Source.Game.Buildings.Towers
{
	public class RChargedLaserWeaponComponent : RLaserWeaponComponent
	{

		public float WeaponDamageIncrease { get; set; } = .05f;
		public float WeaponDamageIncreaseMax { get; set; } = 0.5f;
		
		public RChargedLaserWeaponComponent(Sprite sprite) : base(sprite)
		{
			ContinuousBeam = true;
		}

		protected override void OnInitializeActorComponent()
		{
			LaserSprite.Sprite.Color = new Color(232, 0, 26);
			FireSound = ParentActor.LevelReference.EngineReference.AssetManager.LoadSound("LaserFireCharged");
			FireSound.Volume = ParentActor.LevelReference.EngineReference.GlobalSoundVolume;
			FireSound.Loop = true;
		}

		public override void OnCurrentTargetSwitched(RUnit oldTarget, RUnit newTarget)
		{
			WeaponDamage = WeaponDamageBase;
		}

		protected override void EndFire()
		{
			base.EndFire();
			WeaponDamage = WeaponDamageBase;
			FireSound.Stop();
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			WeaponDamage += WeaponDamageIncrease;
			if (WeaponDamage >= WeaponDamageBase + WeaponDamageIncreaseMax) WeaponDamage = WeaponDamageBase + WeaponDamageIncreaseMax;
		}
	}
}