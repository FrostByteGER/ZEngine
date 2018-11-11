using System.Collections.Generic;

namespace Exofinity.Source.Game.TileMap
{
	public struct LayerJsonWrapper
	{
		private List<int> Data { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
	}

	public class RMapJsonWrapper
	{
		public int Height { get; set; }
		public int Width { get; set; }
	}
}