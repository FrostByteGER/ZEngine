namespace SFML_Engine.Engine.IO
{
	public class ConfigLoader : GenericLoader
	{

		public static string EngineConfigFolderPath { get; } = AssetManager.AssetsPath + "Engine/Config/";
		public static string GameConfigFolderPath { get; } = AssetManager.AssetsPath + "SFML_Pong";
	}
}