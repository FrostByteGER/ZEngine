using ZEngine.Engine.Events;
using ZEngine.Engine.Game.Level;
using ZEngine.Engine.IO.Assets;
using ZEngine.Engine.IO.UserInput;
using ZEngine.Engine.IO.UserInput.Silk;
using ZEngine.Engine.Localization;
using ZEngine.Engine.Messaging;
using ZEngine.Engine.Rendering.Window;
using ZEngine.Engine.Services.Locator;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Services
{
    public class Bootstrap
    {
        internal void SetupInternal(EngineServiceLocator locator)
        {
            Debug.PrintToConsole = true;
            locator.RegisterService<IEngineClock>(new SilkEngineClock());
            var assetRegistry = new AssetRegistry();
            locator.RegisterService<IAssetRegistry>(assetRegistry);
            var assetManager = new AssetManager(assetRegistry);
            locator.RegisterService<IAssetManager>(assetManager);
            locator.RegisterService<ILocalizationManager>(new LocalizationManager());
            var engineMessageBus = new EngineMessageBus();
            locator.RegisterService<IEngineMessageBus>(engineMessageBus);
            var windowManager = new SilkWindowManager();
            locator.RegisterService<IWindowManager>(windowManager);
            locator.RegisterService<IEventManager>(new EventManager(engineMessageBus));
            locator.RegisterService<IInputManager>(new SilkInputManager(engineMessageBus, windowManager));
            locator.RegisterService<ILevelManager>(new LevelManager(engineMessageBus, assetManager));
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