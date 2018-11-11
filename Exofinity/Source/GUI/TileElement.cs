using Exofinity.Source.Game.TileMap;
using ZEngine.Engine.JUI;

namespace Exofinity.Source.GUI
{
	public class TileElement : JCheckbox
	{

		public RTile tile;

		public TileElement(JGUI gui) : base(gui)
		{
		}
	}
}
