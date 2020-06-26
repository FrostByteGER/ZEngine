namespace ZEngine.Engine.IO.Assets
{
    public abstract class Asset<T> where T : AssetMetaData
    {
        public T MetaData { get; private set; }
    }
}