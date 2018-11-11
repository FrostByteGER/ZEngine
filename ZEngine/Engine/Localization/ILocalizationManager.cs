using System.Collections.Generic;

namespace ZEngine.Engine.Localization
{
    public interface ILocalizationManager
    {
        string GetCurrentLanguage();
        void SetCurrentLanguage();
        string Get(string key);
        string[] GetMany(IEnumerable<string> keys);
    }
}