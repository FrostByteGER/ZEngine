using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.JUI;
using System;

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

			GUI gui = new GUI(new Font("D:/CShap/SFML_Engine/Assets/arial.ttf"), renderwindow, im);

			gui.InputManager = im;

			JContainer container = new JContainer(gui);

			container.Box.Position = new Vector2f(100, 100);
			container.Box.Size = new Vector2f(200, 500);
			container.Box.FillColor = new Color(255, 255, 255);

			container.Layout = new JLayout(container);
			

			JElement element1 = new JElement(gui);
			element1.setBackgroundColor(new Color(255, 0 ,0));
			JLabel element2 = new JLabel(gui);
			element2.Text.DisplayedString = "Lable";
			element2.Text.CharacterSize = 50;
			element2.Text.Color = new Color(255,255,255);
			//element2.Text.Font = new Font("D:/CShap/SFML_Engine/Assets/arial.ttf");
			element2.setBackgroundColor(new Color(0, 255, 0));
			JCheckbox element3 = new JCheckbox(gui);
			element3.setBackgroundColor (new Color(255, 255, 0));
			element3.Text.DisplayedString = "Checkbox";
			element3.Text.CharacterSize = 50;
			element3.Text.Color = new Color(200, 200, 200);
			JButton element4 = new JButton(gui);
			element4.Text.DisplayedString = "Button";
			element4.Text.CharacterSize = 50;
			element4.Text.Color = new Color(255, 255, 255);
			element4.setBackgroundColor (new Color(0, 0, 255));

			/*

			JContainer container2 = new JContainer(gui);

			container2.Box.Position = new Vector2f(100, 100);
			container2.Box.Size = new Vector2f(200, 500);
			container2.Box.FillColor = new Color(255, 255, 255);

			container2.Layout = new JLayout(container2);


			JCheckbox element6 = new JCheckbox(gui);
			element6.setBackgroundColor (new Color(255, 0, 255));
			JElement element7 = new JElement(gui);
			element7.setBackgroundColor( new Color(0, 255, 255));
			
			*/

			container.addElement(element1);
			container.addElement(element2);
			container.addElement(element3);
			container.addElement(element4);
			//container.addElement(container2);


			//container2.addElement(element6);
			//container2.addElement(element7);

			gui.RootContainer = container;

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
