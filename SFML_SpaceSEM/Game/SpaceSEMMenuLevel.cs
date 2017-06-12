using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.JUI;
using SFML_SpaceSEM.UI;
using System;
using SFML.Audio;
using SFML_Engine.Engine.IO;

namespace SFML_SpaceSEM.Game
{
	public class SpaceSEMMenuLevel : SpaceLevel
	{

		//public Font MainGameFont { get; set; }
		public JGUI GUI { get; private set; }

		// Root Container
		public JContainer rootContainer;
		public JLabel title;

		// Main Left Container
		public JContainer mainLeftContainer;
		public JCheckbox playCheckBox;
		public JCheckbox optionCheckBox;
		public JCheckbox helpCheckBox;
		public JCheckbox creditsCheckBox;
		public JCheckbox editorsCheckBox;
		public JCheckbox exitCheckBox;

		// Play Container
		public JContainer playContainer;

		// Option Container
		public JContainer optionContainer;

		// Help Container
		public JContainer helpContainer;

		// Credit Container
		public JContainer creditContainer;

		// Editor Container
		public JContainer editorContainer;

		// Exit Container
		public JContainer exitContainer;

		private JCheckbox playCampaingnLevel1;
		private JCheckbox playCampaingnLevel2;
		private JCheckbox playCampaingnLevel3;
		private JCheckbox playCampaingnLevel4;

		private JSlider soundSlider { get; set; }
		private JSlider musicSlider { get; set; }

		public Music MenuMusic { get; set; }

		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
			GUI.Tick(deltaTime);
		}


		protected override void InitLevel()
		{
			base.InitLevel();
			MenuMusic = SoundPoolManager.LoadMusic(SoundPoolManager.SFXPath + "BGM_MainMenu.ogg");

			MenuMusic.Loop = true;
			MenuMusic.Volume = EngineReference.GlobalMusicVolume;
		}

