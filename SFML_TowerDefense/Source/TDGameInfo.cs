namespace SFML_Roguelike.Source
{
	public class TDGameInfo : SFML_Engine.Engine.Game.GameInfo
	{
		public override string GameName { get; protected set; }              = GameInfo.Default.GameName;
		public override string GameVersionPrefix { get; protected set; }     = GameInfo.Default.GameVersionPrefix;
		public override string GameVersionSuffix { get; protected set; }     = GameInfo.Default.GameVersionSuffix;
		public override uint GameMajorVersion { get; protected set; }        = GameInfo.Default.GameMajorVersion;
		public override uint GameMinorVersion { get; protected set; }        = GameInfo.Default.GameMinorVersion;
		public override uint GameHotfixVersion { get; protected set; }       = GameInfo.Default.GameHotfixVersion;
		public override string GameTextureFolderName { get; protected set; } = GameInfo.Default.TextureFolder;
		public override string GameSoundFolderName { get; protected set; }   = GameInfo.Default.SoundFolder;
		public override string GameConfigFolderName { get; protected set; }  = GameInfo.Default.ConfigFolder;
		public override string GameFontFolderName { get; protected set; }    = GameInfo.Default.FontFolder;
		public override string GameShaderFolderName { get; protected set; }  = GameInfo.Default.ShaderFolder;
		public override string GameLevelFolderName { get; protected set; }   = GameInfo.Default.LevelFolder;
	}
}