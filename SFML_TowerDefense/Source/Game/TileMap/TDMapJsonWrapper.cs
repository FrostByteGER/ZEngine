using System.Collections.Generic;

namespace SFML_Roguelike.Source.Game.TileMap
{
	public struct LayerJsonWrapper
	{
		private List<int> Data { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
	}

	public class TDMapJsonWrapper
	{
		public int Height { get; set; }
		public int Width { get; set; }
	}
}