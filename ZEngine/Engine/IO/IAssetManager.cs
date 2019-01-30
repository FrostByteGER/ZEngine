using System.Collections.Generic;
using System.Collections.ObjectModel;
using SFML.Audio;
using SFML.Graphics;
using ZEngine.Engine.Game;

namespace ZEngine.Engine.IO
{
    public interface IAssetManager
    {
        ReadOnlyDictionary<string, Dictionary<string, string>> EngineAssets { get; }
        ReadOnlyDictionary<string, string> EnginePackages { get; }
        ReadOnlyDictionary<string, Dictionary<string, string>> GameAssets { get; }
        ReadOnlyDictionary<string, string> GamePackages { get; }

        IAssetRegistry Registry { get; }

        T LoadAsset<T>(string assetName);
        Texture LoadTexture(string assetName);
        Sound LoadSound(string assetName);
        SoundBuffer LoadSoundBuffer(string assetName);
        Music LoadMusic(string assetName);
        Font LoadFont(string assetName);
        Shader LoadShader(string assetName);
        Config LoadConfig(string assetName);
        T LoadLevelFromFile<T>(string levelName) where T : Level;
        T LoadLevelFromFileAbsolute<T>(string levelPath) where T : Level;
        void SaveLevelToFile(string levelName, Level level);
        void SaveLevelToFileAbsolute(string levelPath, Level level);

    }
}