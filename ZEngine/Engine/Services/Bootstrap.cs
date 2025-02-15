using ZEngine.Engine.Game;
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
        internal void SetupInternal()
        {
            Log.PrintToConsole = true;
            EngineServiceLocator.RegisterService<IClock>(new SilkEngineClock());
            var assetRegistry = new AssetRegistry();
            EngineServiceLocator.RegisterService<IAssetRegistry>(assetRegistry);
            var assetManager = new AssetManager(assetRegistry);
            EngineServiceLocator.RegisterService<IAssetManager>(assetManager);
            EngineServiceLocator.RegisterService<ILocalizationManager>(new LocalizationManager());
            var engineMessageBus = new MessageBus();
            EngineServiceLocator.RegisterService<IMessageBus>(engineMessageBus);
            var windowManager = new SilkWindowManager();
            EngineServiceLocator.RegisterService<IWindowManager>(windowManager);
            EngineServiceLocator.RegisterService<IInputManager>(new SilkInputManager(engineMessageBus, windowManager));
            EngineServiceLocator.RegisterService<ILevelManager>(new LevelManager(engineMessageBus, assetManager));
            Setup();
            InitializeServices();
        }

        protected virtual void Setup()
        {

        }

        private void InitializeServices()
        {
            EngineServiceLocator.InitializeServices();
        }
    }
}