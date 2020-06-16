using ZEngine.Engine.Game.Level;
using ZEngine.Engine.Services;

namespace ZEngine.Engine.IO.Assets
{
    public interface IAssetManager : IEngineService
    {
        void Init();
        T LoadAsset<T>(string assetName);
        T LoadLevel<T>(string levelName) where T : Level;

    }
}