using System.Collections.Generic;
using SFML.Graphics;
using SFML_Engine.Engine.Graphics;

namespace SFML_TowerDefense.Source.Game.TileMap
{
	public class TDTile : SpriteComponent
	{
		public uint TileID { get; internal set; } = 0;
		public List<TDFieldActor> FieldActors { get; set; } = new List<TDFieldActor>();

		public TDTile(Sprite sprite) : base(sprite)
		{
		}
	}
}
