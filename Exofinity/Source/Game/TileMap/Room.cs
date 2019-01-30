using System.Collections.Generic;
using SFML.Graphics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;

namespace Exofinity.Source.Game.TileMap
{
    public class Room : Actor
    {
        public List<RoomTile> Tiles { get; set; }
        public List<Sprite> TileSprites { get; set; }
        public int SizeX { get; internal set; } = 0;
        public int SizeY { get; internal set; } = 0;
        public int TileSizeX { get; internal set; } = 0;
        public int TileSizeY { get; internal set; } = 0;
        public int GameSizeX => SizeX * TileSizeX;
        public int GameSizeY => SizeY * TileSizeY;

        public override TVector2f ActorBounds { get; set; }

        public override TVector2f Origin { get; set; }

        private uint _tileIdCounter;

        public Room()
        {
        }

        public void AddTile(RoomTile tile)
        {
            tile.TileID = _tileIdCounter;
            ++_tileIdCounter;
            Tiles.Add(tile);
        }
    }
}