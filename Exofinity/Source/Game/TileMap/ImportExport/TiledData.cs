using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Exofinity.Source.Game.TileMap.ImportExport
{

    public class BaseMap
    {
        [JsonProperty("version")]
        public float Version { get; set; }
    }
    public class Map : BaseMap
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("tilewidth")]
        public int TileWidth { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("tileheight")]
        public int TileHeight { get; set; }
        [JsonProperty("tilesets")]
        public TileSet[] TileSets { get; set; }
        [JsonProperty("tiledversion")]
        public string TiledVersion { get; set; }
        [JsonProperty("staggerindex")]
        public string StaggerIndex { get; set; }
        [JsonProperty("staggeraxis")]
        public string StaggerAxis { get; set; }
        [JsonProperty("renderorder")]
        public string RenderOrder { get; set; }
        [JsonProperty("properties")]
        public Property[] Properties { get; set; }
        [JsonProperty("orientation")]
        public string Orientation { get; set; }
        [JsonProperty("nextobjectid")]
        public int NextObjectId { get; set; }
        [JsonProperty("nextlayerid")]
        public int NextLayerId { get; set; }
        [JsonProperty("layers")]
        public Layer[] Layers { get; set; }
        [JsonProperty("infinite")]
        public bool Infinite { get; set; }
        [JsonProperty("hexsidelength")]
        public int HexSideLength { get; set; }
        [JsonProperty("backgroundcolor")]
        public string BackgroundColor { get; set; }

    }

    public class Layer
    {
        [JsonProperty("chunks")]
        public Chunk[] Chunks { get; set; }
        [JsonProperty("compression")]
        public string Compression { get; set; }
        [JsonProperty("data")]
        public uint[] Data { get; set; }
        [JsonProperty("draworder")]
        public string DrawOrder { get; set; }
        [JsonProperty("encoding")]
        public string Encoding { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("layers")]
        public Layer[] Layers { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("objects")]
        public Object[] Objects { get; set; }
        [JsonProperty("offsetx")]
        public double OffsetX { get; set; }
        [JsonProperty("offsety")]
        public double OffsetY { get; set; }
        [JsonProperty("opacity")]
        public double Opacity { get; set; }
        [JsonProperty("properties")]
        public Property[] Properties { get; set; }
        [JsonProperty("transparentcolor")]
        public string TransparentColor { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("visible")]
        public bool Visible { get; set; }
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }

    }

    public class Chunk
    {
        [JsonProperty("data")]
        public uint[] Data { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
    }

    public class TileSet
    {
        [JsonProperty("columns")]
        public int Columns { get; set; }
        [JsonProperty("firstgid")]
        public int FirstGId { get; set; }
        [JsonProperty("grid")]
        public Grid Grid { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("imagewidth")]
        public int ImageWidth { get; set; }
        [JsonProperty("imageheight")]
        public int ImageHeight { get; set; }
        [JsonProperty("margin")]
        public int Margin { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("properties")]
        public Property[] Properties { get; set; }
        [JsonProperty("spacing")]
        public int Spacing { get; set; }
        [JsonProperty("terrains")]
        public Terrain[] Terrains { get; set; }
        [JsonProperty("tilecount")]
        public int TileCount { get; set; }
        [JsonProperty("tilewidth")]
        public int TileWidth { get; set; }
        [JsonProperty("tileheight")]
        public int TileHeight { get; set; }
        [JsonProperty("tileoffset")]
        public TileOffset TileOffset { get; set; }
        [JsonProperty("tiles")]
        public Tile[] Tiles { get; set; }
        [JsonProperty("transparentcolor")]
        public string TransparentColor { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("wangsets")]
        public WangSet[] WangSets { get; set; }
    }

    public class TileOffset
    {
        [JsonProperty("x")]
        public double X { get; set; }
        [JsonProperty("y")]
        public double Y { get; set; }
    }

    public class Terrain
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("tile")]
        public int Tile { get; set; }
    }

    public class WangSet
    {
        [JsonProperty("cornercolors")]
        public WangColor[] CornerColors { get; set; }
        [JsonProperty("edgecolors")]
        public WangColor[] EdgeColors { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("tile")]
        public int Tile { get; set; }
        [JsonProperty("wangtiles")]
        public WangTile[] WangTiles { get; set; }
    }

    public class WangColor
    {
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("probability")]
        public double Probability { get; set; }
        [JsonProperty("tile")]
        public int Tile { get; set; }
    }

    public class WangTile
    {
        [JsonProperty("dflip")]
        public bool DFlip { get; set; }
        [JsonProperty("hflip")]
        public bool HFlip { get; set; }
        [JsonProperty("tileid")]
        public int TileId { get; set; }
        [JsonProperty("vflip")]
        public bool VFlip { get; set; }
        [JsonProperty("wangid")]
        public byte[] WangId { get; set; }
    }

    public class Grid
    {
        [JsonProperty("orientation")]
        public string Orientation { get; set; }
        [JsonProperty("width")]
        public double Width { get; set; }
        [JsonProperty("height")]
        public double Height { get; set; }
    }

    public class Property
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("value")]
        public object Value { get; set; }

        public T GetValue<T>()
        {
            return (T) Convert.ChangeType(Value, System.Type.GetType(Type));
        }
    }

    public class Tile
    {
        [JsonProperty("animation")]
        public Frame[] Animation { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("imagewidth")]
        public int ImageWidth { get; set; }
        [JsonProperty("imageheight")]
        public int ImageHeight { get; set; }
        [JsonProperty("objectgroup")]
        public Layer ObjectGroup { get; set; }
        [JsonProperty("properties")]
        public Property[] Properties { get; set; }
        [JsonProperty("terrain")]
        public int[] Terrain { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Frame
    {
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("tileid")]
        public int TileId { get; set; }
    }

    public class Object
    {
        [JsonProperty("ellipse")]
        public bool Ellipse { get; set; }
        [JsonProperty("gid")]
        public int GId { get; set; }
        [JsonProperty("width")]
        public double Width { get; set; }
        [JsonProperty("height")]
        public double Height { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("point")]
        public bool Point { get; set; }
        [JsonProperty("polygon")]
        public Point[] Polygon { get; set; }
        [JsonProperty("polyline")]
        public Point[] Polyline { get; set; }
        [JsonProperty("properties")]
        public Property[] Properties { get; set; }
        [JsonProperty("rotation")]
        public double Rotation { get; set; }
        [JsonProperty("template")]
        public string Template { get; set; }
        [JsonProperty("text")]
        public KeyValuePair<string, object> Text { get; set; } //TODO: Change?
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("visible")]
        public bool Visible { get; set; }
        [JsonProperty("x")]
        public double X { get; set; }
        [JsonProperty("y")]
        public double Y { get; set; }
    }

    public class Point
    {
        [JsonProperty("x")]
        public double X { get; set; }
        [JsonProperty("y")]
        public double Y { get; set; }
    }
}