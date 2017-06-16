using System.Collections.Generic;

namespace SFML_Engine.Engine.IO
{
	public static class ConfigManager
	{

		public static string EngineConfigFolderPath { get; } = AssetManager.GameAssetsPath + "Engine/Config/";
		public static string GameConfigFolderPath { get; } = AssetManager.GameAssetsPath + "Configs/";
		public static string EngineSchemaPaths { get; } = EngineConfigFolderPath + "Schemas/";
		public static string ConfigSchemaPath { get; } = EngineSchemaPaths + "Config.jsonschema";

		public static Config LoadConfig(string configName)
		{
			return LoadConfig(configName, true);
		}

		public static Config LoadConfig(string configName, bool validate)
		{
			var dict = LoadConfigAsDict(configName, validate);
			return new Config(dict);
		}

		public static Config LoadConfigAbsolute(string configPath, bool validate)
		{
			var dict = LoadConfigAsDictAbsolute(configPath, validate);
			return new Config(dict);
		}

		public static Dictionary<string,string> LoadConfigAsDict(string configName)
		{
			return LoadConfigAsDict(configName, true);
		}

		public static Dictionary<string, string> LoadConfigAsDict(string configName, bool validate)
		{
			return JSONManager.LoadObject<Dictionary<string, string>>(GameConfigFolderPath + configName, validate);
		}

		public static Dictionary<string, string> LoadConfigAsDictAbsolute(string configPath, bool validate)
		{
			return JSONManager.LoadObject<Dictionary<string, string>>(configPath, validate);
		}

		public static void SaveConfig(string configName, Dictionary<string, string> data)
		{
			JSONManager.SaveObject(configName, data);
		}
	}
}