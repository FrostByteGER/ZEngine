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

			GUI gui = new GUI(new Font("D:/CShap/SFML_Engine/Assets/comic.ttf"), renderwindow, im);

			JContainer container = new JContainer(gui);

			container.setPosition(new Vector2f(100, 100));
			container.setSize(new Vector2f(600, 600));

			container.Box.FillColor = new Color(255, 255, 255);

			JBorderLayout tempLayout = new JBorderLayout(container);

			tempLayout.TopSize = 0.2f;
			tempLayout.BottemSize = 0.2f;
			tempLayout.LeftSize = 0.2f;
			tempLayout.RightSize = 0.2f;

			container.Layout = tempLayout;

			JChackboxGroup cgroup = new JChackboxGroup();

			JElement element1 = new JElement(gui);
			element1.setBackgroundColor(new Color(255, 0 ,0));
			JLabel element2 = new JLabel(gui);
			element2.Text.DisplayedString = "Lable";
			element2.Text.CharacterSize = 50;
			element2.Text.Color = new Color(255,255,255);
			element2.setBackgroundColor(new Color(0, 255, 0));
			JCheckbox element3 = new JCheckbox(gui);
			element3.setBackgroundColor (new Color(100, 100, 100));
			element3.Text.DisplayedString = "Checkbox";
			element3.Text.CharacterSize = 25;
			cgroup.AddBox(element3);
			JButton element4 = new JButton(gui);
			element4.Text.DisplayedString = "Button";
			element4.Text.CharacterSize = 50;
			element4.Text.Color = new Color(255, 255, 255, 50);
			element4.setBackgroundColor (new Color(0, 0, 255));


			JContainer container2 = new JContainer(gui);


			JGridLayout gridLayout = new JGridLayout(container2);

			gridLayout.Rows = 4;

			container2.Layout = gridLayout;

			JCheckbox element6 = new JCheckbox(gui);
			cgroup.AddBox(element6);
			JSlider element7 = new JSlider(gui);
			element7.setBackgroundColor( new Color(0, 255, 255));
			JSlider element8 = new JSlider(gui);
			element8.DisplayTyp = JSlider.VERTICAL;
			element8.setBackgroundColor(new Color(0, 255, 255));
			JElement element9 = new JElement(gui);


			container.addElement(element1, JBorderLayout.CENTER);
			container.addElement(element2, JBorderLayout.TOP);
			container.addElement(element3, JBorderLayout.BOTTOM);
			container.addElement(element4, JBorderLayout.LEFT);
			container.addElement(container2, JBorderLayout.BOTTOM);


			container2.addElement(element6);
			container2.addElement(element7);
			container2.addElement(element8);
			container2.addElement(element9);

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
