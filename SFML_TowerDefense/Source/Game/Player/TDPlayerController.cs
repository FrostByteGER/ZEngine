using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;
using SFML_TowerDefense.Source.Game.Buildings;
using SFML_TowerDefense.Source.Game.Core;
using SFML_TowerDefense.Source.Game.TileMap;

namespace SFML_TowerDefense.Source.Game.Player
{
	public class TDPlayerController : PlayerController
	{
		public TDLevel LevelRef { get; set; }
		private TVector2f MouseCoords { get; set; } = new TVector2f();
		public TDTile CurrentlyHoveredTile { get; private set; }
		public TDTile CurrentlySelectedTile { get; private set; }
		public TVector2i CurrentlyHoveredTileCoords { get; private set; }
		public TVector2i CurrentlySelectedTileCoords { get; private set; }

		public List<TDNexus> PlayerNexus = new List<TDNexus>();
		public uint Health
		{
			get
			{
				uint health = 0;
				// TODO: Cache the result
				foreach (var nexus in PlayerNexus)
				{
					health += nexus.Health;
				}
				return health;
			}
		}

		private float DeltaTime { get; set; } = 0;
		public uint Gold { get; set; } = 0;
		public uint Score { get; set; } = 0;

		public float ZoomSpeed { get; set; } = 0.1f;
		public float ZoomLevelMin { get; set; } = 0.1f;
		public float ZoomLevel { get; set; } = 1f;
		public float ZoomLevelMax { get; set; } = 2.5f;

		public float CameraPanSpeed { get; set; } = 250.0f;
		public float CameraPanSpeedFastMultiplier = 2.0f;
		public float CameraPanSpeedPreciseMultiplier = .5f;

		public override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			if (Input.IsKeyPressed(Keyboard.Key.F))
			{

			}
		}

		public override void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
		{
			var scrollSpeed = CameraPanSpeed;
			if (Input.IsKeyDown(Keyboard.Key.LShift) && !Input.IsKeyDown(Keyboard.Key.LControl)) scrollSpeed *= CameraPanSpeedFastMultiplier;
			if (!Input.IsKeyDown(Keyboard.Key.LShift) && Input.IsKeyDown(Keyboard.Key.LControl)) scrollSpeed *= CameraPanSpeedPreciseMultiplier;
			if (Input.IsKeyDown(Keyboard.Key.W))
			{
				PlayerCamera.Move(new TVector2f(0, -scrollSpeed * DeltaTime));
			}
			if (Input.IsKeyDown(Keyboard.Key.S))
			{
				PlayerCamera.Move(new TVector2f(0, scrollSpeed * DeltaTime));
			}
			if (Input.IsKeyDown(Keyboard.Key.A))
			{
				PlayerCamera.Move(new TVector2f(-scrollSpeed * DeltaTime, 0));
			}
			if (Input.IsKeyDown(Keyboard.Key.D))
			{
				PlayerCamera.Move(new TVector2f(scrollSpeed * DeltaTime, 0));
			}

		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			LevelRef = LevelReference as TDLevel;
			PlayerNexus = LevelRef?.FindActorsInLevel<TDNexus>().ToList();
		}

		public override void OnMouseMoved(object sender, MouseMoveEventArgs mouseMoveEventArgs)
		{
			base.OnMouseMoved(sender, mouseMoveEventArgs);
			MouseCoords = LevelRef.EngineReference.EngineWindow.MapPixelToCoords(new Vector2i(mouseMoveEventArgs.X, mouseMoveEventArgs.Y), PlayerCamera);
		}

		public override void OnMouseScrolled(object sender, MouseWheelScrollEventArgs mouseWheelScrollEventArgs)
		{
			ZoomLevel += -mouseWheelScrollEventArgs.Delta * ZoomSpeed;
			ZoomLevel = ZoomLevel.Clamp(ZoomLevelMin, ZoomLevelMax);
			if (ZoomLevel <= ZoomLevelMin || ZoomLevel >= ZoomLevelMax) return;
			var zoomLevel = 1 + -mouseWheelScrollEventArgs.Delta * ZoomSpeed;
			PlayerCamera.Zoom(zoomLevel);

		}

		public override void OnMouseButtonPressed(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			base.OnMouseButtonPressed(sender, mouseButtonEventArgs);
			if (Hud.IsHovered) return;
			switch (mouseButtonEventArgs.Button)
			{
				case Mouse.Button.Left:
					if (CurrentlySelectedTile == null)
					{
						CurrentlySelectedTileCoords = LevelRef.WorldCoordsToTileCoords(MouseCoords);
						CurrentlySelectedTile = CurrentlyHoveredTile;
					}
					else
					{
						CurrentlySelectedTileCoords = LevelRef.WorldCoordsToTileCoords(MouseCoords);
						CurrentlySelectedTile.Sprite.Color = Color.White;
						CurrentlySelectedTile = CurrentlyHoveredTile;
					}
					break;
				case Mouse.Button.Right:
					if(CurrentlySelectedTile != null) CurrentlySelectedTile.Sprite.Color = Color.White;
					CurrentlySelectedTileCoords = null;
					CurrentlySelectedTile = null;
					break;
				default:
					break;
			}
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			DeltaTime = deltaTime;
			var coords = LevelRef.WorldCoordsToTileCoords(MouseCoords);
			if (ReferenceEquals(coords, null)) return;
			//Console.WriteLine("TO TILE: " + coords);
			//Console.WriteLine("TO WORLD: " + LevelRef.TileCoordsToWorldCoords(coords));

			var newTile = LevelRef.GetTileByTileCoords(coords);
			if (CurrentlyHoveredTile != null && newTile != CurrentlyHoveredTile)
			{
				if(CurrentlyHoveredTile != CurrentlySelectedTile) CurrentlyHoveredTile.Sprite.Color = Color.White;
				CurrentlyHoveredTileCoords = coords;
				CurrentlyHoveredTile = newTile;
				CurrentlyHoveredTile.Sprite.Color = Color.Green;
			}else if (CurrentlyHoveredTile == null)
			{
				CurrentlyHoveredTileCoords = coords;
				CurrentlyHoveredTile = newTile;
				CurrentlyHoveredTile.Sprite.Color = Color.Green;
			}
		}
	}
}