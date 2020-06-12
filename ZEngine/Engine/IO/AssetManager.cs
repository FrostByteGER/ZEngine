using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ZEngine.Engine.Audio;
using ZEngine.Engine.Game;
using ZEngine.Engine.Game.Level;
using ZEngine.Engine.Rendering;

namespace ZEngine.Engine.IO
{
	public class AssetManager : IAssetManager
    {

        public IAssetRegistry Registry { get; }

		// Names
		public static string EngineFolderName { get; protected internal set; } = "Engine";
		public static string GameFolderName { get; protected internal set; } = "Game";
		public static string PackagesFileName { get; protected internal set; } = "Packages.cfg";
		public static string TextureFolderName { get; protected internal set; } = "Textures";
		public static string SoundFolderName { get; protected internal set; } = "Sounds";
		public static string ConfigFolderName { get; protected internal set; } = "Configs";
		public static string FontFolderName { get; protected internal set; } = "Fonts";
		public static string ShaderFolderName { get; protected internal set; } = "Shaders";
		public static string LevelFolderName { get; protected internal set; } = "Levels";

		// Game Paths
		public static string GameAssetsPath { get; protected internal set; } = "Assets/" + GameFolderName + "/";
		public static string LevelsPath { get; protected internal set; } = GameAssetsPath + LevelFolderName + "/"; // TODO: Remove or Set at Runtime after Core Package has been loaded
		public static string CorePackagesPath { get; protected internal set; } = GameAssetsPath + PackagesFileName;

		// Engine Paths
		public static string EngineAssetsPath { get; protected internal set; } = "Assets/" + EngineFolderName + "/";
		public static string CoreEnginePackagesPath { get; protected internal set; } = GameAssetsPath + PackagesFileName;



		// Engine Assets and Packages
		private Dictionary<string, Dictionary<string, string>> _engineAssets = new Dictionary<string, Dictionary<string, string>>();
		public ReadOnlyDictionary<string, Dictionary<string, string>> EngineAssets => new ReadOnlyDictionary<string, Dictionary<string, string>>(_gameAssets);

		private Dictionary<string, string> _enginePackages = new Dictionary<string, string>();
		public ReadOnlyDictionary<string, string> EnginePackages => new ReadOnlyDictionary<string, string>(_enginePackages);

		// Game Assets and Packages
		private Dictionary<string, Dictionary<string, string>> _gameAssets = new Dictionary<string, Dictionary<string, string>>();
		public ReadOnlyDictionary<string, Dictionary<string, string>> GameAssets => new ReadOnlyDictionary<string, Dictionary<string, string>>(_gameAssets);

		private Dictionary<string, string> _gamePackages = new Dictionary<string, string>();
		public ReadOnlyDictionary<string, string> GamePackages => new ReadOnlyDictionary<string, string>(_gamePackages);

		// Managers
		private JSONManager JSONManager { get; }
		private TexturePoolManager TextureManager { get; }
		private SoundPoolManager AudioManager { get; }

		public AssetManager()
		{
            Registry = new AssetRegistry();
			JSONManager = new JSONManager();
			TextureManager = new TexturePoolManager();
			AudioManager = new SoundPoolManager();
		}

        public void Initialize()
        {
            
        }

        public void Deinitialize()
        {
            
        }

		public void InitPackages()
		{
			_gamePackages = ConfigManager.LoadConfigAsDictAbsolute(CorePackagesPath, false);
			var packages = new KeyValuePair<string, string>[_gamePackages.Count];

			for (var i = 0; i < _gamePackages.Count; ++i)
			{
				packages[i] = _gamePackages.ElementAt(i);
			}

			foreach (var package in packages)
			{
				var assetPackagePath = GameAssetsPath + package.Value + "/" + PackagesFileName;
				var assetPackage = ConfigManager.LoadConfigAsDictAbsolute(assetPackagePath, false);
				_gameAssets.Add(package.Value, assetPackage);
			}

            Registry.EstablishAssetRegistryConnection();
		}

        public void ClearPools()
        {
            TextureManager.ClearPool();
            AudioManager.ClearPool();
        }

