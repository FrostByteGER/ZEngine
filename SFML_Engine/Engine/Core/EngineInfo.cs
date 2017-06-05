namespace SFML_Engine.Engine.Core
{
	public sealed class EngineInfo
	{
		public static string EngineName          = SFML_Engine.EngineInfo.Default.EngineName;
		public static string EngineVersionPrefix = SFML_Engine.EngineInfo.Default.EngineVersionPrefix;
		public static string EngineVersionSuffix = SFML_Engine.EngineInfo.Default.EngineVersionSuffix;

		public static uint EngineMajorVersion  = SFML_Engine.EngineInfo.Default.EngineMajorVersion;
		public static uint EngineMinorVersion  = SFML_Engine.EngineInfo.Default.EngineMinorVersion;
		public static uint EngineHotfixVersion = SFML_Engine.EngineInfo.Default.EngineHotfixVersion;

		public static string EngineFullName = SFML_Engine.EngineInfo.Default.EngineName + " " + SFML_Engine.EngineInfo.Default.EngineVersionPrefix + SFML_Engine.EngineInfo.Default.EngineMajorVersion + 
												"." + SFML_Engine.EngineInfo.Default.EngineMinorVersion + "." + SFML_Engine.EngineInfo.Default.EngineHotfixVersion + 
												SFML_Engine.EngineInfo.Default.EngineVersionSuffix;

	}
}