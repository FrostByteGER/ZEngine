using ZEngine.Engine.IO;
using ZEngine.Engine.Localization;
using ZEngine.Engine.Messaging;

namespace ZEngine.Engine.Services
{
    public class Bootstrap
    {
        protected internal virtual void Setup()
        {
            ServiceLocator.RegisterService<IAssetManager>(new AssetManager());
            ServiceLocator.RegisterService<ILocalizationManager>(new LocalizationManager());
            ServiceLocator.RegisterService<IMessageBus>(new MessageBus());
        }
    }
}