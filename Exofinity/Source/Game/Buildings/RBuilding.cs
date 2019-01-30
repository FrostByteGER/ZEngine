using Exofinity.Source.Game.TileMap;
using ZEngine.Engine.Game;

namespace Exofinity.Source.Game.Buildings
{
	public class RBuilding : RFieldActor
	{
		//TODO: Maybe a Building should not know how expensive it is, rather the GUI or the "Building-Spawner" should know this.
		public uint Cost { get; set; } = 0;

		public float ScrapMultiplier { get; set; } = 0.75f;

		public uint Health { get; set; } = 1;


		public RBuilding()
		{
		}
	}
}