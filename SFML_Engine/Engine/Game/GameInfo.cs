namespace SFML_Engine.Engine.Game
{
	public class GameInfo
	{
		public virtual string GameName { get; protected set; }  = "MY_GAME";
		public virtual string GameVersionPrefix { get; protected set; } = "v";
		public virtual string GameVersionSuffix { get; protected set; } = "";

		public virtual uint GameMajorVersion { get; protected set; }  = 1;
		public virtual uint GameMinorVersion { get; protected set; } = 0;
		public virtual uint GameHotfixVersion { get; protected set; } = 0;

		//public string GameFullName = GameName + " " + GameVersionPrefix + GameMajorVersion + "." + GameMinorVersion + "." + GameHotfixVersion + GameVersionSuffix;

	}
}