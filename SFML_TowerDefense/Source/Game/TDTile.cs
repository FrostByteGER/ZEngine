using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;

namespace SFML_TowerDefense.Source.Game
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
