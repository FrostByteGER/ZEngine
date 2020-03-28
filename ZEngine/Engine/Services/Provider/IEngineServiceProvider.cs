namespace ZEngine.Engine.Services.Provider
{
    internal interface IEngineServiceProvider
    {
        T GetService<T>(string id = null) where T : IEngineService;
    }
}