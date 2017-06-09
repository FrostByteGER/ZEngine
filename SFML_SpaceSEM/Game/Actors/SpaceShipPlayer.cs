using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using SFML_SpaceSEM.Game.Players;

namespace SFML_SpaceSEM.Game.Actors
{
	public class SpaceShipPlayer : SpaceShipActor
	{

		public SpaceGamePlayerController ControllerRef { get; set; } = null;

		public SpaceShipPlayer(Sprite sprite, Level level) : base(sprite, level)
		{
			MaxVelocity = new TVector2f(600);
			var weapon1 = new WeaponComponent(new Sprite());
			var weapon2 = new WeaponComponent(new Sprite());
			weapon1.LocalPosition += new TVector2f(26.0f, 15.0f);
			weapon2.LocalPosition += new TVector2f(-26.0f, 15.0f);
			AddComponent(weapon1);
			AddComponent(weapon2);
			WeaponSystems.Add(weapon1);
			WeaponSystems.Add(weapon2);
		}

		public override void FireWeapons()
		{
			foreach (var weapon in WeaponSystems)
			{
				weapon.OnFire();
			}
		}
	}
}