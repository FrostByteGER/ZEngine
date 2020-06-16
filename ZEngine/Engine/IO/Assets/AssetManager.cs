using System;
using ZEngine.Engine.Game.Level;

namespace ZEngine.Engine.IO.Assets
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

        public AssetManager(IAssetRegistry registry)
		{
            Registry = registry;
        }

        public void Initialize()
        {
            
        }

        public void Deinitialize()
        {
            
        }

		public void Init()
		{
            Registry.EstablishConnection();
		}

        /// <summary>
		/// NOTE: This is pure abuse of Generics and is used only as a convenience for Casting. I may delete this in the future and use LoadXXX methods instead.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="assetName"></param>
		public virtual T LoadAsset<T>(string assetName)
		{
			throw new NotImplementedException();
		}

        public T LoadLevel<T>(string levelName) where T : Level
		{
            throw new NotImplementedException();
			//return JSONManager.LoadObject<T>(LevelsPath + levelName);
		}
    }
}