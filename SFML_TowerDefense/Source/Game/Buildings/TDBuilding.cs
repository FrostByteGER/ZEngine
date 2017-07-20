using SFML_Engine.Engine.Game;

namespace SFML_TowerDefense.Source.Game.Buildings
{
	public class TDBuilding : TDFieldActor
	{
		//TODO: Maybe a Building should not know how expensive it is, rather the GUI or the "Building-Spawner" should know this.
		//public int Cost { get; set; } = 0;

		public uint Health { get; set; } = 1;


		public TDBuilding(Level level) : base(level)
		{
		}
	}
}