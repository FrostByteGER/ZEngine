namespace ZEngine.Engine.Services.Locator
{
    /// <summary>
    /// ServiceLocator for engine services.
    /// </summary>
    public class EngineServiceLocator : AbstractServiceLocator<IEngineService>
    {
        
        private static EngineServiceLocator Instance { get; } = new();

        public static T GetService<T>(string id = null) where T : IEngineService
        {
            return Instance.BaseGetService<T>(id);
        }

        public static void RegisterService<T>(IEngineService service, string id = null) where T : IEngineService
        {
            Instance.BaseRegisterService<T>(service, id);
        }

        public static void UnregisterService<T>(string id = null) where T : IEngineService
        {
            Instance.BaseUnregisterService<T>(id);
        }
        
        public static void InitializeServices()
        {
            Instance.BaseInitializeServices();
        }

        public static void DeinitializeServices()
        {
            Instance.BaseDeinitializeServices();
        }
    }
}