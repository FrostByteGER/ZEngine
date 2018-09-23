using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Messaging;

namespace SFML_Engine.Engine.Services
{
    public abstract class Bootstrap
    {
        protected internal virtual void Setup()
        {
            ServiceLocator.RegisterService<IAssetManager>(new AssetManager());
            ServiceLocator.RegisterService<IMessageBus>(new MessageBus());
        }
    }
}