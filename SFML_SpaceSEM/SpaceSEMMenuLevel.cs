using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using Text = SFML_Engine.Engine.SFML.Graphics.Text;
using SFML_Engine.Engine.JUI;
using System;

namespace SFML_SpaceSEM
{
	public class SpaceSEMMenuLevel : Level
	{

		public static Font MainGameFont { get; set; }
		/* old
		public Text MainLogo { get; set; } = new Text();
		public Text Play { get; set; } = new Text();
		public Text AllowSound { get; set; } = new Text();
		public Text ExitGame { get; set; } = new Text();
		public Text HighScoreText { get; set; } = new Text();

		public List<Text> Menu { get; set; }
		*/
		private SpaceGUI GUI;

		public JContainer rootContainer;
		public JLabel title;

		public JCheckbox playCheckBox;

		public JContainer playContainer;

		public JContainer mainLeftContainer;

		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
			GUI.Tick(deltaTime);
		}

		public static Color ColorSelected { get; } = new Color(255, 255, 255, 255);
		public static Color ColorUnselected { get; } = new Color(162, 160, 160, 255);
		public object Menu { get; internal set; }

		public void InitiateMenu()
		{

			// old vvv
			/*
			MainLogo.Font = MainGameFont;
			MainLogo.DisplayedString = "Space SEM";
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
			*/
			// old ^^^

			// Font TODO 
			GUI = new SpaceGUI(MainGameFont, EngineReference.EngineWindow, EngineReference.InputManager);

			// Root Container
			rootContainer = new JContainer(GUI);

			rootContainer.setPosition(new Vector2f(100, 100));
			rootContainer.setSize(new Vector2f(600, 600));

			JBorderLayout borderLayout = new JBorderLayout(rootContainer);

			borderLayout.TopSize = 0.2f;
			borderLayout.BottemSize = 0.2f;
			borderLayout.LeftSize = 0.2f;
			borderLayout.RightSize = 0.2f;

			rootContainer.Layout = borderLayout;

			// Title
			title = new JLabel(GUI);
			title.Text.DisplayedString = "Game Title";

			// Main Menue
			mainLeftContainer = new JContainer(GUI);
			mainLeftContainer.Layout = new JLayout(mainLeftContainer);

			playCheckBox = new JCheckbox(GUI);
			playCheckBox.Text.DisplayedString = "Play";
			playCheckBox.Something += this.ChangeCenterContainer;

			JCheckbox optionCheckBox = new JCheckbox(GUI);
			optionCheckBox.Text.DisplayedString = "Options";

			JCheckbox helpCheckBox = new JCheckbox(GUI);
			helpCheckBox.Text.DisplayedString = "Help";

			JCheckbox creditsCheckBox = new JCheckbox(GUI);
			creditsCheckBox.Text.DisplayedString = "Credits";

			JCheckbox editorsCheckBox = new JCheckbox(GUI);
			editorsCheckBox.Text.DisplayedString = "Editor";
			editorsCheckBox.IsEnabled = false;

			JCheckbox exitCheckBox = new JCheckbox(GUI);
			exitCheckBox.Text.DisplayedString = "Exit";

			JChackboxGroup mainCheckBoxGroup = new JChackboxGroup();
			mainCheckBoxGroup.AddBox(playCheckBox);
			mainCheckBoxGroup.AddBox(optionCheckBox);
			mainCheckBoxGroup.AddBox(helpCheckBox);
			mainCheckBoxGroup.AddBox(creditsCheckBox);
			mainCheckBoxGroup.AddBox(editorsCheckBox);
			mainCheckBoxGroup.AddBox(exitCheckBox);

			// Container vvv
			// Play Menue
			playContainer = new JContainer(GUI);
			playContainer.Layout = new JLayout(playContainer);
			JLabel playTemp = new JLabel(GUI);
			playTemp.Text.DisplayedString = "PlayTemp";
			playContainer.addElement(playTemp);


			// Option Menue

			JContainer optionContainer = new JContainer(GUI);
			optionContainer.Layout = new JLayout(optionContainer);
			JLabel optionTemp = new JLabel(GUI);
			optionTemp.Text.DisplayedString = "OptionTemp";
			optionContainer.addElement(optionTemp);

			// Help Menue

			JContainer helpContainer = new JContainer(GUI);
			helpContainer.Layout = new JLayout(helpContainer);
			JLabel helpTemp = new JLabel(GUI);
			helpTemp.Text.DisplayedString = "OptionTemp";
			helpContainer.addElement(helpTemp);

			// Credit Menue

			JContainer creditContainer = new JContainer(GUI);
			creditContainer.Layout = new JLayout(creditContainer);
			JLabel creditTemp = new JLabel(GUI);
			creditTemp.Text.DisplayedString = "OptionTemp";
			creditContainer.addElement(creditTemp);

			// Editor Menue

			JContainer editorContainer = new JContainer(GUI);
			editorContainer.Layout = new JLayout(editorContainer);
			JLabel editorTemp = new JLabel(GUI);
			editorTemp.Text.DisplayedString = "OptionTemp";
			editorContainer.addElement(editorTemp);

			// Exit Menue

			JContainer exitContainer = new JContainer(GUI);
			exitContainer.Layout = new JLayout(exitContainer);
			JLabel exitTemp = new JLabel(GUI);
			exitTemp.Text.DisplayedString = "OptionTemp";
			exitContainer.addElement(exitTemp);

			//Container ^^^

			rootContainer.addElement(title, JBorderLayout.TOP);
			rootContainer.addElement(mainLeftContainer, JBorderLayout.LEFT);

			mainLeftContainer.addElement(playCheckBox);
			mainLeftContainer.addElement(optionCheckBox);
			mainLeftContainer.addElement(helpCheckBox);
			mainLeftContainer.addElement(creditsCheckBox);
			mainLeftContainer.addElement(editorsCheckBox);
			mainLeftContainer.addElement(exitCheckBox);

			GUI.RootContainer = rootContainer;
		}

		public override void OnLevelLoad()
		{
			/* old
			var dummyPawn = new SpriteActor();
			var menuController = new SpaceSEMMenuPlayerController(dummyPawn);
			RegisterPlayer(menuController);
			RegisterActor(dummyPawn);
			RegisterActor(MainLogo);
			RegisterActor(Play);
			RegisterActor(AllowSound);
			RegisterActor(ExitGame);
			RegisterActor(HighScoreText);
			//HighScoreText.DisplayedString = "Highscore: " + ((BreakoutPersistentGameMode)EngineReference.PersistentGameMode).AlltimeHighScore;
			*/
			base.OnLevelLoad();
		}

		public void ChangeCenterContainer(JElement element)
		{
			Console.WriteLine(playCheckBox.IsSelected);
			if (playCheckBox.IsSelected)
			{
				GUI.RootContainer.addElement(playContainer, JBorderLayout.CENTER);
			}
			else
			{
				GUI.RootContainer.addElement(null, JBorderLayout.CENTER);
			}
			
		}

		protected override void LevelDraw(ref RenderWindow renderWindow)
		{
			base.LevelDraw(ref renderWindow);
			renderWindow.Draw(GUI);
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			//BreakoutPersistentGameMode.SwitchMusic();
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
			//BreakoutPersistentGameMode.BGM_Main.Stop();
		}

		public override void ShutdownLevel()
		{
			base.ShutdownLevel();
			UnregisterPlayers();
			UnregisterActors();
		}
	}
}