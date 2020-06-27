using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using ZEngine.Engine.Utility;
using Debug = ZEngine.Engine.Utility.Debug;

namespace ZEngine.Engine.IO.Assets
{
    public class AssetRegistry : IAssetRegistry
    {
        private SQLiteConnection _registryConnection;

        protected const string DatabaseName = "AssetRegistry";
        private const string DatabaseFileExtension = ".db";
        private const string AssetCollectionName = "Assets";

        public bool AllowNewDatabaseCreation { get; internal set; } = true;

        public void Initialize()
        {
            
        }

        public void Deinitialize()
        {
            
        }

        public void EstablishConnection()
        {
            if (_registryConnection != null)
            {
                Debug.LogError("Connection to Asset Database already established!", DebugLogCategories.Engine);
                return;
            }

            var dbMissing = !File.Exists(DatabaseName + DatabaseFileExtension);

            if (dbMissing)
            {
                Debug.LogWarning("Asset Registry Database does not exist, a new one will be created!", DebugLogCategories.Engine);
            }


            _registryConnection = new SQLiteConnection($"Data Source = {DatabaseName + DatabaseFileExtension}; Version = 3; FailIfMissing = {!AllowNewDatabaseCreation}");
            _registryConnection.Open();

            if (AllowNewDatabaseCreation && dbMissing)
            {
                Debug.Log("Sucessfully created and connected to Asset Registry Database", DebugLogCategories.Engine);
            }
            else
            {
                Debug.Log("Sucessfully connected to Asset Registry Database", DebugLogCategories.Engine);
            }
                
        }

        public AssetPointer GetAsset(Guid guid)
        {
            return GetAsset(guid.ToString());
        }

        public AssetPointer GetAsset(string guid)
        {
            var cmd = _registryConnection.CreateCommand();
            cmd.CommandText = "SELECT path FROM @collection WHERE guid = @guid LIMIT 1";
            cmd.Parameters.AddWithValue("@collection", AssetCollectionName);
            cmd.Parameters.AddWithValue("@guid", guid);
            var ptr = (AssetPointer) cmd.ExecuteScalar();
            return ptr;
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
        // TODO: Extract to ZenForge
        internal bool CookDB(string cookPath)
        {
            var fileConnection = new SQLiteConnection($"Data Source = {cookPath + DatabaseName + DatabaseFileExtension}; Version = 3;");
            fileConnection.Open();
            _registryConnection.BackupDatabase(fileConnection, _registryConnection.Database, _registryConnection.Database, -1, null, -1);
            return true;
        }
    }
}