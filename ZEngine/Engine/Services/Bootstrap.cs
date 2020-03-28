using ZEngine.Engine.Events;
using ZEngine.Engine.IO;
using ZEngine.Engine.Localization;
using ZEngine.Engine.Messaging;
using ZEngine.Engine.Services.Locator;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Services
{
    public class Bootstrap
    {
        internal void SetupInternal(EngineServiceLocator locator)
        {
            Debug.PrintToConsole = true;
            locator.RegisterService<IAssetManager>(new AssetManager());
            locator.RegisterService<ILocalizationManager>(new LocalizationManager());
            var engineMessageBus = new EngineMessageBus();
            locator.RegisterService<IEngineMessageBus>(engineMessageBus);
            locator.RegisterService<IEventManager>(new EventManager(engineMessageBus));
            locator.RegisterService<IInputManager>(new InputManager(engineMessageBus));
            Setup(locator);
            InitializeServices(locator);
        }

        protected virtual void Setup(EngineServiceLocator locator)
        {

        }

        private void InitializeServices(EngineServiceLocator locator)
        {
            locator.InitializeServices();
        }
    }
}