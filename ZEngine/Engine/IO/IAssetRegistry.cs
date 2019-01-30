using System;
using System.Collections.Generic;

namespace ZEngine.Engine.IO
{
    public interface IAssetRegistry
    {
        void EstablishAssetRegistryConnection();
        string GetAsset(Guid guid);
        string GetAsset(string guid);
        bool AddAsset(string assetPath);
        bool DeleteAsset(Guid guid);
        bool DeleteAsset(string guid);
        int DeleteAssets(IEnumerable<Guid> guids);
        int DeleteAssets(IEnumerable<string> guids);

        /// <summary>
        /// Deletes the given assets from the registry.
        /// </summary>
        /// <param name="guids">List of guids as a comma separated string.</param>
        /// <returns></returns>
        int DeleteAssets(string guids);

        bool MoveAsset(Guid guid, string newPath);
        bool MoveAsset(string guid, string newPath);
    }
}