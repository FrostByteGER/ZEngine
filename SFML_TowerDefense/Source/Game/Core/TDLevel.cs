using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;
using SFML_TowerDefense.Source.Game.AI;
using SFML_TowerDefense.Source.Game.Buildings;
using SFML_TowerDefense.Source.Game.Player;
using SFML_TowerDefense.Source.Game.TileMap;
using SFML_TowerDefense.Source.Game.Units;
using SFML_TowerDefense.Source.GUI;

namespace SFML_TowerDefense.Source.Game.Core
{
	public class TDLevel : Level
	{
		public TDMap Map { get; protected set; } = null;

		public TDLevel()
		{
		}

		protected override void InitLevel()
		{
			base.InitLevel();
			GameMode = new TDGameMode();
			var pc = new TDPlayerController();
			RegisterPlayer(pc);

			Map = new TDMap("",this);

			/////////////////////////
			Map.Tiles = new List<TDTile>();
			Map.TileSprites = new List<Sprite>();

			// Map Data
			dynamic mapData = JSONManager.LoadObject<dynamic>("Assets/Game/Levels/" + "test" + ".json");

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
			Texture texture = EngineReference.AssetManager.LoadTexture("Level01Sheet");
			// Count of all Tiles in the Sheet Texture
			int tileCount = sheetData.tilecount.ToObject<int>();
			// Width of a Tile in the Texture
			int tilewidth = sheetData.tilewidth.ToObject<int>();
			// Height of a Tile in the Texture
			int tileheight = sheetData.tileheight.ToObject<int>();
			Map.TileSizeX = tilewidth;
			Map.TileSizeY = tileheight;

			// Map Layer Array
			dynamic layers = mapData.layers;
			// First Layer
			var layer0 = layers[0];
			// Layer Tile Reference IDs. One-Based indices, NOT ZERO-BASED!
			int[] layerData = layer0.data.ToObject<int[]>();

			// Map Tile Width. Currently not needed, defined inside the Sheet Data
			int tileWidth = mapData.tilewidth.ToObject<int>();
			// Map Tile Height. Currently not needed, defined inside the Sheet Data
			int tileHeight = mapData.tileheight.ToObject<int>();
			// Map Width in Tiles
			int mapWidth = mapData.width.ToObject<int>();
			// Map Height in Tiles
			int mapHeight = mapData.height.ToObject<int>();

			Map.SizeX = mapWidth;
			Map.SizeY = mapHeight;

			Map.ActorBounds = new TVector2f(Map.GameSizeX / 2.0f, Map.GameSizeY / 2.0f);
			Map.Origin = Map.ActorBounds;

			Map.SetRootComponent(new ActorComponent("SceneComponent"));
			int j = 0;
			int k = 0;
			foreach (var l in layerData)
			{
				var tile = new TDTile(new Sprite(texture, new IntRect((l - 1) * tilewidth, 0, tilewidth, tileheight)));
				tile.LocalPosition = new TVector2f(k * tilewidth - Map.ActorBounds.X + tilewidth / 2.0f, (j / mapWidth) * tileheight - Map.ActorBounds.Y + tileheight / 2.0f);
				Map.AddComponent(tile);
				Map.Tiles.Add(tile);
				k = k >= mapWidth - 1 ? 0 : k + 1;
				++j;
			}

			RegisterActor(Map);


			// Object Layer
			var layer1 = layers[1];
			var objects = layer1.objects.ToObject<IEnumerable<dynamic>>();
			foreach (var mapObject in objects)
			{
				var xOrigin = mapObject.x.ToObject<int>() - Map.ActorBounds.X + tilewidth / 2.0f;
				var yOrigin = mapObject.y.ToObject<int>() - Map.ActorBounds.Y + tileheight / 2.0f;
				var tileCoords = WorldCoordsToTileCoords(xOrigin, yOrigin);
				var objectType = mapObject.type.ToObject<string>();
				if (objectType == "TDSpawner")
				{
					var spawner = new TDSpawner(this);
					spawner.TilePosition = tileCoords;
					var rawWaves = mapObject.properties.waves.ToObject<IEnumerable<dynamic>>();
					foreach (var rawWave in rawWaves)
					{
						var wave = new TDWave(this);
						wave.SpawnSpeed = rawWave.spawnspeed.ToObject<float>();
						wave.Amount = rawWave.spawnamount.ToObject<uint>();
						var rawTypes = rawWave.unittypes.ToObject<IEnumerable<dynamic>>();
						var typeList = new List<Type>();
						foreach (var rawType in rawTypes)
						{
							var typeName = rawType.type.ToObject<string>();
							var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(myType => myType.Name == typeName && myType.IsSubclassOf(typeof(TDUnit)));
							if (type != null) typeList.Add(type);
						}
						wave.UnitTypes = typeList;
						spawner.Waves.Add(wave);
					}
					RegisterActor(spawner);
				}
				else if (objectType == "TDNexus")
				{
					var nexus = new TDNexus(this);
					nexus.TilePosition = tileCoords;
					nexus.Health = mapObject.properties.Health.ToObject<uint>();
					nexus.NexusID = mapObject.properties.NexusID.ToObject<uint>();
					RegisterActor(nexus);
					GetTileByTileCoords(tileCoords).FieldActors.Add(nexus);
				}
				else if (objectType == "TDOrefield")
				{
					var resourceField = new TDResource(this)
					{
						TilePosition = tileCoords,
						ResourceAmount = mapObject.properties.Value.ToObject<uint>()
					};
					RegisterActor(resourceField);
					GetTileByTileCoords(tileCoords).FieldActors.Add(resourceField);
				}
				else if (objectType == "TDMine")
				{
					var mine = new TDMine(this) {TilePosition = tileCoords};
					RegisterActor(mine);
					GetTileByTileCoords(tileCoords).FieldActors.Add(mine);
				}
				else if (objectType == "TDPath")
				{

					var waypoints = mapObject.polyline.ToObject<IEnumerable<dynamic>>();
					var waypointObjects = new List<TDWaypoint>();
					TDWaypoint previousWaypoint = null;
					foreach (var waypoint in waypoints)
					{
						xOrigin = mapObject.x.ToObject<int>() - Map.ActorBounds.X;
						yOrigin = mapObject.y.ToObject<int>() - Map.ActorBounds.Y;
						var xLocal = waypoint.x.ToObject<int>();
						var yLocal = waypoint.y.ToObject<int>();
						var wp = new TDWaypoint(this) {Position = new TVector2f(xOrigin + xLocal, yOrigin + yLocal)};
						if (previousWaypoint != null) previousWaypoint.NextWaypoint = wp;
						waypointObjects.Add(wp);
						RegisterActor(wp);
						GetTileByTileCoords(tileCoords).FieldActors.Add(wp);
						previousWaypoint = wp;
					}
					waypointObjects[waypointObjects.Count - 1].TargetNexus = mapObject.properties.TargetNexus.ToObject<uint>();
				}
			}


			/////////////////////////


			GameHud Hud = new GameHud(new Font("./Assets/Game/Fonts/Main.ttf"), EngineReference.EngineWindow, pc.Input);

			Hud.RootContainer.setPosition(new Vector2f(50, 50));
			Hud.RootContainer.setSize(new Vector2f(700, 700));

			pc.Hud = Hud;
		}

