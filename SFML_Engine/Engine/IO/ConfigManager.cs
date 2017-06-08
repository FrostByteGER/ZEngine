namespace SFML_Engine.Engine.IO
{
	public class ConfigManager : GenericIOManager
	{

		public static string EngineConfigFolderPath { get; } = AssetManager.AssetsPath + "Engine/Config/";
		public static string GameConfigFolderPath { get; } = AssetManager.AssetsPath + "SFML_Pong";
		public override T Load<T>(string path)
		{
			throw new System.NotImplementedException();
		}

		public override void Save<T>(string path, T data)
		{
			throw new System.NotImplementedException();
		}
	}
}