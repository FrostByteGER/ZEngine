namespace SFML_Engine.Engine.IO
{
	public class AssetManager
	{

		public static string AssetsPath { get; } = "Assets/";
		public static string CorePackagesFileName = "Packages.cfg";

		public AssetLoader GameAssetLoader { get; set; }
		public ConfigLoader GameConfigLoader { get; set; }
	}
}