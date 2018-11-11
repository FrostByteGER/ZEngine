using System.Collections.Generic;
using Exofinity.Source.Game.Core;
using SFML.Graphics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;

namespace Exofinity.Source.Game.TileMap
{
	public class TDMap : TDActor
	{
		public List<TDTile> Tiles { get; set; }
		public List<Sprite> TileSprites { get; set; }
		public int SizeX { get; internal set; } = 0;
		public int SizeY { get; internal set; } = 0;
		public int TileSizeX { get; internal set; } = 0;
		public int TileSizeY { get; internal set; } = 0;
		public int GameSizeX => SizeX * TileSizeX;
		public int GameSizeY => SizeY * TileSizeY;

		public override TVector2f ActorBounds { get; set; }

		public override TVector2f Origin { get; set; }


		public TDMap(string levelName ,Level level) : base(level)
		{

		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}
