using ZEngine.Engine.Services.Locator;

namespace ZEngine.Engine.Services
{
    /// <summary>
    /// Interface to be implemented by any interface or class that wants to be globally available via <see cref="GlobalServiceLocator"/>
    /// </summary>
    public interface IGlobalService : IAbstractService
    {

    }
}