using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using ZEngine.Engine.Utility;
using Debug = ZEngine.Engine.Utility.Debug;

namespace ZEngine.Engine.IO
{
    public class AssetRegistry : IAssetRegistry
    {
        private SQLiteConnection _registryConnection;

        private const string DatabaseName = "AssetRegistry";
        private const string DatabaseFileExtension = ".db";
        private const string AssetCollectionName = "Assets";

        public void EstablishAssetRegistryConnection()
        {
            if (_registryConnection != null)
            {
                Debug.LogWarning("Connection to Asset Database already established!", DebugCategories.Engine);
                return;
            }

            var dbMissing = File.Exists(DatabaseName + DatabaseFileExtension);

            if(dbMissing)
                Debug.LogWarning("Asset Registry Database does not exist!", DebugCategories.Engine);

            _registryConnection = new SQLiteConnection($"Data Source = {DatabaseName + DatabaseFileExtension}; Version = 3; FailIfMissing = {dbMissing}");
            _registryConnection.Open();

            if (dbMissing)
            {
                //TODO: PRefill with table data!
                Debug.Log("Sucessfully created and connected to Asset Registry Database", DebugCategories.Engine);
            }
            else
            {
                Debug.Log("Sucessfully connected to Asset Registry Database", DebugCategories.Engine);
            }
                
        }

        public string GetAsset(Guid guid)
        {
            return GetAsset(guid.ToString());
        }

        public string GetAsset(string guid)
        {
            var cmd = _registryConnection.CreateCommand();
            cmd.CommandText = "SELECT path FROM @collection WHERE guid = @guid LIMIT 1";
            cmd.Parameters.AddWithValue("@collection", AssetCollectionName);
            cmd.Parameters.AddWithValue("@guid", guid);
            
            return cmd.ExecuteScalar().ToString();
        }

        public bool AddAsset(string assetPath)
        {
            var cmd = _registryConnection.CreateCommand();
            cmd.CommandText = "IF NOT EXISTS (SELECT * FROM @collection WHERE path = @path) INSERT INTO @collection (guid, path) VALUES (?,?)";
            cmd.Parameters.AddWithValue("@collection", AssetCollectionName);
            cmd.Parameters.AddWithValue("@path", assetPath);
            cmd.Parameters.Add(Guid.NewGuid().ToString());
            cmd.Parameters.Add(assetPath);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteAsset(Guid guid)
        {
            return DeleteAsset(guid.ToString());
        }

        public bool DeleteAsset(string guid)
        {
            var cmd = _registryConnection.CreateCommand();
            cmd.CommandText = "DELETE FROM @collection WHERE guid = @guid";
            cmd.Parameters.AddWithValue("@collection", AssetCollectionName);
            cmd.Parameters.AddWithValue("@guid", guid);
            return cmd.ExecuteNonQuery() > 0;
        }

        public int DeleteAssets(IEnumerable<Guid> guids)
        {
            return DeleteAssets(string.Join(",", guids.Select(e => e.ToString())));
        }

        public int DeleteAssets(IEnumerable<string> guids)
        {
            return DeleteAssets(string.Join(",", guids));
        }

        public int DeleteAssets(string guids)
        {
            var cmd = _registryConnection.CreateCommand();
            cmd.CommandText = "DELETE FROM @collection WHERE guid IN (@guids)";
            cmd.Parameters.AddWithValue("@collection", AssetCollectionName);
            cmd.Parameters.AddWithValue("@guids", guids);
            return cmd.ExecuteNonQuery();
        }

        public bool MoveAsset(Guid guid, string newPath)
        {
            return MoveAsset(guid.ToString(), newPath);
        }

        public bool MoveAsset(string guid, string newPath)
        {
            var cmd = _registryConnection.CreateCommand();
            cmd.CommandText = "UPDATE @collection SET path = @path WHERE guid = @guid";
            cmd.Parameters.AddWithValue("@collection", AssetCollectionName);
            cmd.Parameters.AddWithValue("@path", newPath);
            cmd.Parameters.AddWithValue("@guid", guid);
            return cmd.ExecuteNonQuery() > 0;
        }

        internal bool CookDB(string cookPath)
        {
            var fileConnection = new SQLiteConnection($"Data Source = {cookPath + DatabaseName + DatabaseFileExtension}; Version = 3;");
            fileConnection.Open();
            _registryConnection.BackupDatabase(fileConnection, _registryConnection.Database, _registryConnection.Database, -1, null, -1);
            return true;
        }
    }
}