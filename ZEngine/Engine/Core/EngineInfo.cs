namespace ZEngine.Engine.Core
{
	public sealed class EngineInfo
	{
		public static readonly string EngineName          = ZEngine.EngineInfo.Default.EngineName;
		public static readonly string EngineVersionPrefix = ZEngine.EngineInfo.Default.EngineVersionPrefix;
		public static readonly string EngineVersionSuffix = ZEngine.EngineInfo.Default.EngineVersionSuffix;

		public static readonly uint EngineMajorVersion  = ZEngine.EngineInfo.Default.EngineMajorVersion;
		public static readonly uint EngineMinorVersion  = ZEngine.EngineInfo.Default.EngineMinorVersion;
		public static readonly uint EngineHotfixVersion = ZEngine.EngineInfo.Default.EngineHotfixVersion;

		public static readonly string EngineFullName = ZEngine.EngineInfo.Default.EngineName + " " + ZEngine.EngineInfo.Default.EngineVersionPrefix + ZEngine.EngineInfo.Default.EngineMajorVersion + 
		                                              "." + ZEngine.EngineInfo.Default.EngineMinorVersion + "." + ZEngine.EngineInfo.Default.EngineHotfixVersion + 
		                                              ZEngine.EngineInfo.Default.EngineVersionSuffix;

	}
}