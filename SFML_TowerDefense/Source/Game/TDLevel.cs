using System;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using SFML_TowerDefense.Source.Game.Player;
using SFML_TowerDefense.Source.GUI;
using SFML.System;

namespace SFML_TowerDefense.Source.Game
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
			var pc = new TDPlayerController();
			RegisterPlayer(pc);

			// EngineReference.InputManager

			GameHud Hud = new GameHud(new Font("./Assets/Game/Fonts/Main.ttf"), EngineReference.EngineWindow, pc.Input);

			Hud.RootContainer.setPosition(new Vector2f(50, 50));
			Hud.RootContainer.setSize(new Vector2f(700, 700));

			pc.Hud = Hud;
		}

		// TODO: Verify working
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

		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
			//Hud.Tick(deltaTime);
		}

		protected override void LevelDraw(ref RenderWindow renderWindow)
		{
			base.LevelDraw(ref renderWindow);
			//renderWindow.Draw(Hud);
		}

		public TDTile GetTileByTileCoords(TVector2i tileCoords)
		{
			var index = (Map.SizeY / 2 + tileCoords.Y) * Map.SizeX + (Map.SizeY / 2 + tileCoords.X);
			return Map.Tiles[index];
		}
	}
}