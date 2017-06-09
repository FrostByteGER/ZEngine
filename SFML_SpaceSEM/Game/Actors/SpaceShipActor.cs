using System.Collections.Generic;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;

namespace SFML_SpaceSEM.Game.Actors
{
	public class SpaceShipActor : SpriteActor
	{
		public uint Healthpoints { get; set; } = 1;
		public uint MaxHealthpoints { get; set; } = 5;
		public List<WeaponComponent> WeaponSystems { get; set; } = new List<WeaponComponent>();


		public SpaceShipActor(Sprite sprite, Level level) : base(sprite, level)
		{
		}

		public virtual void FireWeapons()
		{
			foreach (var weapon in WeaponSystems)
			{
				weapon.OnFire();
			}
		}
	}
}