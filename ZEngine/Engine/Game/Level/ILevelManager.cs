using System.Diagnostics.CodeAnalysis;
using ZEngine.Engine.Services;

namespace ZEngine.Engine.Game.Level
{
    public interface ILevelManager : IEngineService, ITickable
    {
        ulong LevelIDCounter { get; set; }

        void LoadLevel([NotNull] Level level);
        bool LoadLevel([NotNull] string levelName);
    }
}