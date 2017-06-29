using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;

namespace SFML_TowerDefense.Source.Game.Player
{
	public class TDPlayerController : PlayerController
	{
		public TDLevel LevelRef { get; set; }

		private TVector2f MouseCoords { get; set; } = new TVector2f();
		public TDTile CurrentlySelectedTile { get; private set; }
		private float DeltaTime { get; set; } = 0;

		public override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyPressed(sender, keyEventArgs);
		}

		public override void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
		{
			if (Input.IsKeyDown(Keyboard.Key.W))
			{
				PlayerCamera.Move(new TVector2f(0, -100 * DeltaTime));
			}
			if (Input.IsKeyDown(Keyboard.Key.S))
			{
				PlayerCamera.Move(new TVector2f(0, 100 * DeltaTime));
			}
			if (Input.IsKeyDown(Keyboard.Key.A))
			{
				PlayerCamera.Move(new TVector2f(-100 * DeltaTime, 0));
			}
			if (Input.IsKeyDown(Keyboard.Key.D))
			{
				PlayerCamera.Move(new TVector2f(100 * DeltaTime, 0));
			}
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			LevelRef = LevelReference as TDLevel;
		}

		public override void OnMouseMoved(object sender, MouseMoveEventArgs mouseMoveEventArgs)
		{
			base.OnMouseMoved(sender, mouseMoveEventArgs);
			MouseCoords = LevelRef.EngineReference.EngineWindow.MapPixelToCoords(new Vector2i(mouseMoveEventArgs.X, mouseMoveEventArgs.Y));
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			DeltaTime = deltaTime;
			var coords = LevelRef.WorldCoordsToTileCoords(MouseCoords);
			if (ReferenceEquals(coords, null)) return;
			Console.WriteLine("TO TILE: " + coords);
			Console.WriteLine("TO WORLD: " + LevelRef.TileCoordsToWorldCoords(coords));

			var newTile = LevelRef.GetTileByTileCoords(coords);
			if (CurrentlySelectedTile != null && newTile != CurrentlySelectedTile)
			{
				CurrentlySelectedTile.Sprite.Color = Color.White;
				CurrentlySelectedTile = newTile;
				CurrentlySelectedTile.Sprite.Color = Color.Green;
			}else if (CurrentlySelectedTile == null)
			{
				CurrentlySelectedTile = newTile;
				CurrentlySelectedTile.Sprite.Color = Color.Green;
			}
		}
	}
}