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

        public void InitializeService()
        {
            
        }

        public void DeinitializeService()
        {
            
        }
    }
}