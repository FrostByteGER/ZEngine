using System.Diagnostics;
using ZEngine.Engine.IO;
using ZEngine.Engine.Localization;
using ZEngine.Engine.Messaging;
using Debug = ZEngine.Engine.Utility.Debug;

namespace ZEngine.Engine.Services
{
    public class Bootstrap
    {
        protected internal virtual void Setup()
        {
            Debug.PrintToConsole = true;
            ServiceLocator.RegisterService<IAssetManager>(new AssetManager());
            ServiceLocator.RegisterService<ILocalizationManager>(new LocalizationManager());
            ServiceLocator.RegisterService<IMessageBus>(new MessageBus());
        }
    }
}