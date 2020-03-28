using System.Collections.Generic;
using ZEngine.Engine.Services;

namespace ZEngine.Engine.Localization
{
    public interface ILocalizationManager : IEngineService
    {
        string GetCurrentLanguage();
        void SetCurrentLanguage();
        string Get(string key);
        string[] GetMany(IEnumerable<string> keys);
    }
}