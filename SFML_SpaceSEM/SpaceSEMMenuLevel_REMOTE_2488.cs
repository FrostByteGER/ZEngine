using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.JUI;
using System;
using SFML_Engine.Engine.Game;
using SFML_SpaceSEM.UI;

namespace SFML_SpaceSEM
{
	public class SpaceSEMMenuLevel : Level
	{

		public static Font MainGameFont { get; set; }
		private SpaceGUI GUI;

		public JContainer rootContainer;
		public JLabel title;

		public JContainer mainLeftContainer;

		public JCheckbox playCheckBox;
		public JContainer playContainer;

		public JCheckbox optionCheckBox;
		public JContainer optionContainer;


		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
			GUI.Tick(deltaTime);
		}

		public static Color ColorSelected { get; } = new Color(255, 255, 255, 255);
		public static Color ColorUnselected { get; } = new Color(162, 160, 160, 255);
		public object Menu { get; internal set; }


		protected override void InitLevel()
		{
			base.InitLevel();
			InitiateMenu();
		}

		private void InitiateMenu()
		{

			// Font TODO 
			GUI = new SpaceGUI(MainGameFont, EngineReference.EngineWindow, EngineReference.InputManager);

			// Root Container
			rootContainer = new JContainer(GUI);

			rootContainer.setBackgroundColor( Color.Blue);

			rootContainer.setPosition(new Vector2f(50, 50));
			rootContainer.setSize(new Vector2f(700, 700));

			JBorderLayout borderLayout = new JBorderLayout(rootContainer);

			borderLayout.TopSize = 0.1f;
			//borderLayout.BottemSize = 0.2f;
			borderLayout.LeftSize = 0.2f;
			//borderLayout.RightSize = 0.2f;

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

			optionCheckBox = new JCheckbox(GUI);
			optionCheckBox.Text.DisplayedString = "Options";
			optionCheckBox.Something += this.ChangeCenterContainer;

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
			base.OnLevelLoad();
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
	}
}