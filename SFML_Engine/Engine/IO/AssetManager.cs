using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.IO
{
	public class AssetManager
	{

		public static string GameProjectName { get; } = "SFML_SpaceSEM";
		public static string AssetsPath { get; } = "Assets/" + GameProjectName + "/";
		public static string LevelsPath { get; } = AssetsPath + "Levels/";
		public static string CorePackagesFileName = "Packages.cfg";

		public JSONManager JSONManager { get; set; }
		public ConfigManager GameConfigManager { get; set; }

		public TexturePoolManager TextureManager { get; }
		public SoundPoolManager AudioManager { get; }

		public AssetManager()
		{
			JSONManager = new JSONManager();
			GameConfigManager = new ConfigManager();
			TextureManager = new TexturePoolManager();
			AudioManager = new SoundPoolManager();
		}


		public T LoadLevelFromFile<T>(string levelName) where T : Level
		{
			return JSONManager.LoadObject<T>(LevelsPath + levelName);
		}

		public T LoadLevelFromFileAbsolute<T>(string levelPath) where T : Level
		{
			return JSONManager.LoadObject<T>(levelPath);
		}

		public void SaveLevelToFile(string levelName, Level level)
		{
			JSONManager.SaveObject(LevelsPath + levelName, level);
		}

		public void SaveLevelToFileAbsolute(string levelPath, Level level)
		{
			JSONManager.SaveObject(levelPath, level);
		}
	}
}