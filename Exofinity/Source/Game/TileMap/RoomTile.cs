using SFML.Graphics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Graphics;

namespace Exofinity.Source.Game.TileMap
{
    public class RoomTile : SpriteComponent
    {
        public uint TileID { get; internal set; } = 0;
        public Actor TileActor { get; set; }

        public RoomTile(Sprite sprite) : base(sprite)
        {
        }
    }
}