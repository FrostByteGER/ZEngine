using System.Diagnostics.CodeAnalysis;
using ZEngine.Engine.Services;

namespace ZEngine.Engine.Game.Level
{
    public interface ILevelManager_OLD : IEngineService, ITickable
    {
        ulong LevelIDCounter { get; set; }

        void LoadLevel([NotNull] Level_OLD levelOld);
        bool LoadLevel([NotNull] string levelName);
    }
}