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

		public virtual string GameTextureFolderName { get; protected set; } = "Textures";
		public virtual string GameSoundFolderName { get; protected set; } = "Sounds";
		public virtual string GameConfigFolderName { get; protected set; } = "Configs";
		public virtual string GameFontFolderName { get; protected set; } = "Fonts";
		public virtual string GameShaderFolderName { get; protected set; } = "Shaders";
		public virtual string GameLevelFolderName { get; protected set; } = "Levels";

		public string GameFullName = "GENERATE_ME";

		public virtual string GenerateFullGameName()
		{
			GameFullName = GameName + " " + GameVersionPrefix + GameMajorVersion + "." + GameMinorVersion + "." + GameHotfixVersion + GameVersionSuffix;
			return GameFullName;
		}

	}
}