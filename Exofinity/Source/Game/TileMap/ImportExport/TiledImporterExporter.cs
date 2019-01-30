using System;
using System.Globalization;
using System.Linq;

namespace Exofinity.Source.Game.TileMap.ImportExport
{
    public class TiledImporterExporter<TPreImport, TInput, TOutput> : IImporterExporter<TPreImport, TInput, TOutput> where TPreImport : BaseMap where TInput : RMapConfiguration where TOutput : Map
    {

        public static readonly Version[] AllowedVersions = {new Version("1.0") };
        public bool PreImport(TPreImport data)
        {
            var dataVersion = new Version(data.Version.ToString(CultureInfo.InvariantCulture));
            return AllowedVersions.Any(e => e.Equals(dataVersion));
        }

        public TOutput Export(TInput data)
        {
            throw new NotImplementedException();
        }

        public TInput Import(TOutput data)
        {
            var inputMap = new RMapConfiguration(data.Type);
            return (TInput) inputMap;
        }
    }
}