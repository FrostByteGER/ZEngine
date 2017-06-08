using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.JUI;
using System;
using SFML_Engine.Engine.Game;
using SFML_SpaceSEM.Game;
using SFML_SpaceSEM.UI;

namespace SFML_SpaceSEM
{
	public class SpaceSEMMenuLevel : Level
	{

		public Font MainGameFont { get; set; }
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

		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
			GUI.Tick(deltaTime);
		}

		public void InitiateMenu()
		{

			// Font TODO 
			GUI = new JGUI(MainGameFont, EngineReference.EngineWindow, EngineReference.InputManager);

			// Root Container
			rootContainer = new JContainer(GUI);
			rootContainer.setBackgroundColor(new Color(0,0,0,0));
			rootContainer.setPosition(new Vector2f(50, 50));
			rootContainer.setSize(new Vector2f(700, 700));

			JBorderLayout borderLayout = new JBorderLayout(rootContainer);
			borderLayout.TopSize = 0.2f;
			borderLayout.LeftSize = 0.2f;
			rootContainer.Layout = borderLayout;

			// Title
			title = new JLabel(GUI);
			title.Text.DisplayedString = "Game Title";

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
				playCampaingnLabel.setTextString("Campaingn");

				// Campain Level
				JContainer playCampaignContainer = new JContainer(GUI);
				JGridLayout playCampaingnLayout = new JGridLayout(playCampaignContainer);
				playCampaingnLayout.Rows = 4;
				playCampaignContainer.Layout = playCampaingnLayout;

				JCheckbox playCampaingnLevel1 = new JCheckbox(GUI);
				JCheckbox playCampaingnLevel2 = new JCheckbox(GUI);
				JCheckbox playCampaingnLevel3 = new JCheckbox(GUI);
				JCheckbox playCampaingnLevel4 = new JCheckbox(GUI);

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

				// Custom Level
				JContainer playCustomContainer = new JContainer(GUI);
				JGridLayout playCustomLayout = new JGridLayout(playCustomContainer);
				playCustomLayout.Rows = 4;
				playCustomContainer.Layout = playCustomLayout;

				JCheckbox playCustomLevel1 = new JCheckbox(GUI);
				JCheckbox playCustomLevel2 = new JCheckbox(GUI);
				JCheckbox playCustomLevel3 = new JCheckbox(GUI);
				JCheckbox playCustomLevel4 = new JCheckbox(GUI);

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

				JLabel playRightHighScoreLabel = new JLabel(GUI);
				playRightHighScoreLabel.setTextString("HighScore");

				JLabel playRightDoneLabel = new JLabel(GUI);
				playRightDoneLabel.setTextString("Done");

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
				musikLabel.Text.DisplayedString = "Musik";

				OnOffCheckbox musikBox = new OnOffCheckbox(GUI);
				musikBox.Select();

				JSlider musikSlider = new JSlider(GUI);

				musikContainer.addElement(musikLabel, JBorderLayout.LEFT);
				musikContainer.addElement(musikBox, JBorderLayout.CENTER);
				musikContainer.addElement(musikSlider, JBorderLayout.RIGHT);

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

				JSlider soundSlider = new JSlider(GUI);

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

				JButton resDecButton = new JButton(GUI);
				resDecButton.Text.DisplayedString = "<";

				JChooser resChooser = new JChooser(GUI);
				resChooser.Choose.Add("800 X 800");
				resChooser.Choose.Add("1000 X 1000");
				resChooser.Choose.Add("1200 X 1200");
				resChooser.Next();

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

				JButton screenModeDecButton = new JButton(GUI);
				screenModeDecButton.Text.DisplayedString = "<";

				JChooser screenModeLabel = new JChooser(GUI);

				screenModeLabel.Choose.Add("Window");
				screenModeLabel.Choose.Add("Window Borderless");
				screenModeLabel.Choose.Add("Fullscreen");
				screenModeLabel.Next();

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

				JButton applyOptionButton = new JButton(GUI);
				applyOptionButton.Text.DisplayedString = "Apply";

				JButton cancelOptionButton = new JButton(GUI);
				cancelOptionButton.Text.DisplayedString = "Cancel";

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

				// CheckBox Group for Level Selection
				JCheckboxGroup editCheckboxGroup = new JCheckboxGroup();
				editCheckboxGroup.AddBox(editCustomLevel1);
				editCheckboxGroup.AddBox(editCustomLevel2);
				editCheckboxGroup.AddBox(editCustomLevel3);
				editCheckboxGroup.AddBox(editCustomLevel4);

				editCenterContainer.addElement(editCustomLabel);
				editCenterContainer.addElement(editCustomContainer);

				editorContainer.addElement(editCenterContainer, JBorderLayout.CENTER);
				//editorContainer.addElement(editCustomLabel, JBorderLayout.TOP);
			}
			// Exit Menue

			exitContainer = new JContainer(GUI);
			exitContainer.Layout = new JLayout(exitContainer);
			JLabel exitTemp = new JLabel(GUI);
			exitTemp.Text.DisplayedString = "exitContainer";
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

		private void PlayRightStartButton_LoadLevel()
		{
			EngineReference.LoadLevel(new SpaceGameLevel(), false);
		}

		public bool LoadSpaceLevel(string levelName)
		{
			return LoadSpaceLevel(levelName, true);
		}

		public bool LoadSpaceLevel(string levelName, bool destroyPrevious)
		{
			if (string.IsNullOrWhiteSpace(levelName)) return false;

			var level = new Level();

			return EngineReference.LoadLevel(level, destroyPrevious);
		}
	}
}