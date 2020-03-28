namespace ZEngine.Engine.Services.Locator
{
    /// <summary>
    /// Global Service Locator. Used for services that need to exist before the creation of a <see cref="Engine"/> instance e.g. Logging
    /// </summary>
    public class GlobalServiceLocator : AbstractServiceLocator<IGlobalService>
    {

        public static GlobalServiceLocator Instance { get; } = new GlobalServiceLocator();

        public static T GetService<T>(string id = null) where T : IGlobalService
        {
            return Instance.BaseGetService<T>(id);
        }

        public static void RegisterService<T>(IGlobalService service, string id = null) where T : IGlobalService
        {
            Instance.BaseRegisterService<T>(service, id);
        }

        public static void UnregisterService<T>(string id = null) where T : IGlobalService
        {
            Instance.BaseUnregisterService<T>(id);
        }
    }
}