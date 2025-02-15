using System.Diagnostics.CodeAnalysis;
using ZEngine.Engine.Services;
namespace ZEngine.Engine.Game
{
    public interface ILevelManager : IEngineService, ITickable
    {
        void LoadLevel([NotNull] Level level);
    }
}