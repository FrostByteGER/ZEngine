using System.Collections.Generic;

namespace ZEngine.Engine.IO.Configs
{
	public class ConfigManager : IConfigManager
	{
        public void Initialize()
        {

        }

        public void Deinitialize()
        {

        }

		public static Config LoadConfig(string configName)
		{
			return LoadConfig(configName, true);
		}

		public static Config LoadConfig(string configName, bool validate)
		{
			var dict = LoadConfigAsDict(configName, validate);
			return new Config(dict);
		}

        public static Dictionary<string, string> LoadConfigAsDict(string configName, bool validate)
		{
			return JSONManager.LoadObject<Dictionary<string, string>>(configName, validate);
		}

        public static void SaveConfig(string configName, Dictionary<string, string> data)
		{
			JSONManager.SaveObject(configName, data);
		}
    }
}