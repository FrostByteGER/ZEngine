using System;

namespace ZEngine.Engine.IO.Assets
{
    public struct AssetPointer
    {
        /// <summary>
        /// Guid of the Asset
        /// </summary>
        public Guid Guid { get; internal set; }
        /// <summary>
        /// Path of the file where the asset lies in
        /// </summary>
        public string Filepath { get; internal set; }
        /// <summary>
        /// Binary offset inside the file
        /// </summary>
        public long Offset { get; internal set; }
        public long Length { get; internal set; }

        public AssetPointer(Guid guid, string filepath, long offset, long length)
        {
            Guid = guid;
            Filepath = filepath;
            Offset = offset;
            Length = length;
        }
    }
}