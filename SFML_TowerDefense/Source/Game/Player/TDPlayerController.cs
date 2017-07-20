using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;
using SFML_TowerDefense.Source.Game.AI;
using SFML_TowerDefense.Source.Game.Buildings;
using SFML_TowerDefense.Source.Game.Core;
using SFML_TowerDefense.Source.Game.TileMap;
using SFML_TowerDefense.Source.Game.Units;

namespace SFML_TowerDefense.Source.Game.Player
{
	public class TDPlayerController : PlayerController
	{
		public TDLevel LevelRef { get; set; }

		private TVector2f MouseCoords { get; set; } = new TVector2f();
		public TDTile CurrentlyHoveredTile { get; private set; }
		public TDTile CurrentlySelectedTile { get; private set; }

		public List<TDNexus> PlayerNexus = new List<TDNexus>();
		public uint Health
		{
			get
			{
				uint health = 0;
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

		public override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			if (Input.IsKeyPressed(Keyboard.Key.F))
			{
				var actor = LevelRef.SpawnActor<TDFieldActor>();
				var sprite = new SpriteComponent(new Sprite(LevelRef.EngineReference.AssetManager.LoadTexture("TowerBase")));
				var spriteGun = new SpriteComponent(new Sprite(LevelRef.EngineReference.AssetManager.LoadTexture("TowerGunT3")));
				actor.SetRootComponent(sprite);
				actor.AddComponent(spriteGun);
				actor.Position = CurrentlyHoveredTile.WorldPosition;
				CurrentlyHoveredTile.FieldActors.Add(actor);

				var testActor = LevelRef.SpawnActor<TDUnit>();
				testActor.Position = new TVector2f(100,100);
				testActor.CurrentWaypoint = new TDWaypoint(LevelRef);
				testActor.CurrentWaypoint.Position = CurrentlyHoveredTile.WorldPosition;
			}
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
			PlayerNexus = LevelRef?.FindActorsInLevel<TDNexus>().ToList();
		}

		public override void OnMouseMoved(object sender, MouseMoveEventArgs mouseMoveEventArgs)
		{
			base.OnMouseMoved(sender, mouseMoveEventArgs);
			MouseCoords = LevelRef.EngineReference.EngineWindow.MapPixelToCoords(new Vector2i(mouseMoveEventArgs.X, mouseMoveEventArgs.Y), PlayerCamera);
		}

		public override void OnMouseScrolled(object sender, MouseWheelScrollEventArgs mouseWheelScrollEventArgs)
		{
			var zoomLevel = 1 + -mouseWheelScrollEventArgs.Delta * ZoomSpeed;
			PlayerCamera.Zoom(zoomLevel);

		}

		public override void OnMouseButtonPressed(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			base.OnMouseButtonPressed(sender, mouseButtonEventArgs);
			switch (mouseButtonEventArgs.Button)
			{
				case Mouse.Button.Left:
					if (CurrentlySelectedTile == null)
					{
						CurrentlySelectedTile = CurrentlyHoveredTile;
					}
					else
					{
						CurrentlySelectedTile.Sprite.Color = Color.White;
						CurrentlySelectedTile = CurrentlyHoveredTile;
					}
					break;
				case Mouse.Button.Right:
					CurrentlySelectedTile.Sprite.Color = Color.White;
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
				CurrentlyHoveredTile = newTile;
				CurrentlyHoveredTile.Sprite.Color = Color.Green;
			}else if (CurrentlyHoveredTile == null)
			{
				CurrentlyHoveredTile = newTile;
				CurrentlyHoveredTile.Sprite.Color = Color.Green;
			}
		}
	}
}