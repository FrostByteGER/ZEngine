using System.Collections.Generic;

namespace Exofinity.Source.Game.Core
{
    public abstract class BaseConfiguration : IConfiguration
    {
        private static readonly List<BaseConfiguration> AllConfigs = new List<BaseConfiguration>();
        private static readonly Dictionary<string, BaseConfiguration> ConfigsByName = new Dictionary<string, BaseConfiguration>();

        public int Id { get; private set; }

        public string Name { get; private set; }

        protected BaseConfiguration()
        {

        }

        protected BaseConfiguration(string id)
        {
            Register(this, id);
        }

        public static implicit operator int(BaseConfiguration config)
        {
            return config.Id;
        }

        public static implicit operator BaseConfiguration(int id)
        {
            return AllConfigs[id];
        }

        public static implicit operator BaseConfiguration(string name)
        {
            if (ConfigsByName.TryGetValue(name, out var baseConfiguration))
                return baseConfiguration;
            throw new KeyNotFoundException("The key '" + name + "' was not present in the config dictionary.");
        }

        public static bool HasConfiguration(string name)
        {
            return ConfigsByName.ContainsKey(name);
        }

        public static bool HasConfiguration(int id)
        {
            return id < AllConfigs.Count;
        }

        private static void Register(BaseConfiguration config, string id)
        {
            AllConfigs.Add(config);
            config.Id = AllConfigs.Count - 1;
            config.Name = id;
            ConfigsByName[config.Name] = config;
        }

        public static BaseConfiguration GetConfigByName(string name)
        {
            return ConfigsByName[name];
        }

        public static IEnumerable<T> GetConfigsOfType<T>() where T : BaseConfiguration
        {
            foreach (var allConfig in AllConfigs)
            {
                var config = allConfig;
                if (config is T t)
                    yield return t;
            }
        }

        public virtual object CreateDefaultCopy()
        {
            return MemberwiseClone();
        }
    }
}