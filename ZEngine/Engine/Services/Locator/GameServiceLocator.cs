namespace ZEngine.Engine.Services.Locator
{
    public class GameServiceLocator : AbstractServiceLocator<IGameService>
    {
        public T GetService<T>(string id = null) where T : IGameService
        {
            return BaseGetService<T>(id);
        }

        public void RegisterService<T>(IGameService service, string id = null) where T : IGameService
        {
            BaseRegisterService<T>(service, id);
        }

        public void UnregisterService<T>(string id = null) where T : IGameService
        {
            BaseUnregisterService<T>(id);
        }
    }
}