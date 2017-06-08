using System.Collections.Generic;
using SFML.Graphics;
using SFML_Engine.Engine.Game;

namespace SFML_SpaceSEM.Game.Actors
{
	public class SpaceShipPlayer : SpaceShipActor
	{
		public List<WeaponComponent> WeaponSystems { get; set; } = new List<WeaponComponent>();

		public SpaceShipPlayer(Sprite sprite, Level level) : base(sprite, level)
		{
			AddComponent(new WeaponComponent(sprite));
			WeaponSystems.Add(GetComponent<WeaponComponent>(2));
		}

		public void FireWeapons()
		{
			foreach (var weapon in WeaponSystems)
			{
				weapon.OnFire();
			}
		}
	}
}