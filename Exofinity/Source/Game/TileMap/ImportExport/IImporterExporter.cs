namespace Exofinity.Source.Game.TileMap.ImportExport
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPreImport">The type that the data will be converted for checking if it can be actually imported. e.g. version string check</typeparam>
    /// <typeparam name="TInput">The type that the data will be converted to for engine use.</typeparam>
    /// <typeparam name="TOutput">The type that the data will be converted to for external use()</typeparam>
    public interface IImporterExporter<in TPreImport, TInput, TOutput>
    {
        bool PreImport(TPreImport data);
        TOutput Export(TInput data);
        TInput Import(TOutput data);
    }
}