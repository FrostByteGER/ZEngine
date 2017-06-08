using SFML.Window;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_SpaceSEM.Game.Actors;

namespace SFML_SpaceSEM.Game.Players
{
	public class SpaceGamePlayerController : PlayerController
	{
		public SpaceShipPlayer Player { get; set; }

		public SpaceGamePlayerController()
		{
		}

		public SpaceGamePlayerController(SpriteActor playerPawn) : base(playerPawn)
		{
			Player = playerPawn as SpaceShipPlayer;
		}

		public override void RegisterInput()
		{
			base.RegisterInput();
		}

		public override void UnregisterInput()
		{
			base.UnregisterInput();
		}

		public override void OnMouseButtonPressed(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			if (Input.MouseLeftPressed)
			{
				Player.FireWeapons();
			}
		}

		public override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			if (Input.IsKeyPressed(Keyboard.Key.Space))
			{
				Player.FireWeapons();
			}
			if (Input.IsKeyPressed(Keyboard.Key.Escape))
			{
				LevelReference.EngineReference.LoadPreviousLevel(true);
			}
		}

		public override void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyDown(sender, keyEventArgs);
		}

		public override void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyReleased(sender, keyEventArgs);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameResume()
		{
			base.OnGameResume();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}