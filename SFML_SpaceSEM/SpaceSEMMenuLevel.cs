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