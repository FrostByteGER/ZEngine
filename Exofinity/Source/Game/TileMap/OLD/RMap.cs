using System.Collections.Generic;
using Exofinity.Source.Game.Core;
using SFML.Graphics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;

namespace Exofinity.Source.Game.TileMap
{
    public class RMapConfiguration : BaseConfiguration
    {
        public List<RTile> Tiles { get; set; }
        public List<Sprite> TileSprites { get; set; }
        public int SizeX { get; internal set; } = 0;
        public int SizeY { get; internal set; } = 0;
        public int TileSizeX { get; internal set; } = 0;
        public int TileSizeY { get; internal set; } = 0;

        protected RMapConfiguration()
        {

        }

        public RMapConfiguration(string id) : base(id)
        {

        }
    }


	public class RMap : RActor
	{
        public RMapConfiguration Config { get; }
		public List<RTile> Tiles { get; set; }
		public List<Sprite> TileSprites { get; set; }
		public int SizeX { get; private set; } = 0;
		public int SizeY { get; private set; } = 0;
		public int TileSizeX { get; private set; } = 0;
		public int TileSizeY { get; private set; } = 0;
		public int GameSizeX => SizeX * TileSizeX;
		public int GameSizeY => SizeY * TileSizeY;

		public override TVector2f ActorBounds { get; set; }

		public override TVector2f Origin { get; set; }


		public RMap(RMapConfiguration config)
        {
            Config = config;
            Tiles = config.Tiles;
            TileSprites = config.TileSprites;
            SizeX = config.SizeX;
            SizeY = config.SizeY;
            TileSizeX = config.TileSizeX;
            TileSizeY = config.TileSizeY;
        }

        protected override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}
