using System.Collections.Generic;

namespace SFML_TowerDefense.Source.Game
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