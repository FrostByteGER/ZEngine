using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SFML.Audio;
using SFML.Graphics;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.IO
{
	public class AssetManager
	{

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
		public JSONManager JSONManager { get; set; }
		public TexturePoolManager TextureManager { get; }
		public SoundPoolManager AudioManager { get; }

		public AssetManager()
		{
			JSONManager = new JSONManager();
			TextureManager = new TexturePoolManager();
			AudioManager = new SoundPoolManager();
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
		}

		/// <summary>
		/// NOTE: This is pure abuse of Generics and is used only as a convenience for Casting. I may delete this in the future and use LoadXXX methods instead.
		/// TODO: This needs a complete rewrite of the AssetManagers Assets variable. Instead of saving a dict in a dict, save the dict with direct assetpaths. Then in this method, check its extension and delegate to the appropiate Manager and return the loaded asset.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="assetName"></param>
		public virtual T LoadAsset<T>(string assetName)
		{
			Dictionary<string, string> assets = null;
			string name = null;

			var assetType = typeof(T);
			if (assetType == typeof(Texture))
			{
				_gameAssets.TryGetValue("Textures", out assets);
				if (assets == null) return default(T);
				assets.TryGetValue(assetName, out name);
				return name == null ? default(T) : (T)(object)TextureManager.LoadTexture(GameAssetsPath + "Textures/" + name);
			}
			if (assetType == typeof(Sound))
			{
				_gameAssets.TryGetValue("SFX", out assets);
				if (assets == null) return default(T);
				assets.TryGetValue(assetName, out name);
				return name == null ? default(T) : (T)(object)AudioManager.LoadSound(name);
			}
			if (assetType == typeof(SoundBuffer))
			{
				_gameAssets.TryGetValue("SFX", out assets);
				if (assets == null) return default(T);
				assets.TryGetValue(assetName, out name);
				return name == null ? default(T) : (T)(object)AudioManager.LoadSoundBuffer(name);
			}
			if (assetType == typeof(Font))
			{
				//TODO: Add Font Management
				return default(T);
			}
			if (assetType == typeof(Shader))
			{
				//TODO: Add Shader Management
				return default(T);
			}
			return default(T);
		}

		public Texture LoadTexture(string assetName)
		{
			string textureFolder = null;
			_gamePackages.TryGetValue(TextureFolderName, out textureFolder);
			if (textureFolder == null) return null;
			Dictionary<string, string> textureAssets = null;
			_gameAssets.TryGetValue(textureFolder, out textureAssets);
			if (textureAssets == null) return null;
			string textureName = null;
			textureAssets.TryGetValue(assetName, out textureName);
			return string.IsNullOrEmpty(textureName) ? null : TextureManager.LoadTexture(GameAssetsPath + textureFolder + "/" + textureName);
		}

		public Sound LoadSound(string assetName)
		{
			string soundFolder = null;
			_gamePackages.TryGetValue(SoundFolderName, out soundFolder);
			if (soundFolder == null) return null;
			Dictionary<string, string> soundAssets = null;
			_gameAssets.TryGetValue(soundFolder, out soundAssets);
			if (soundAssets == null) return null;
			string soundName = null;
			soundAssets.TryGetValue(assetName, out soundName);
			return string.IsNullOrEmpty(soundName) ? null : AudioManager.LoadSound(GameAssetsPath + soundFolder + "/" + soundName);
		}

		public SoundBuffer LoadSoundBuffer(string assetName)
		{
			string soundFolder = null;
			_gamePackages.TryGetValue(SoundFolderName, out soundFolder);
			if (soundFolder == null) return null;
			Dictionary<string, string> soundAssets = null;
			_gameAssets.TryGetValue(soundFolder, out soundAssets);
			if (soundAssets == null) return null;
			string soundName = null;
			soundAssets.TryGetValue(assetName, out soundName);
			return string.IsNullOrEmpty(soundName) ? null : AudioManager.LoadSoundBuffer(GameAssetsPath + soundFolder + "/" + soundName);
		}

		public Font LoadFont(string assetName)
		{
			return null;
		}

		public Shader LoadShader(string assetName)
		{
			return null;
		}

		public Config LoadConfig(string assetName)
		{
			string configFolder = null;
			_gamePackages.TryGetValue(ConfigFolderName, out configFolder);
			if (configFolder == null) return null;
			Dictionary<string, string> configAssets = null;
			_gameAssets.TryGetValue(configFolder, out configAssets);
			if (configAssets == null) return null;
			string configName = null;
			configAssets.TryGetValue(assetName, out configName);
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