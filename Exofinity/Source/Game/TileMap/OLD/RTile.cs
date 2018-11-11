using System.Collections.Generic;
using SFML.Graphics;
using ZEngine.Engine.Graphics;

namespace Exofinity.Source.Game.TileMap
{
	public class RTile : SpriteComponent
	{
		public uint TileID { get; internal set; } = 0;
		public bool Buildable { get; set; } = true;
		public List<RFieldActor> FieldActors { get; set; } = new List<RFieldActor>();

		public RTile(Sprite sprite) : base(sprite)
		{
		}
	}
}
