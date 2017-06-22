using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_TowerDefense.Source.Game
{
	public class TDMap
	{
		public List<List<TDTile>> Tiles;
		public int XSize { get; set; } = 0;
		public int YSize { get; set; } = 0;

		public TDMap()
		{
			Tiles = new List<List<TDTile>>();
		}


	}
}
