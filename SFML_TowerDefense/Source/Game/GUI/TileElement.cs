using SFML_Engine.Engine.JUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_TowerDefense.Source.Game.GUI
{
	public class TileElement : JCheckbox
	{

		public TDTile tile;

		public TileElement(JGUI gui) : base(gui)
		{
		}
	}
}
