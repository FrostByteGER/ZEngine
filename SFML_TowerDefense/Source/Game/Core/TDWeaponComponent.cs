using SFML.Graphics;
using SFML_Engine.Engine.Graphics;
using SFML_Roguelike.Source.Game.Buildings.Towers;

namespace SFML_Roguelike.Source.Game.Core
{
	public class TDWeaponComponent : SpriteComponent
	{
		public TDWeaponType WeaponType { get; set; } = TDWeaponType.Hitscan;
		public float WeaponDamageBase { get; set; } = 10.0f;
		public float WeaponDamage { get; set; } = 10.0f;
		public float FireRate { get; set; } = 1;
		public virtual uint WeaponRange { get; set; } = 100;
		public float RechargeTime { get; set; } = 0.5f;
		public float CurrentRechargeTime { get; set; } = 0.0f;
		public TDDamageType DamageType { get; set; } = TDDamageType.Normal;
		public string WeaponAbility { get; set; } = "NONE";
		public TDWeaponState WeaponState { get; set; } = TDWeaponState.ReadyToFire;

		public TDWeaponComponent(Sprite sprite) : base(sprite)
		{
		}
	}
}