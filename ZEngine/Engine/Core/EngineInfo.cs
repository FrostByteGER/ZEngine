namespace ZEngine.Engine.Core
{
	public sealed class EngineInfo
	{
		public static string EngineName          = ZEngine.EngineInfo.Default.EngineName;
		public static string EngineVersionPrefix = ZEngine.EngineInfo.Default.EngineVersionPrefix;
		public static string EngineVersionSuffix = ZEngine.EngineInfo.Default.EngineVersionSuffix;

		public static uint EngineMajorVersion  = ZEngine.EngineInfo.Default.EngineMajorVersion;
		public static uint EngineMinorVersion  = ZEngine.EngineInfo.Default.EngineMinorVersion;
		public static uint EngineHotfixVersion = ZEngine.EngineInfo.Default.EngineHotfixVersion;

		public static string EngineFullName = ZEngine.EngineInfo.Default.EngineName + " " + ZEngine.EngineInfo.Default.EngineVersionPrefix + ZEngine.EngineInfo.Default.EngineMajorVersion + 
												"." + ZEngine.EngineInfo.Default.EngineMinorVersion + "." + ZEngine.EngineInfo.Default.EngineHotfixVersion + 
												ZEngine.EngineInfo.Default.EngineVersionSuffix;

	}
}