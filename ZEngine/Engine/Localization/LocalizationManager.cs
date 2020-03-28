using System.Collections.Generic;

namespace ZEngine.Engine.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        private LocalizationDatabase _db;

        public LocalizationManager()
        {
            _db = new LocalizationDatabase();
        }

        public string GetCurrentLanguage()
        {
            throw new System.NotImplementedException();
        }

        public void SetCurrentLanguage()
        {
            throw new System.NotImplementedException();
        }

        public string Get(string key)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetMany(IEnumerable<string> keys)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            
        }

        public void Deinitialize()
        {
            
        }
    }

    /// <summary>
    /// Quick class for accessing the Localization. Can be used for simple retrieving of locas. Using the actual ILocalizationManager may be
    /// more efficient if large amounts of localizations are retrieved.
    /// <br/>
    /// NOTE: Do not access this class until the ServiceLocator has initialized the LocalizationManager. Usually this is done in the Bootstrap class!
    /// </summary>
    public class Loca
    {
        /*
        private static ILocalizationManager _localization;
        private static ILocalizationManager Localization => _localization ?? (_localization = GlobalServiceLocator.GetService<ILocalizationManager>());

        public static string Get(string key)
        {
            return Localization.Get(key);
        }

        public static string[] GetMany(IEnumerable<string> keys)
        {
            return Localization.GetMany(keys);
        }
        */
    }
}