		public void InitiateMenu()
		{

			// Font TODO 
			GUI = new JGUI(((SpaceSEMGameInstance)EngineReference.GameInstance).MainGameFont, EngineReference.EngineWindow, EngineReference.InputManager);

			// Root Container
			rootContainer = new JContainer(GUI);
			rootContainer.setBackgroundColor(new Color(0, 0, 0, 0));
			rootContainer.setPosition(new Vector2f(50, 50));
			rootContainer.setSize(new Vector2f(700, 700));

			JBorderLayout borderLayout = new JBorderLayout(rootContainer);
			borderLayout.TopSize = 0.2f;
			borderLayout.LeftSize = 0.2f;
			rootContainer.Layout = borderLayout;

			// Title
			title = new JLabel(GUI);
			title.Text.DisplayedString = "Space SEM";

			// Main Menue
			{
				mainLeftContainer = new JContainer(GUI);
				mainLeftContainer.Layout = new JLayout(mainLeftContainer);

				playCheckBox = new JCheckbox(GUI);
				playCheckBox.Text.DisplayedString = "Play";
				playCheckBox.Something += this.ChangeCenterContainer;

				optionCheckBox = new JCheckbox(GUI);
				optionCheckBox.Text.DisplayedString = "Options";
				optionCheckBox.Something += this.ChangeCenterContainer;

				helpCheckBox = new JCheckbox(GUI);
				helpCheckBox.Text.DisplayedString = "Help";
				helpCheckBox.Something += this.ChangeCenterContainer;


				creditsCheckBox = new JCheckbox(GUI);
				creditsCheckBox.Text.DisplayedString = "Credits";
				creditsCheckBox.Something += this.ChangeCenterContainer;


				editorsCheckBox = new JCheckbox(GUI);
				editorsCheckBox.Text.DisplayedString = "Editor";
				editorsCheckBox.Something += this.ChangeCenterContainer;
				editorsCheckBox.IsVisable = false;


				exitCheckBox = new JCheckbox(GUI);
				exitCheckBox.Text.DisplayedString = "Exit";
				exitCheckBox.Something += this.ChangeCenterContainer;


				JCheckboxGroup mainCheckBoxGroup = new JCheckboxGroup();
				mainCheckBoxGroup.AddBox(playCheckBox);
				mainCheckBoxGroup.AddBox(optionCheckBox);
				mainCheckBoxGroup.AddBox(helpCheckBox);
				mainCheckBoxGroup.AddBox(creditsCheckBox);
				mainCheckBoxGroup.AddBox(editorsCheckBox);
				mainCheckBoxGroup.AddBox(exitCheckBox);
			}
			// Container vvv
			// Play Menue
			{
				playContainer = new JContainer(GUI);

				JBorderLayout playContainerLayout = new JBorderLayout(playContainer);
				playContainerLayout.RightSize = 0.3f;
				playContainer.Layout = playContainerLayout;

				// Play Center
				JContainer playCenterContainer = new JContainer(GUI);
				playCenterContainer.Layout = new JLayout(playCenterContainer);

				// Campain Label
				JLabel playCampaingnLabel = new JLabel(GUI);
				playCampaingnLabel.setTextString("Campaign");

				// Campain Level
				JContainer playCampaignContainer = new JContainer(GUI);
				JGridLayout playCampaingnLayout = new JGridLayout(playCampaignContainer);
				playCampaingnLayout.Rows = 4;
				playCampaignContainer.Layout = playCampaingnLayout;

				playCampaingnLevel1 = new JCheckbox(GUI);
				playCampaingnLevel2 = new JCheckbox(GUI);
				playCampaingnLevel3 = new JCheckbox(GUI);
				playCampaingnLevel4 = new JCheckbox(GUI);

				playCampaingnLevel1.setTextString("01");
				playCampaingnLevel2.setTextString("02");
				playCampaingnLevel3.setTextString("03");
				playCampaingnLevel4.setTextString("04");

				playCampaignContainer.addElement(playCampaingnLevel1);
				playCampaignContainer.addElement(playCampaingnLevel2);
				playCampaignContainer.addElement(playCampaingnLevel3);
				playCampaignContainer.addElement(playCampaingnLevel4);

				// Custom Label
				JLabel playCustomLabel = new JLabel(GUI);
				playCustomLabel.setTextString("Custom");
				playCustomLabel.IsVisable = false;

				// Custom Level
				JContainer playCustomContainer = new JContainer(GUI);
				JGridLayout playCustomLayout = new JGridLayout(playCustomContainer);
				playCustomLayout.Rows = 4;
				playCustomContainer.Layout = playCustomLayout;

				JCheckbox playCustomLevel1 = new JCheckbox(GUI);
				JCheckbox playCustomLevel2 = new JCheckbox(GUI);
				JCheckbox playCustomLevel3 = new JCheckbox(GUI);
				JCheckbox playCustomLevel4 = new JCheckbox(GUI);

				playCustomLevel1.IsVisable = false;
				playCustomLevel2.IsVisable = false;
				playCustomLevel3.IsVisable = false;
				playCustomLevel4.IsVisable = false;

				playCustomLevel1.setTextString("01");
				playCustomLevel2.setTextString("02");
				playCustomLevel3.setTextString("03");
				playCustomLevel4.setTextString("04");

				playCustomContainer.addElement(playCustomLevel1);
				playCustomContainer.addElement(playCustomLevel2);
				playCustomContainer.addElement(playCustomLevel3);
				playCustomContainer.addElement(playCustomLevel4);

				// CheckBox Group for Level Selection
				JCheckboxGroup playCheckboxGroup = new JCheckboxGroup();
				playCheckboxGroup.AddBox(playCampaingnLevel1);
				playCheckboxGroup.AddBox(playCampaingnLevel2);
				playCheckboxGroup.AddBox(playCampaingnLevel3);
				playCheckboxGroup.AddBox(playCampaingnLevel4);

				playCheckboxGroup.AddBox(playCustomLevel1);
				playCheckboxGroup.AddBox(playCustomLevel2);
				playCheckboxGroup.AddBox(playCustomLevel3);
				playCheckboxGroup.AddBox(playCustomLevel4);

				playCampaingnLevel1.Select();

				// add Everything to Center Container
				playCenterContainer.addElement(playCampaingnLabel);
				playCenterContainer.addElement(playCampaignContainer);
				playCenterContainer.addElement(playCustomLabel);
				playCenterContainer.addElement(playCustomContainer);

				// Play Right
				JContainer playRightContainer = new JContainer(GUI);
				playRightContainer.Layout = new JLayout(playRightContainer);

				JLabel playRightNameLabel = new JLabel(GUI);
				playRightNameLabel.setTextString("Name");
				playRightNameLabel.IsVisable = false;


				JLabel playRightHighScoreLabel = new JLabel(GUI);
				playRightHighScoreLabel.setTextString("HighScore");
				playRightHighScoreLabel.IsVisable = false;

				JLabel playRightDoneLabel = new JLabel(GUI);
				playRightDoneLabel.setTextString("Done");
				playRightDoneLabel.IsVisable = false;

				JButton playRightStartButton = new JButton(GUI);
				playRightStartButton.setTextString("Start");
				playRightStartButton.Something += PlayRightStartButton_LoadLevel;

				playRightContainer.addElement(playRightNameLabel);
				playRightContainer.addElement(playRightHighScoreLabel);
				playRightContainer.addElement(playRightDoneLabel);
				playRightContainer.addElement(playRightStartButton);


				// add Everything to play Container
				playContainer.addElement(playCenterContainer, JBorderLayout.CENTER);
				playContainer.addElement(playRightContainer, JBorderLayout.RIGHT);

			}
			// Option Menue
			{
				optionContainer = new JContainer(GUI);
				optionContainer.Layout = new JLayout(optionContainer);

				// Musik
				JContainer musikContainer = new JContainer(GUI);
				JBorderLayout musikContainerLayout = new JBorderLayout(musikContainer);
				musikContainerLayout.LeftSize = 0.2f;
				musikContainerLayout.RightSize = 0.6f;
				musikContainer.Layout = musikContainerLayout;

				JLabel musikLabel = new JLabel(GUI);
				musikLabel.Text.DisplayedString = "Music";

				OnOffCheckbox musikBox = new OnOffCheckbox(GUI);
				musikBox.Select();
				musikBox.Something += MusikBox_Something;

				musicSlider = new JSlider(GUI);
				musicSlider.Something += MusicSlider_Something;

				musikContainer.addElement(musikLabel, JBorderLayout.LEFT);
				musikContainer.addElement(musikBox, JBorderLayout.CENTER);
				musikContainer.addElement(musicSlider, JBorderLayout.RIGHT);

				optionContainer.addElement(musikContainer);

				// Sound
				JContainer soundContainer = new JContainer(GUI);
				JBorderLayout soundContainerLayout = new JBorderLayout(soundContainer);
				soundContainerLayout.LeftSize = 0.2f;
				soundContainerLayout.RightSize = 0.6f;
				soundContainer.Layout = soundContainerLayout;

				JLabel soundLabel = new JLabel(GUI);
				soundLabel.Text.DisplayedString = "Sound";

				OnOffCheckbox soundBox = new OnOffCheckbox(GUI);
				soundBox.Select();
				soundBox.Something += SoundBox_Something;

				soundSlider = new JSlider(GUI);
				soundSlider.Something += SoundSlider_Something;

				soundContainer.addElement(soundLabel, JBorderLayout.LEFT);
				soundContainer.addElement(soundBox, JBorderLayout.CENTER);
				soundContainer.addElement(soundSlider, JBorderLayout.RIGHT);

				optionContainer.addElement(soundContainer);

				// Reselutions

				JContainer resContainer = new JContainer(GUI);
				JBorderLayout resContainerLayout = new JBorderLayout(resContainer);
				resContainerLayout.LeftSize = 0.2f;
				resContainerLayout.RightSize = 0.2f;
				resContainer.Layout = resContainerLayout;

				JButton resIncButton = new JButton(GUI);
				resIncButton.Text.DisplayedString = ">";
				resIncButton.IsVisable = false;

				JButton resDecButton = new JButton(GUI);
				resDecButton.Text.DisplayedString = "<";
				resDecButton.IsVisable = false;

				JChooser resChooser = new JChooser(GUI);
				resChooser.Choose.Add("800 X 800");
				resChooser.Choose.Add("1000 X 1000");
				resChooser.Choose.Add("1200 X 1200");
				resChooser.Next();
				resChooser.IsVisable = false;

				resIncButton.Something += resChooser.Next;
				resDecButton.Something += resChooser.Back;

				resContainer.addElement(resDecButton, JBorderLayout.LEFT);
				resContainer.addElement(resChooser, JBorderLayout.CENTER);
				resContainer.addElement(resIncButton, JBorderLayout.RIGHT);

				optionContainer.addElement(resContainer);

				// ScreenMode

				JContainer screenModeContainer = new JContainer(GUI);
				JBorderLayout screenModeContainerLayout = new JBorderLayout(screenModeContainer);
				screenModeContainerLayout.LeftSize = 0.2f;
				screenModeContainerLayout.RightSize = 0.2f;
				screenModeContainer.Layout = screenModeContainerLayout;

				JButton screenModeIncButton = new JButton(GUI);
				screenModeIncButton.Text.DisplayedString = ">";
				screenModeIncButton.IsVisable = false;

				JButton screenModeDecButton = new JButton(GUI);
				screenModeDecButton.Text.DisplayedString = "<";
				screenModeDecButton.IsVisable = false;

				JChooser screenModeLabel = new JChooser(GUI);

				screenModeLabel.Choose.Add("Window");
				screenModeLabel.Choose.Add("Window Borderless");
				screenModeLabel.Choose.Add("Fullscreen");
				screenModeLabel.Next();
				screenModeLabel.IsVisable = false;

				screenModeIncButton.Something += screenModeLabel.Next;
				screenModeDecButton.Something += screenModeLabel.Back;

				screenModeContainer.addElement(screenModeDecButton, JBorderLayout.LEFT);
				screenModeContainer.addElement(screenModeLabel, JBorderLayout.CENTER);
				screenModeContainer.addElement(screenModeIncButton, JBorderLayout.RIGHT);

				optionContainer.addElement(screenModeContainer);

				// Default Apply Cancel

				JContainer optionBottemContainer = new JContainer(GUI);
				JGridLayout optionBottemLayout = new JGridLayout(optionBottemContainer);
				optionBottemLayout.Rows = 3;
				optionBottemContainer.Layout = optionBottemLayout;

				JButton defaultOptionButton = new JButton(GUI);
				defaultOptionButton.Text.DisplayedString = "Default";
				defaultOptionButton.IsVisable = false;

				JButton applyOptionButton = new JButton(GUI);
				applyOptionButton.Text.DisplayedString = "Apply";
				applyOptionButton.IsVisable = false;

				JButton cancelOptionButton = new JButton(GUI);
				cancelOptionButton.Text.DisplayedString = "Cancel";
				cancelOptionButton.IsVisable = false;

				optionBottemContainer.addElement(defaultOptionButton);
				optionBottemContainer.addElement(applyOptionButton);
				optionBottemContainer.addElement(cancelOptionButton);

				optionContainer.addElement(optionBottemContainer);
			}
			// Help Menue

			helpContainer = new JContainer(GUI);
			helpContainer.Layout = new JLayout(helpContainer);
			JLabel helpTemp = new JLabel(GUI);
			helpTemp.Text.DisplayedString = "helpContainer";
			helpContainer.addElement(helpTemp);

			// Credit Menue

			creditContainer = new JContainer(GUI);
			creditContainer.Layout = new JLayout(creditContainer);
			JLabel creditTemp = new JLabel(GUI);
			creditTemp.Text.DisplayedString = "Made By Kevin Kügler and Jan Schult";
			creditContainer.addElement(creditTemp);

			// Editor Menue
			{
				editorContainer = new JContainer(GUI);
				JBorderLayout editContainerLayout = new JBorderLayout(editorContainer);
				editContainerLayout.RightSize = 0.3f;
				editorContainer.Layout = editContainerLayout;

				JContainer editCenterContainer = new JContainer(GUI);
				editCenterContainer.Layout = new JLayout(editCenterContainer);

				// Custom Label
				JLabel editCustomLabel = new JLabel(GUI);
				editCustomLabel.setTextString("Custom");
				editCustomLabel.IsVisable = false;

				// Custom Level
				JContainer editCustomContainer = new JContainer(GUI);
				JGridLayout editCustomLayout = new JGridLayout(editCustomContainer);
				editCustomLayout.Rows = 4;
				editCustomContainer.Layout = editCustomLayout;

				JCheckbox editCustomLevel1 = new JCheckbox(GUI);
				JCheckbox editCustomLevel2 = new JCheckbox(GUI);
				JCheckbox editCustomLevel3 = new JCheckbox(GUI);
				JCheckbox editCustomLevel4 = new JCheckbox(GUI);

				editCustomLevel1.setTextString("01");
				editCustomLevel2.setTextString("02");
				editCustomLevel3.setTextString("03");
				editCustomLevel4.setTextString("04");

				editCustomContainer.addElement(editCustomLevel1);
				editCustomContainer.addElement(editCustomLevel2);
				editCustomContainer.addElement(editCustomLevel3);
				editCustomContainer.addElement(editCustomLevel4);
				editCustomLevel1.Select();


				// CheckBox Group for Level Selection
				JCheckboxGroup editCheckboxGroup = new JCheckboxGroup();
				editCheckboxGroup.AddBox(editCustomLevel1);
				editCheckboxGroup.AddBox(editCustomLevel2);
				editCheckboxGroup.AddBox(editCustomLevel3);
				editCheckboxGroup.AddBox(editCustomLevel4);

				editCenterContainer.addElement(null);
				editCenterContainer.addElement(editCustomLabel);
				editCenterContainer.addElement(editCustomContainer);
				editCenterContainer.addElement(null);

				// Right Container

				JContainer editRightContainer = new JContainer(GUI);
				editRightContainer.Layout = new JLayout(editRightContainer);

				JLabel editLevelInfoLable = new JLabel(GUI);

				editLevelInfoLable.setTextString("Level Info");
				editRightContainer.addElement(editLevelInfoLable);

				JButton editStartButton = new JButton(GUI);
				editStartButton.setTextString("Start");
				editStartButton.Something += OpenEditor;
				editRightContainer.addElement(editStartButton);

				editorContainer.addElement(editCenterContainer, JBorderLayout.CENTER);
				editorContainer.addElement(editRightContainer, JBorderLayout.RIGHT);
			}
			// Exit Menue
			{
				exitContainer = new JContainer(GUI);
				JBorderLayout exitLayout = new JBorderLayout(exitContainer);
				exitLayout.TopSize = 0.2f;
				exitLayout.BottemSize = 0.2f;
				exitLayout.LeftSize = 0.2f;
				exitLayout.RightSize = 0.2f;
				exitContainer.Layout = exitLayout;

				JContainer exitCenterContainer = new JContainer(GUI);

				JGridLayout exitCenterLayout = new JGridLayout(exitCenterContainer);
				exitCenterLayout.Rows = 2;
				exitCenterContainer.Layout = exitCenterLayout;

				JButton exitExitButton = new JButton(GUI);
				exitExitButton.setTextString("YES");
				JButton exitCancelButton = new JButton(GUI);
				exitCancelButton.setTextString("NO");

				exitCancelButton.Something += delegate ()
				{
					exitCheckBox.Deselect();
					ChangeCenterContainer();
				};

				exitExitButton.Something += delegate ()
				{
					EngineReference.RequestTermination = true;
					this.Dispose();
				};

				exitCenterContainer.addElement(exitExitButton);
				exitCenterContainer.addElement(exitCancelButton);

				exitContainer.addElement(exitCenterContainer, JBorderLayout.CENTER);
			}
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

		private void MusicSlider_Something()
		{
			EngineReference.GlobalMusicVolume = (uint) musicSlider.SliderValue;
			MenuMusic.Volume = EngineReference.GlobalMusicVolume;
		}

		private void SoundSlider_Something()
		{
			EngineReference.GlobalSoundVolume = (uint)soundSlider.SliderValue;
		}

		private void MusikBox_Something()
		{
			EngineReference.GlobalMusicEnabled = !EngineReference.GlobalMusicEnabled;
			EngineReference.GlobalMusicVolume = (uint) (EngineReference.GlobalMusicEnabled ? 50 : 0);
			MenuMusic.Volume = EngineReference.GlobalMusicVolume;
		}

		private void SoundBox_Something()
		{
			EngineReference.GlobalSoundEnabled = !EngineReference.GlobalSoundEnabled;
			EngineReference.GlobalSoundVolume = (uint)(EngineReference.GlobalMusicEnabled ? 50 : 0);
		}

		public override void OnLevelLoad()
		{
			base.OnLevelLoad();
			InitiateMenu();
		}

		public void ChangeCenterContainer()
		{
			if (playCheckBox.IsSelected)
			{
				GUI.RootContainer.addElement(playContainer, JBorderLayout.CENTER);
			}
			else if (optionCheckBox.IsSelected)
			{
				GUI.RootContainer.addElement(optionContainer, JBorderLayout.CENTER);
			}
			else if (helpCheckBox.IsSelected)
			{
				GUI.RootContainer.addElement(helpContainer, JBorderLayout.CENTER);
			}
			else if (creditsCheckBox.IsSelected)
			{
				GUI.RootContainer.addElement(creditContainer, JBorderLayout.CENTER);
			}
			else if (editorsCheckBox.IsSelected)
			{
				GUI.RootContainer.addElement(editorContainer, JBorderLayout.CENTER);
			}
			else if (exitCheckBox.IsSelected)
			{
				GUI.RootContainer.addElement(exitContainer, JBorderLayout.CENTER);
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
			MenuMusic.Play();
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
			GUI.IsActive = false;
			MenuMusic.Stop();
		}

		public override void OnGameResume()
		{
			base.OnGameResume();
			GUI.IsActive = true;
			MenuMusic.Play();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
			MenuMusic.Stop();
		}

		// GUI Functions

		private void PlayRightStartButton_LoadLevel()
		{

			SpaceGameLevel level = new SpaceGameLevel();

			Console.WriteLine(playCampaingnLevel1);

			if (playCampaingnLevel1.IsSelected)
			{
				level.SpaceLevelID = 1;
			}
			else if (playCampaingnLevel2.IsSelected)
			{
				level.SpaceLevelID = 2;
			}
			else if (playCampaingnLevel3.IsSelected)
			{
				level.SpaceLevelID = 3;
			}
			else if (playCampaingnLevel4.IsSelected)
			{
				level.SpaceLevelID = 4;
			}


			EngineReference.LoadLevel(level, false);
		}

		public void OpenEditor()
		{

			SpaceEditorLevel edit = new SpaceEditorLevel();
			edit.LevelName = "level_1";

			EngineReference.LoadLevel(edit, false);
		}
	}
}