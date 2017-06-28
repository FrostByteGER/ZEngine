using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.JUI;
using System;
using System.IO;

namespace SFML_TowerDefense.Source.GUI
{
	public class GUILevelTest : Level 
	{

		public JGUI GUI;

		private JContainer MenueContainer;
		private JContainer StartContainer;
		private JContainer OptionContainer;
		private JContainer CreditContainer;
		private JContainer ExitContainer;

		protected override void InitLevel()
		{
			base.InitLevel();
			initGUI();
		}

		private void initGUI()
		{
			GUI = new JGUI(new Font("./Assets/Game/Fonts/Main.ttf"), EngineReference.EngineWindow, EngineReference.InputManager);

			GUI.GUISpace.Position = new Vector2f(50,50);
			GUI.GUISpace.Size = new Vector2f(700,700);

			MenueContainer = initMainMenue();
			StartContainer = initStart();
			CreditContainer = initCredis();
			OptionContainer = initOption();
			ExitContainer = initExit();

			GUI.RootContainer = MenueContainer;
		}

		private JContainer initMainMenue()
		{
			JContainer container = new JContainer(GUI);

			container.Layout = new JLayout(container);

			JButton start = new JButton(GUI);
			start.setTextString("START");
			start.OnExecute += OnStartButton;
			container.addElement(start);

			JButton options = new JButton(GUI);
			options.setTextString("OPTION");
			options.OnExecute += OnOptionsButton;
			container.addElement(options);

			JButton credits = new JButton(GUI);
			credits.setTextString("CREDITS");
			credits.OnExecute += OnCreditsButton;
			container.addElement(credits);

			JButton exit = new JButton(GUI);
			exit.setTextString("EXIT");
			exit.OnExecute += OnExitButton;
			container.addElement(exit);

			return container;
		}

		private JContainer initStart()
		{
			JContainer container = new JContainer(GUI);
			container.Layout = new JLayout(container);

			JButton back = new JButton(GUI);
			back.setTextString("BACK");
			back.OnExecute += BackToMainMenue;
			container.addElement(back);

			return container;
		}

		private JContainer initOption()
		{
			JContainer container = new JContainer(GUI);
			container.Layout = new JLayout(container);

			container.addElement(CreateOptionSlider("MUSIK"));
			container.addElement(CreateOptionSlider("SOUND"));

			JButton back = new JButton(GUI);
			back.setTextString("BACK");
			back.OnExecute += BackToMainMenue;
			container.addElement(back);

			return container;
		}

		private JContainer initCredis()
		{
			JContainer container = new JContainer(GUI);
			container.Layout = new JLayout(container);

			JLabel creditLable = new JLabel(GUI);
			creditLable.setTextString("Made by Kevin and Jan");
			container.addElement(creditLable);

			JButton back = new JButton(GUI);
			back.setTextString("BACK");
			back.OnExecute += BackToMainMenue;
			container.addElement(back);

			return container;
		}

		private JContainer initExit()
		{
			JContainer container = new JContainer(GUI);

			JGridLayout layout = new JGridLayout(container);

			layout.Rows = 2;

			container.Layout = layout;

			JButton yes = new JButton(GUI);
			yes.setTextString("YES");
			yes.OnExecute += delegate ()
			{
				EngineReference.RequestTermination = true;
			};
			container.addElement(yes);

			JButton no = new JButton(GUI);
			no.setTextString("NO");
			no.OnExecute += BackToMainMenue;
			container.addElement(no);

			return container;
		}

		private JContainer CreateOptionSlider(String name)
		{
			JContainer container = new JContainer(GUI);

			JGridLayout layout = new JGridLayout(container);
			layout.Rows = 2;

			JContainer nameOnOffContainer = new JContainer(GUI);
			container.addElement(nameOnOffContainer);

			JBorderLayout layout2 = new JBorderLayout(nameOnOffContainer);
			layout2.RightSize = 0.2f;

			nameOnOffContainer.Layout = layout2;

			JLabel nameLabel = new JLabel(GUI);
			nameLabel.setTextString(name);
			nameOnOffContainer.addElement(nameLabel, JBorderLayout.CENTER);

			JCheckbox checkBox = CreateYesOnBox();
			nameOnOffContainer.addElement(checkBox, JBorderLayout.RIGHT);

			JContainer sliderValueContainer = new JContainer(GUI);
			container.addElement(sliderValueContainer);

			JBorderLayout layout3 = new JBorderLayout(sliderValueContainer);
			layout3.RightSize = 0.3f;
			layout3.LeftSize = 0.05f;

			sliderValueContainer.Layout = layout3;

			JSlider slider = new JSlider(GUI);
			sliderValueContainer.addElement(slider, JBorderLayout.CENTER);
			JDistanceContainer padding = new JDistanceContainer();

			padding.SetVertikal(0.1f);

			slider.Padding = padding;

			checkBox.OnExecute += delegate ()
			{
				if (checkBox.IsSelected)
				{
					slider.IsEnabled = true;
				}
				else
				{
					slider.IsEnabled = false;
				}
			};
			checkBox.Select();
			checkBox.Execute();

			JLabel value = new JLabel(GUI);
			value.setTextString(((int)(slider.SliderValue * 100)) + "%");
			slider.OnDrag += delegate ()
			{
				value.setTextString(((int)(slider.SliderValue * 100)) + "%");
			};
			sliderValueContainer.addElement(value, JBorderLayout.RIGHT);

			return container;
		}

		private JCheckbox CreateYesOnBox()
		{
			JCheckbox checkbox = new JCheckbox(GUI);

			checkbox.OnExecute += delegate ()
			{
				if (checkbox.IsSelected)
				{
					checkbox.setTextString("ON");
				}
				else
				{
					checkbox.setTextString("OFF");
				}
			};
			return checkbox;
		}

		public void OnStartButton()
		{
			GUI.RootContainer = StartContainer;
		}

		public void OnOptionsButton()
		{
			GUI.RootContainer = OptionContainer;
		}

		public void OnCreditsButton()
		{
			GUI.RootContainer = CreditContainer;
		}

		public void OnExitButton()
		{
			GUI.RootContainer = ExitContainer;
		}

		public void BackToMainMenue()
		{
			GUI.RootContainer = MenueContainer;
		}

		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);
			GUI.Tick(deltaTime);
		}

		protected override void LevelDraw(ref RenderWindow renderWindow)
		{
			base.LevelDraw(ref renderWindow);
			renderWindow.Draw(GUI);
		}
	}
}
