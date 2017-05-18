using SFML_Engine.Engine.Game;

namespace SFML_Breakout
{
	public class BreakoutPersistentGameMode : PersistentGameMode
	{
		public uint HighScore { get; set; } = 0;
		public uint AlltimeHighScore { get; set; } = 0;
		public uint CurrentLevel { get; set; } = 1;
		public uint MaxLevels { get; set; } = 1;

	}
}