		/// <summary>
		/// NOTE: This is pure abuse of Generics and is used only as a convenience for Casting. I may delete this in the future and use LoadXXX methods instead.
		/// TODO: This needs a complete rewrite of the AssetManagers Assets variable. Instead of saving a dict in a dict, save the dict with direct assetpaths. Then in this method, check its extension and delegate to the appropiate Manager and return the loaded asset.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="assetName"></param>
		public virtual T LoadAsset<T>(string assetName)
		{
			Dictionary<string, string> assets;
			string name;

			var assetType = typeof(T);
			if (assetType == typeof(Texture))
			{
				_gameAssets.TryGetValue("Textures", out assets);
				if (assets == null) return default;
				assets.TryGetValue(assetName, out name);
				return name == null ? default : (T)(object)TextureManager.LoadTexture(GameAssetsPath + "Textures/" + name);
			}
			if (assetType == typeof(Sound))
			{
				_gameAssets.TryGetValue("SFX", out assets);
				if (assets == null) return default;
				assets.TryGetValue(assetName, out name);
				return name == null ? default : (T)(object)AudioManager.LoadSound(name);
			}
			if (assetType == typeof(SoundBuffer))
			{
				_gameAssets.TryGetValue("SFX", out assets);
			    if (assets == null) return default;
				assets.TryGetValue(assetName, out name);
				return name == null ? default : (T)(object)AudioManager.LoadSoundBuffer(name);
			}
			if (assetType == typeof(Font))
			{
				//TODO: Add Font Management
				return default;
			}
			if (assetType == typeof(Shader))
			{
				//TODO: Add Shader Management
				return default;
			}
			return default;
		}

		public Texture LoadTexture(string assetName)
		{
		    _gamePackages.TryGetValue(TextureFolderName, out var textureFolder);
			if (textureFolder == null) return null;
		    _gameAssets.TryGetValue(textureFolder, out var textureAssets);
			if (textureAssets == null) return null;
		    textureAssets.TryGetValue(assetName, out var textureName);
			return string.IsNullOrEmpty(textureName) ? null : TextureManager.LoadTexture(GameAssetsPath + textureFolder + "/" + textureName);
		}

		public Sound LoadSound(string assetName)
		{
		    _gamePackages.TryGetValue(SoundFolderName, out var soundFolder);
			if (soundFolder == null) return null;
		    _gameAssets.TryGetValue(soundFolder, out var soundAssets);
			if (soundAssets == null) return null;
		    soundAssets.TryGetValue(assetName, out var soundName);
			return string.IsNullOrEmpty(soundName) ? null : AudioManager.LoadSound(GameAssetsPath + soundFolder + "/" + soundName);
		}

		public SoundBuffer LoadSoundBuffer(string assetName)
		{
		    _gamePackages.TryGetValue(SoundFolderName, out var soundFolder);
			if (soundFolder == null) return null;
		    _gameAssets.TryGetValue(soundFolder, out var soundAssets);
			if (soundAssets == null) return null;
		    soundAssets.TryGetValue(assetName, out var soundName);
			return string.IsNullOrEmpty(soundName) ? null : AudioManager.LoadSoundBuffer(GameAssetsPath + soundFolder + "/" + soundName);
		}

		public Music LoadMusic(string assetName)
		{
		    _gamePackages.TryGetValue(SoundFolderName, out var musicFolder);
			if (musicFolder == null) return null;
		    _gameAssets.TryGetValue(musicFolder, out var musicAssets);
			if (musicAssets == null) return null;
		    musicAssets.TryGetValue(assetName, out var musicName);
			return string.IsNullOrEmpty(musicName) ? null : AudioManager.LoadMusic(GameAssetsPath + musicFolder + "/" + musicName);
		}

		public Font LoadFont(string assetName)
		{
		    _gamePackages.TryGetValue(FontFolderName, out var fontFolder);
			if (fontFolder == null) return null;
		    _gameAssets.TryGetValue(fontFolder, out var fontAssets);
			if (fontAssets == null) return null;
		    fontAssets.TryGetValue(assetName, out var fontName);
			return string.IsNullOrEmpty(fontName) ? null : new Font(GameAssetsPath + fontFolder + "/" + fontName);
		}

		public Shader LoadShader(string assetName)
		{
			return null;
		}

		public Config LoadConfig(string assetName)
		{
		    _gamePackages.TryGetValue(ConfigFolderName, out var configFolder);
			if (configFolder == null) return null;
		    _gameAssets.TryGetValue(configFolder, out var configAssets);
			if (configAssets == null) return null;
		    configAssets.TryGetValue(assetName, out var configName);
			return string.IsNullOrEmpty(configName) ? null : ConfigManager.LoadConfig(GameAssetsPath + configFolder + "/" + configName);
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