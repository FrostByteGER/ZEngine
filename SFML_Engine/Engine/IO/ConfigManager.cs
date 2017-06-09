namespace SFML_Engine.Engine.IO
{
	public class ConfigManager
	{

		public static string EngineConfigFolderPath { get; } = AssetManager.AssetsPath + "Engine/Config/";
		public static string GameConfigFolderPath { get; } = AssetManager.AssetsPath + "SFML_Pong";

		protected virtual T Load<T>(string path)
		{
			throw new System.NotImplementedException();
		}

		protected virtual void Save(string path, string data)
		{
			throw new System.NotImplementedException();
		}
	}
}