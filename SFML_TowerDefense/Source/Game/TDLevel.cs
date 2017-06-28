using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;

namespace SFML_TowerDefense.Source.Game
{
	public class TDLevel : Level
	{
		public TDMap Map { get; private set; } = null;

		public TDLevel()
		{
		}

		// TODO: Verify working
		public TVector2i WorldCoordsToTileCoords(float worldX, float worldY)
		{
			return WorldCoordsToTileCoords(new TVector2f(worldX, worldY));
		}

		// TODO: Verify working
		public TVector2i WorldCoordsToTileCoords(TVector2f worldCoords)
		{
			// If the World Position lies outside the map boundaries, it cannot be converted to TileCoords.
			if (worldCoords.X > Map.Position.X + Map.ActorBounds.X || worldCoords.X < Map.Position.X - Map.ActorBounds.X ||
			    worldCoords.Y > Map.Position.Y + Map.ActorBounds.Y || worldCoords.Y < Map.Position.Y - Map.ActorBounds.Y) return null;
			var tileCoords = new TVector2i
			{
				X = (int) (worldCoords.X / Map.TileSizeX),
				Y = (int) (worldCoords.Y / Map.TileSizeY)
			};
			return tileCoords;
		}

		// TODO: Verify working
		public TVector2f TileCoordsToWorldCoords(int tileX, int tileY)
		{
			return TileCoordsToWorldCoords(new TVector2i(tileX, tileY));
		}

		// TODO: Verify working
		public TVector2f TileCoordsToWorldCoords(TVector2i tileCoords)
		{
			var worldCoords = new TVector2f();
			var mapWorldPosition = Map.Position;
			worldCoords.X = mapWorldPosition.X + (tileCoords.X * Map.TileSizeX) / 2.0f;
			worldCoords.Y = mapWorldPosition.Y + (tileCoords.Y * Map.TileSizeY) / 2.0f;
			return worldCoords;
		}

		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
		}
	}
}