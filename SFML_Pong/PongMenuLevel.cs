using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using Text = SFML_Engine.Engine.SFML.Graphics.Text;

namespace SFML_Pong
{
	public class PongMenuLevel : Level
	{

		public Font MainGameFont { get; set; }
		public Text MainLogo { get; set; } = new Text();
		public Text PlayCoop { get; set; } = new Text();
		public Text PlayVSBot { get; set; } = new Text();
		public Text AllowSound { get; set; } = new Text();
		public Text LoadResourcePack { get; set; } = new Text();
		public Text ExitGame { get; set; } = new Text();

		public List<Text> Menu { get; set; }
		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
		}

		public static Color ColorSelected { get; } = new Color(255, 255, 255, 255);
		public static Color ColorUnselected { get; } = new Color(162, 160, 160, 255);

		public override void OnLevelLoad()
		{
			MainGameFont = new Font("Assets/SFML_Pong/arial.ttf");
			MainLogo.Font = MainGameFont;
			MainLogo.DisplayedString = "Pong";
			MainLogo.CharacterSize = 100;
			MainLogo.Color = ColorSelected;
			MainLogo.Style = Text.Styles.Regular;
			MainLogo.Origin = new Vector2f(MainLogo.GetLocalBounds().Width / 2.0f, MainLogo.GetLocalBounds().Height / 2.0f);
			MainLogo.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 100);

			PlayCoop.Font = MainGameFont;
			PlayCoop.DisplayedString = "Player vs. Player";
			PlayCoop.CharacterSize = 50;
			PlayCoop.Color = ColorSelected;
			PlayCoop.Style = Text.Styles.Regular;
			PlayCoop.Origin = new Vector2f(PlayCoop.GetLocalBounds().Width / 2.0f, PlayCoop.GetLocalBounds().Height / 2.0f);
			PlayCoop.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 250);


			PlayVSBot.Font = MainGameFont;
			PlayVSBot.DisplayedString = "Play vs. Bot";
			PlayVSBot.CharacterSize = 50;
			PlayVSBot.Color = ColorUnselected;
			PlayVSBot.Style = Text.Styles.Regular;
			PlayVSBot.Origin = new Vector2f(PlayVSBot.GetLocalBounds().Width / 2.0f, PlayVSBot.GetLocalBounds().Height / 2.0f);
			PlayVSBot.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 320);

			AllowSound.Font = MainGameFont;
			AllowSound.DisplayedString = "Play Sounds";
			AllowSound.CharacterSize = 50;
			AllowSound.Color = ColorUnselected;
			AllowSound.Style = Text.Styles.Regular;
			AllowSound.Origin = new Vector2f(AllowSound.GetLocalBounds().Width / 2.0f, AllowSound.GetLocalBounds().Height / 2.0f);
			AllowSound.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 390);

			LoadResourcePack.Font = MainGameFont;
			LoadResourcePack.DisplayedString = "Standard Resource Pack";
			LoadResourcePack.CharacterSize = 50;
			LoadResourcePack.Color = ColorUnselected;
			LoadResourcePack.Style = Text.Styles.Regular;
			LoadResourcePack.Origin = new Vector2f(LoadResourcePack.GetLocalBounds().Width / 2.0f, LoadResourcePack.GetLocalBounds().Height / 2.0f);
			LoadResourcePack.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 460);

			ExitGame.Font = MainGameFont;
			ExitGame.DisplayedString = "Exit Game";
			ExitGame.CharacterSize = 50;
			ExitGame.Color = ColorUnselected;
			ExitGame.Style = Text.Styles.Regular;
			ExitGame.Origin = new Vector2f(ExitGame.GetLocalBounds().Width / 2.0f, ExitGame.GetLocalBounds().Height / 2.0f);
			ExitGame.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 530);

			Menu = new List<Text> { PlayCoop, PlayVSBot , AllowSound, LoadResourcePack, ExitGame };
			RegisterActor(MainLogo);
			RegisterActor(PlayCoop);
			RegisterActor(PlayVSBot);
			RegisterActor(AllowSound);
			RegisterActor(LoadResourcePack);
			RegisterActor(ExitGame);
			Console.WriteLine("Pong Menu Level #" + LevelID + " Loaded");
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