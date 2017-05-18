using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using Text = SFML_Engine.Engine.SFML.Graphics.Text;

namespace SFML_Breakout
{
	public class BreakoutMenuLevel : Level
	{

		public static Font MainGameFont { get; set; }
		public Text MainLogo { get; set; } = new Text();
		public Text Play { get; set; } = new Text();
		public Text AllowSound { get; set; } = new Text();
		public Text ExitGame { get; set; } = new Text();
		public Text HighScoreText { get; set; } = new Text();

		public List<Text> Menu { get; set; }
		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
		}

		public static Color ColorSelected { get; } = new Color(255, 255, 255, 255);
		public static Color ColorUnselected { get; } = new Color(162, 160, 160, 255);

		public void InitiateMenu()
		{
			MainLogo.Font = MainGameFont;
			MainLogo.DisplayedString = "Pong";
			MainLogo.CharacterSize = 100;
			MainLogo.Color = ColorSelected;
			MainLogo.Style = Text.Styles.Regular;
			MainLogo.Origin = new Vector2f(MainLogo.GetLocalBounds().Width / 2.0f, MainLogo.GetLocalBounds().Height / 2.0f);
			MainLogo.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 100);

			Play.Font = MainGameFont;
			Play.DisplayedString = "Play";
			Play.CharacterSize = 50;
			Play.Color = ColorSelected;
			Play.Style = Text.Styles.Regular;
			Play.Origin = new Vector2f(Play.GetLocalBounds().Width / 2.0f, Play.GetLocalBounds().Height / 2.0f);
			Play.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 250);

			AllowSound.Font = MainGameFont;
			AllowSound.DisplayedString = "Mute Sounds";
			AllowSound.CharacterSize = 50;
			AllowSound.Color = ColorUnselected;
			AllowSound.Style = Text.Styles.Regular;
			AllowSound.Origin = new Vector2f(AllowSound.GetLocalBounds().Width / 2.0f, AllowSound.GetLocalBounds().Height / 2.0f);
			AllowSound.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 320);

			ExitGame.Font = MainGameFont;
			ExitGame.DisplayedString = "Exit Game";
			ExitGame.CharacterSize = 50;
			ExitGame.Color = ColorUnselected;
			ExitGame.Style = Text.Styles.Regular;
			ExitGame.Origin = new Vector2f(ExitGame.GetLocalBounds().Width / 2.0f, ExitGame.GetLocalBounds().Height / 2.0f);
			ExitGame.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 390);

			HighScoreText.Font = MainGameFont;
			HighScoreText.DisplayedString = "Highscore: 0";
			HighScoreText.CharacterSize = 50;
			HighScoreText.Color = ColorSelected;
			HighScoreText.Style = Text.Styles.Bold;
			HighScoreText.Origin = new Vector2f(HighScoreText.GetLocalBounds().Width / 2.0f, HighScoreText.GetLocalBounds().Height / 2.0f);
			HighScoreText.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 500);

			Menu = new List<Text> { Play, AllowSound, ExitGame };
		}

		public override void OnLevelLoad()
		{

			RegisterActor(MainLogo);
			RegisterActor(Play);
			RegisterActor(AllowSound);
			RegisterActor(ExitGame);
			RegisterActor(HighScoreText);
			HighScoreText.DisplayedString = "Highscore: " + ((BreakoutPersistentGameMode) EngineReference.PersistentGameMode).AlltimeHighScore;

			base.OnLevelLoad();
		}

		public override void OnGameStart()
		{

			base.OnGameStart();
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