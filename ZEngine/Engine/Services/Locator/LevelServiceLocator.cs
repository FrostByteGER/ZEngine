namespace ZEngine.Engine.Services.Locator
{
    internal class LevelServiceLocator : AbstractServiceLocator<ILevelService>
    {
        public T GetService<T>(string id = null) where T : ILevelService
        {
            return BaseGetService<T>(id);
        }

        public void RegisterService<T>(ILevelService service, string id = null) where T : ILevelService
        {
            BaseRegisterService<T>(service, id);
        }

        public void UnregisterService<T>(string id = null) where T : ILevelService
        {
            BaseUnregisterService<T>(id);
        }
    }
}