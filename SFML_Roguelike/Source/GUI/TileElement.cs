using SFML_Engine.Engine.JUI;
using SFML_TowerDefense.Source.Game.TileMap;

namespace SFML_TowerDefense.Source.GUI
{
	public class TileElement : JCheckbox
	{

		public TDTile tile;

		public TileElement(JGUI gui) : base(gui)
		{
		}
	}
}
