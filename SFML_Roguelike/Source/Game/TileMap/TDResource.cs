using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;

namespace SFML_TowerDefense.Source.Game.TileMap
{
	public class TDResource : TDFieldActor
	{

		public uint ResourceAmount { get; set; } = 100;

		/// <summary>
		/// Is this Resourcefield depleted or not.
		/// </summary>
		public bool Depleted => ResourceAmount == 0;

		public TDResource(Level level) : base(level)
		{
			var resourceSprite = new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("OreField")));
			SetRootComponent(resourceSprite);
			Origin = resourceSprite.Origin;
		}

		public uint Mine(uint amount)
		{
			

			if (amount >= ResourceAmount)
			{
				var end = ResourceAmount;
				ResourceAmount = 0;
				return end;
			}

			ResourceAmount -= amount;

			return amount;
		}
	}
}