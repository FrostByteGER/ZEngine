using SFML_Engine.Engine.Game;

namespace SFML_TowerDefense.Source.Game
{
	public class TDUnit : TDActor
	{

		public int HP { get; set; } = 100;
		public float MovmentSpeed { get; set; } = 1;

		public TDUnit(Level level) : base(level)
		{
		}
	}
}