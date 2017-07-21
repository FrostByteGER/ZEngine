using System;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;

namespace SFML_TowerDefense.Source.Game.Buildings
{
	public class TDNexus : TDBuilding
	{

		public uint NexusID { get; set; } = 0;
		public TDNexus(Level level) : base(level)
		{
			var nexusSprite = new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("Nexus")));
			SetRootComponent(nexusSprite);
			Origin = nexusSprite.Origin;
		}


		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}