		public TVector2i WorldCoordsToTileCoords(float worldX, float worldY)
		{
			return WorldCoordsToTileCoords(new TVector2f(worldX, worldY));
		}

		// TODO: Verify if theres a better way instead of all these If-Checks.
		public TVector2i WorldCoordsToTileCoords(TVector2f worldCoords)
		{
			var tileSizeX = Map.TileSizeX;
			var tileSizeY = Map.TileSizeY;
			var actorBounds = Map.ActorBounds;
			var mapPosition = Map.Position;

			// If the World Position lies outside the map boundaries, it cannot be converted to TileCoords.
			if (worldCoords.X > mapPosition.X + actorBounds.X || worldCoords.X < mapPosition.X - actorBounds.X ||
			    worldCoords.Y > mapPosition.Y + actorBounds.Y || worldCoords.Y < mapPosition.Y - actorBounds.Y) return null;

			var tileCoords =  new TVector2i();

			if (worldCoords.X < mapPosition.X + tileSizeX / 2.0f)
			{
				tileCoords.X = (int)((worldCoords.X - tileSizeX / 2.0f) / tileSizeX);
			}
			else if (worldCoords.X > mapPosition.X - tileSizeX / 2.0f)
			{
				tileCoords.X = (int)((worldCoords.X + tileSizeX / 2.0f) / tileSizeX);
			}

			if (worldCoords.Y < mapPosition.Y + tileSizeY / 2.0f)
			{
				tileCoords.Y = (int) ((worldCoords.Y - tileSizeY / 2.0f) / tileSizeY);
			}
			else if (worldCoords.Y > mapPosition.Y - tileSizeY / 2.0f)
			{
				tileCoords.Y = (int)((worldCoords.Y + tileSizeY / 2.0f) / tileSizeY);
			}

			return tileCoords;
		}

		public TVector2f TileCoordsToWorldCoords(int tileX, int tileY)
		{
			return TileCoordsToWorldCoords(new TVector2i(tileX, tileY));
		}

		public TVector2f TileCoordsToWorldCoords(TVector2i tileCoords)
		{
			var worldCoords = new TVector2f();
			var mapWorldPosition = Map.Position;

			worldCoords.X = mapWorldPosition.X + (tileCoords.X * Map.TileSizeX);
			worldCoords.Y = mapWorldPosition.Y + (tileCoords.Y * Map.TileSizeY);
			return worldCoords;
		}

		public TDTile GetTileByTileCoords(TVector2i tileCoords)
		{
			var index = (Map.SizeY / 2 + tileCoords.Y) * Map.SizeX + (Map.SizeY / 2 + tileCoords.X);
			return Map.Tiles[index];
		}
	}
}