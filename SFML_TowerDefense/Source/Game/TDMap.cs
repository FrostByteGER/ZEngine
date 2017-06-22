using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;

namespace SFML_TowerDefense.Source.Game
{
	public class TDMap : TDActor
	{
		public List<TDTile> Tiles { get; set; }
		public List<Sprite> TileSprites { get; set; }
		public int SizeX { get; set; } = 0;
		public int SizeY { get; set; } = 0;


		public TDMap(Level level) : base(level)
		{

			//TODO: Needs a complete overhaul!
			Tiles = new List<TDTile>();
			TileSprites = new List<Sprite>();
			dynamic mapData = JSONManager.LoadObject<dynamic>("Assets/Game/Levels/test.json");
			var tilesheetData = mapData.tilesets;
			int startIndex = tilesheetData[0].firstgid.ToObject<int>();
			string sheetSource = tilesheetData[0].source.ToObject<string>();
			dynamic sheetData = JSONManager.LoadObject<dynamic>("Assets/Game/Levels/" + sheetSource);
			int sheetColumns = sheetData.columns.ToObject<int>();
			string sheetImage = sheetData.image.ToObject<string>();
			var texture = level.EngineReference.AssetManager.LoadTexture("Level01Sheet");
			int tileCount = sheetData.tilecount.ToObject<int>();
			int tileheight = sheetData.tileheight.ToObject<int>();
			int tilewidth = sheetData.tilewidth.ToObject<int>();


			dynamic layers = mapData.layers;
			var layer0 = layers[0];
			int[] layerData = layer0.data.ToObject<int[]>();
			var layer1 = layers[1];
			var object0 = layer1.objects[0];
			var tileHeight = mapData.tileheight.ToObject<int>();
			var tileWidth = mapData.tilewidth.ToObject<int>();
			var mapHeight = mapData.height.ToObject<int>();
			var mapWidth = mapData.width.ToObject<int>();
			int j = 0;
			int k = 0;
			foreach (var l in layerData)
			{
				var tile = new TDTile(new Sprite(texture, new IntRect((l - 1) * tilewidth, 0, tilewidth, tileheight)));
				tile.LocalPosition = new TVector2f(k * tilewidth, (j / mapWidth) * tileheight);
				AddComponent(tile);
				Tiles.Add(tile);
				k = k >= mapWidth - 1 ? 0 : k + 1;
				++j;
			}
			SetRootComponent(Tiles[0]);

		}

		public override void Tick(float deltaTime)
		{
			foreach (var comp in Components)
			{
				Console.WriteLine(comp.WorldPosition);
			}
			base.Tick(deltaTime);
		}
	}
}
