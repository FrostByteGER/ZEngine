using Exofinity.Source.Game.Buildings.Towers;
using SFML.Graphics;
using ZEngine.Engine.Graphics;

namespace Exofinity.Source.Game.Core
{
	public class RWeaponComponent : SpriteComponent
	{
		public TDWeaponType WeaponType { get; set; } = TDWeaponType.Hitscan;
		public float WeaponDamageBase { get; set; } = 10.0f;
		public float WeaponDamage { get; set; } = 10.0f;
		public float FireRate { get; set; } = 1;
		public virtual uint WeaponRange { get; set; } = 100;
		public float RechargeTime { get; set; } = 0.5f;
		public float CurrentRechargeTime { get; set; } = 0.0f;
		public RDamageType DamageType { get; set; } = RDamageType.Normal;
		public string WeaponAbility { get; set; } = "NONE";
		public RWeaponState WeaponState { get; set; } = RWeaponState.ReadyToFire;

		public RWeaponComponent(Sprite sprite) : base(sprite)
		{
		}
	}
}