using System.Collections.Generic;
using SFML.Graphics;
using SFML_Engine.Engine.Graphics;

namespace SFML_Roguelike.Source.Game.TileMap
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
