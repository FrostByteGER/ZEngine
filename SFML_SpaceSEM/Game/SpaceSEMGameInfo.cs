namespace SFML_SpaceSEM.Game
{
	public class SpaceSEMGameInfo : SFML_Engine.Engine.Game.GameInfo
	{
		public override string GameName { get; protected set; } = GameInfo.Default.GameName;

		public override string GameVersionPrefix { get; protected set; } = GameInfo.Default.GameVersionPrefix;

		public override string GameVersionSuffix { get; protected set; } = GameInfo.Default.GameVersionSuffix;

		public override uint GameMajorVersion { get; protected set; } = GameInfo.Default.GameMajorVersion;

		public override uint GameMinorVersion { get; protected set; } = GameInfo.Default.GameMinorVersion;

		public override uint GameHotfixVersion { get; protected set; } = GameInfo.Default.GameHotfixVersion;
	}
}