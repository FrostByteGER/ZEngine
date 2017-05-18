using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Physics;

namespace SFML_Breakout
{
	public class BreakoutMenuPlayerController : PlayerController
	{
		public BreakoutMenuPlayerController()
		{
		}

		public BreakoutMenuPlayerController(SpriteActor playerPawn) : base(playerPawn)
		{
		}

		public BreakoutMenuLevel LevelRef { get; set; }
		public int SelectedIndex { get; set; } = 0;

		public override void RegisterInput()
		{
			Input = LevelReference.EngineReference.InputManager;

			Input.RegisterKeyInput(OnKeyPressed, OnKeyReleased);

			Input.RegisterJoystickInput(null, null, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved);
		}

		public override void UnregisterInput()
		{
			Input = LevelReference.EngineReference.InputManager;

			Input.UnregisterKeyInput(OnKeyPressed, OnKeyReleased);

			Input.UnregisterJoystickInput(null, null, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved);
		}

		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			if (Input.EnterPressed)
			{
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Play")
				{
					IsActive = false;
					if (((BreakoutPersistentGameMode) LevelReference.EngineReference.PersistentGameMode).CurrentLevel == 0)
						++((BreakoutPersistentGameMode) LevelReference.EngineReference.PersistentGameMode).CurrentLevel;
					LevelReference.EngineReference.RegisterEvent(new SwitchLevelEvent<SwitchLevelParams>(new SwitchLevelParams(this, LevelReference.EngineReference.Levels[(int) ((BreakoutPersistentGameMode) LevelReference.EngineReference.PersistentGameMode).CurrentLevel ])));
				}
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Mute Sounds")
				{
					Engine.Instance.GlobalVolume = 0;
					BreakoutPersistentGameMode.BGM_Main.Volume = 0;
					LevelRef.Menu[SelectedIndex].DisplayedString = "Play Sounds";
					LevelRef.Menu[SelectedIndex].Origin = new Vector2f(LevelRef.Menu[SelectedIndex].GetLocalBounds().Width / 2.0f, LevelRef.Menu[SelectedIndex].GetLocalBounds().Height / 2.0f);
					LevelRef.Menu[SelectedIndex].Position = new Vector2f(LevelReference.EngineReference.EngineWindowWidth / 2.0f, 320);
				}
				else if (LevelRef.Menu[SelectedIndex].DisplayedString == "Play Sounds")
				{
					Engine.Instance.GlobalVolume = 50;
					BreakoutPersistentGameMode.BGM_Main.Volume = 50;
					LevelRef.Menu[SelectedIndex].DisplayedString = "Mute Sounds";
					LevelRef.Menu[SelectedIndex].Origin = new Vector2f(LevelRef.Menu[SelectedIndex].GetLocalBounds().Width / 2.0f, LevelRef.Menu[SelectedIndex].GetLocalBounds().Height / 2.0f);
					LevelRef.Menu[SelectedIndex].Position = new Vector2f(LevelReference.EngineReference.EngineWindowWidth / 2.0f, 320);
				}
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Exit Game")
				{
					Engine.Instance.CloseEngineWindow();
				}
			}
			if (Input.WPressed || Input.UpPressed)
			{
				if (SelectedIndex <= 0)
				{
					SelectedIndex = LevelRef.Menu.Count - 1;
				}
				else
				{
					--SelectedIndex;
				}
				foreach (var item in LevelRef.Menu)
				{
					item.Color = BreakoutMenuLevel.ColorUnselected;
				}
				LevelRef.Menu[SelectedIndex].Color = BreakoutMenuLevel.ColorSelected;
			}
			if (Input.SPressed || Input.DownPressed)
			{
				if (SelectedIndex >= LevelRef.Menu.Count - 1)
				{
					SelectedIndex = 0;
				}
				else
				{
					++SelectedIndex;
				}
				foreach (var item in LevelRef.Menu)
				{
					item.Color = BreakoutMenuLevel.ColorUnselected;
				}
				LevelRef.Menu[SelectedIndex].Color = BreakoutMenuLevel.ColorSelected;
			}
		}

		protected override void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyReleased(sender, keyEventArgs);
		}

		protected override void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			base.OnJoystickButtonPressed(sender, joystickButtonEventArgs);
			if (joystickButtonEventArgs.Button == 0)
			{
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Play")
				{
					IsActive = false;
					LevelReference.EngineReference.RegisterEvent(new SwitchLevelEvent<SwitchLevelParams>(new SwitchLevelParams(this, LevelReference.EngineReference.Levels[1])));
				}
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Mute Sounds")
				{
					Engine.Instance.GlobalVolume = 0;
					LevelRef.Menu[SelectedIndex].DisplayedString = "Play Sounds";
				}
				else if (LevelRef.Menu[SelectedIndex].DisplayedString == "Play Sounds")
				{
					Engine.Instance.GlobalVolume = 10;
					LevelRef.Menu[SelectedIndex].DisplayedString = "Mute Sounds";
				}
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Exit Game")
				{
					Engine.Instance.CloseEngineWindow();
				}
			}
		}

		protected override void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			base.OnJoystickButtonReleased(sender, joystickButtonEventArgs);
		}

		protected override void OnJoystickMoved(object sender, JoystickMoveEventArgs joystickMoveEventArgs)
		{
			if (joystickMoveEventArgs.Axis == Joystick.Axis.PovY && Math.Abs(joystickMoveEventArgs.Position - -100.0f) < 0.0001f)
			{
				if (SelectedIndex <= 0)
				{
					SelectedIndex = LevelRef.Menu.Count - 1;
				}
				else
				{
					--SelectedIndex;
				}
				foreach (var item in LevelRef.Menu)
				{
					item.Color = BreakoutMenuLevel.ColorUnselected;
				}
				LevelRef.Menu[SelectedIndex].Color = BreakoutMenuLevel.ColorSelected;
			}
			if (joystickMoveEventArgs.Axis == Joystick.Axis.PovY && Math.Abs(joystickMoveEventArgs.Position - 100.0f) < 0.0001f)
			{
				if (SelectedIndex >= LevelRef.Menu.Count - 1)
				{
					SelectedIndex = 0;
				}
				else
				{
					++SelectedIndex;
				}
				foreach (var item in LevelRef.Menu)
				{
					item.Color = BreakoutMenuLevel.ColorUnselected;
				}
				LevelRef.Menu[SelectedIndex].Color = BreakoutMenuLevel.ColorSelected;
			}
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			LevelRef = PlayerPawn.LevelReference as BreakoutMenuLevel;
			SelectedIndex = 0;
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}