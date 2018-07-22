using System.Collections.Generic;
using SFML.Graphics;
using SFML_Engine.Engine.Graphics;

namespace SFML_Roguelike.Source.Game.TileMap
{
	public class TDTile : SpriteComponent
	{
		public uint TileID { get; internal set; } = 0;
		public bool Buildable { get; set; } = true;
		public List<TDFieldActor> FieldActors { get; set; } = new List<TDFieldActor>();

		public TDTile(Sprite sprite) : base(sprite)
		{
		}
	}
}
