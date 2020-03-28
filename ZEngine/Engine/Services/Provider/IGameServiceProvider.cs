namespace ZEngine.Engine.Services.Provider
{
    public interface IGameServiceProvider
    {
        T GetService<T>(string id = null) where T : IGameService;
    }
}