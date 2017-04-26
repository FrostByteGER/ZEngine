namespace SFML_Engine.Engine.IO
{
	public class ConfigReader
	{
		public static string AssetsPath { get; } = "Assets/";
		public static string CorePackagesFileName = "Packages.cfg";
		public static string EngineConfigFolderPath { get; } = AssetsPath + "Engine/Config/";
		public static string MainEngineConfigFilePath { get; } = EngineConfigFolderPath + CorePackagesFileName;
		public static string GameConfigFolderPath { get; } = AssetsPath + "SFML_Pong";
		public static string MainGameConfigFilePath { get; } = GameConfigFolderPath + CorePackagesFileName;

	}
}