namespace ZEngine.Engine.Services.Locator
{
    /// <summary>
    /// ServiceLocator for engine services.
    /// </summary>
    public class EngineServiceLocator : AbstractServiceLocator<IEngineService>
    {

        public T GetService<T>(string id = null) where T : IEngineService
        {
            return BaseGetService<T>(id);
        }

        public void RegisterService<T>(IEngineService service, string id = null) where T : IEngineService
        {
            BaseRegisterService<T>(service, id);
        }

        public void UnregisterService<T>(string id = null) where T : IEngineService
        {
            BaseUnregisterService<T>(id);
        }
    }
}