using System;
using System.Collections.Generic;
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
		public int SizeX { get; private set; } = 0;
		public int SizeY { get; private set; } = 0;
		public int TileSizeX { get; private set; } = 0;
		public int TileSizeY { get; private set; } = 0;
		public int GameSizeX => SizeX * TileSizeX;
		public int GameSizeY => SizeY * TileSizeY;

		public override TVector2f ActorBounds { get; set; }

		public override TVector2f Origin { get; set; }


		public TDMap(string levelName ,Level level) : base(level)
		{

			Tiles = new List<TDTile>();
			TileSprites = new List<Sprite>();

			// Map Data
			dynamic mapData = JSONManager.LoadObject<dynamic>("Assets/Game/Levels/" + levelName + ".json");

			// Tilesheet Array
			var tilesheets = mapData.tilesets;

			// First Index of the first Tilesheet
			int startIndex = tilesheets[0].firstgid.ToObject<int>();

			// Level Sheet Data Filename
			string sheetSource = tilesheets[0].source.ToObject<string>();

			// Level Sheet Data
			dynamic sheetData = JSONManager.LoadObject<dynamic>("Assets/Game/Levels/" + sheetSource);

			// Count of Columns in the Sheet Texture
			int sheetColumns = sheetData.columns.ToObject<int>();

			// Sheet Texture Name
			string sheetImage = sheetData.image.ToObject<string>();
			// Sheet Texture
			Texture texture = level.EngineReference.AssetManager.LoadTexture("Level01Sheet");
			// Count of all Tiles in the Sheet Texture
			int tileCount = sheetData.tilecount.ToObject<int>();
			// Width of a Tile in the Texture
			int tilewidth = sheetData.tilewidth.ToObject<int>();
			// Height of a Tile in the Texture
			int tileheight = sheetData.tileheight.ToObject<int>();
			TileSizeX = tilewidth;
			TileSizeY = tileheight;

			// Map Layer Array
			dynamic layers = mapData.layers;
			// First Layer
			var layer0 = layers[0];
			// Layer Tile Reference IDs. One-Based indices, NOT ZERO-BASED!
			int[] layerData = layer0.data.ToObject<int[]>();
			// Object Layer
			var layer1 = layers[1];
			//IEnumerable objects = layer1.objects.ToObject<IEnumerable>();
			//foreach (var mapObject in objects)
			//{
				//TODO: Spawn Objects here
			//}

			// Map Tile Width. Currently not needed, defined inside the Sheet Data
			int tileWidth = mapData.tilewidth.ToObject<int>();
			// Map Tile Height. Currently not needed, defined inside the Sheet Data
			int tileHeight = mapData.tileheight.ToObject<int>();
			// Map Width in Tiles
			int mapWidth = mapData.width.ToObject<int>();
			// Map Height in Tiles
			int mapHeight = mapData.height.ToObject<int>();

			SizeX = mapWidth;
			SizeY = mapHeight;

			ActorBounds = new TVector2f(GameSizeX / 2.0f, GameSizeY / 2.0f);
			Origin = ActorBounds;

			SetRootComponent(new ActorComponent("SceneComponent"));
			int j = 0;
			int k = 0;
			foreach (var l in layerData)
			{
				var tile = new TDTile(new Sprite(texture, new IntRect((l - 1) * tilewidth, 0, tilewidth, tileheight)));
				tile.LocalPosition = new TVector2f(k * tilewidth - ActorBounds.X + tilewidth / 2.0f, (j / mapWidth) * tileheight - ActorBounds.Y + tileheight / 2.0f);
				AddComponent(tile);
				Tiles.Add(tile);
				k = k >= mapWidth - 1 ? 0 : k + 1;
				++j;
			}

		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}


	}
}
