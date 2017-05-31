using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.JUI;
using System;
using System.IO;

namespace SFML_Engine.Engine
{
    internal sealed class Start
    {
		public bool open { set; get; } = true;

		public void OnEngineWindowClose(object sender, EventArgs args)
		{
			// Close the window when OnClose event is received
			var window = (RenderWindow)sender;
			window.Close();
			open = true;
		}

		public static void Main(string[] args)
        {

			Start st = new Start();

			RenderWindow renderwindow = new RenderWindow(new VideoMode(800, 800), "Test JUI", Styles.Titlebar | Styles.Close);

			renderwindow.SetFramerateLimit(60);
			renderwindow.SetKeyRepeatEnabled(true);
			renderwindow.SetActive();
			renderwindow.Display();
			renderwindow.SetVisible(true);

			InputManager im = new InputManager();

			JGUI gui = new JGUI(new Font("./comic.ttf"), renderwindow, im);

			// Root Container
			JContainer rootContainer = new JContainer(gui);

			rootContainer.setPosition(new Vector2f(100, 100));
			rootContainer.setSize(new Vector2f(600, 600));

			JBorderLayout borderLayout = new JBorderLayout(rootContainer);

			borderLayout.TopSize = 0.2f;
			borderLayout.BottemSize = 0.2f;
			borderLayout.LeftSize = 0.2f;
			borderLayout.RightSize = 0.2f;

			rootContainer.Layout = borderLayout;

			// Title
			JLabel title = new JLabel(gui);
			title.Text.DisplayedString = "Game Title";

			// Main Menue
			JContainer mainLeftContainer = new JContainer(gui);
			mainLeftContainer.Layout = new JLayout(mainLeftContainer);

			JCheckbox playCheckBox = new JCheckbox(gui);
			playCheckBox.Text.DisplayedString = "Play";

			JCheckbox optionCheckBox = new JCheckbox(gui);
			optionCheckBox.Text.DisplayedString = "Options";

			JCheckbox helpCheckBox = new JCheckbox(gui);
			helpCheckBox.Text.DisplayedString = "Help";

			JCheckbox creditsCheckBox = new JCheckbox(gui);
			creditsCheckBox.Text.DisplayedString = "Credits";

			JCheckbox editorsCheckBox = new JCheckbox(gui);
			editorsCheckBox.Text.DisplayedString = "Editor";
			editorsCheckBox.IsEnabled = false;

			JCheckbox exitCheckBox = new JCheckbox(gui);
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
			JContainer playContainer = new JContainer(gui);
			playContainer.Layout = new JLayout(playContainer);
			JLabel playTemp = new JLabel(gui);
			playTemp.Text.DisplayedString = "PlayTemp";
			playContainer.addElement(playTemp);


			// Option Menue

			JContainer optionContainer = new JContainer(gui);
			optionContainer.Layout = new JLayout(playContainer);
			JLabel optionTemp = new JLabel(gui);
			optionTemp.Text.DisplayedString = "OptionTemp";
			optionContainer.addElement(optionTemp);

			// Help Menue

			JContainer helpContainer = new JContainer(gui);
			helpContainer.Layout = new JLayout(playContainer);
			JLabel helpTemp = new JLabel(gui);
			helpTemp.Text.DisplayedString = "OptionTemp";
			helpContainer.addElement(helpTemp);

			// Credit Menue

			JContainer creditContainer = new JContainer(gui);
			creditContainer.Layout = new JLayout(playContainer);
			JLabel creditTemp = new JLabel(gui);
			creditTemp.Text.DisplayedString = "OptionTemp";
			creditContainer.addElement(creditTemp);

			// Editor Menue

			JContainer editorContainer = new JContainer(gui);
			editorContainer.Layout = new JLayout(playContainer);
			JLabel editorTemp = new JLabel(gui);
			editorTemp.Text.DisplayedString = "OptionTemp";
			editorContainer.addElement(editorTemp);

			// Exit Menue

			JContainer exitContainer = new JContainer(gui);
			exitContainer.Layout = new JLayout(playContainer);
			JLabel exitTemp = new JLabel(gui);
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


			gui.RootContainer = rootContainer;

			renderwindow.Closed += st.OnEngineWindowClose;



			while (st.open)
			{
				
				renderwindow.Clear();
				//container.Layout.ReSize();
				renderwindow.Draw(gui);
				renderwindow.DispatchEvents();
				renderwindow.Display();
				gui.Tick(0.1F);
			}

			/*
			JContainer container = new JContainer();

			container.Position = new Vector2f(100, 100);
			container.Size = new Vector2f(200, 500);

			container.Layout = new JLayout(container);

			JElement element1 = new JElement();
			JElement element2 = new JElement();
			JElement element3 = new JElement();
			JElement element4 = new JElement();

			container.addElement(element1);
			container.addElement(element2);
			container.addElement(element3);
			container.addElement(element4);

			Engine engine = Engine.Instance;
	        engine.EngineWindowWidth = 800;
	        engine.EngineWindowHeight = 600;
			engine.InitEngine();

            var Level = new Level();

			engine.RegisterLevel(Level);
			engine.StartEngine();
            Console.ReadLine();
			*/
		}
	}
}
