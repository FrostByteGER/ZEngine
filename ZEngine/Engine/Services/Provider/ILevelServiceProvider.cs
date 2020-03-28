namespace ZEngine.Engine.Services.Provider
{
    public interface ILevelServiceProvider
    {
        T GetService<T>(string id = null) where T : IEngineService;
    }
}