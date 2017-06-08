using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.JUI;
using SFML_SpaceSEM.IO;
using SFML_SpaceSEM.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace SFML_SpaceSEM.Game
{
	public class SpaceEditorLevel : Level
	{

		//ShipeTypeList TODO

		private SpaceLevelDataWrapper SpawnData { get; set; }

		// Spawner with Time
		private List<SpaceLevelSpawnerDataWrapper> Spawners;

		private SpaceLevelSpawnerDataWrapper SelectedSpawner;

		private JCheckbox elementCheckBoxSpawner;
		private JCheckbox elementCheckBoxShip1;
		private JCheckbox elementCheckBoxShip2;
		private JCheckbox elementCheckBoxShip3;
		private JCheckbox elementCheckBoxShip4;

		private JGUI GUI { get; set; }

		public SpaceEditorLevel()
		{
			
		}

		private void initEditor()
		{
			GUI = new JGUI(((SpaceSEMGameInstance)EngineReference.GameInstance).MainGameFont, EngineReference.EngineWindow, EngineReference.InputManager);

			JContainer MainEditContainer = new JContainer(GUI);
			MainEditContainer.setBackgroundColor(new Color(0, 0, 0, 0));
			MainEditContainer.setPosition(new Vector2f(50, 50));
			MainEditContainer.setSize(new Vector2f(700, 700));
			

			JBorderLayout MainLayout = new JBorderLayout(MainEditContainer);
			MainLayout.RightSize = 0.4f;
			MainEditContainer.Layout = MainLayout;

			// Main Center
			
			JContainer MainCenterContainer = new JContainer(GUI);

			JBorderLayout MainCenterContainerLayout = new JBorderLayout(MainCenterContainer);

			MainCenterContainerLayout.TopSize = 0.1f;
			MainCenterContainerLayout.BottemSize = 0.1f;

			MainCenterContainer.Layout = MainCenterContainerLayout;

			JLabel LevelNameLable = new JLabel(GUI);
			LevelNameLable.setTextString("Level Name");
			MainCenterContainer.addElement(LevelNameLable, JBorderLayout.TOP);


			EditCenterElement CenterElement = new EditCenterElement(GUI);
			MainCenterContainer.addElement(CenterElement, JBorderLayout.CENTER);


			JContainer levelSlideCpntainer = new JContainer(GUI);
			JBorderLayout levelSliderLayout = new JBorderLayout(levelSlideCpntainer);
			levelSliderLayout.RightSize = 0.35f;

			EditorSlider levelSlider = new EditorSlider(GUI);

			JLabel sliderLable = new JLabel(GUI);
			levelSlider.LinkedLable = sliderLable;
			sliderLable.setTextString("Time :300");

			levelSlideCpntainer.addElement(levelSlider, JBorderLayout.CENTER);
			levelSlideCpntainer.addElement(sliderLable, JBorderLayout.RIGHT);

			MainCenterContainer.addElement(levelSlideCpntainer, JBorderLayout.BOTTOM);
			

			// Main Right
			
			JContainer MainRightContainer = new JContainer(GUI);

			JGridLayout RightLayout = new JGridLayout(MainRightContainer);
			RightLayout.Rows = 2;


			// Ships
			JContainer shipContainer = new JContainer(GUI);

			JBorderLayout shipContainerLayout = new JBorderLayout(shipContainer);
			shipContainerLayout.TopSize = 0.1f;
			shipContainerLayout.BottemSize = 0.1f;

			JLabel shipLable = new JLabel(GUI);
			shipLable.setTextString("Ships");
			shipContainer.addElement(shipLable, JBorderLayout.TOP);

			// TODO List of all Ships is a Spawner

			JLabel shipList = new JLabel(GUI);
			shipList.setTextString("test\ntest\ntest\ntest\ntest\ntest\ntest\ntest");
			shipContainer.addElement(shipList, JBorderLayout.CENTER);

			JButton removeButton = new JButton(GUI);
			removeButton.setTextString("Remove");
			shipContainer.addElement(removeButton, JBorderLayout.BOTTOM);

			MainRightContainer.addElement(shipContainer);

			// Elements

			JContainer elementContainer = new JContainer(GUI);

			JBorderLayout elementContainerLayout = new JBorderLayout(elementContainer);
			elementContainerLayout.TopSize = 0.1f;
			elementContainerLayout.BottemSize = 0.2f;

			JLabel elementLabel = new JLabel(GUI);
			elementLabel.setTextString("Elements");
			elementContainer.addElement(elementLabel, JBorderLayout.TOP);

			JContainer elementListContainer = new JContainer(GUI);
			elementListContainer.Layout = new JLayout(elementListContainer);

			elementCheckBoxSpawner = new JCheckbox(GUI);
			elementCheckBoxShip1 = new JCheckbox(GUI);
			elementCheckBoxShip2 = new JCheckbox(GUI);
			elementCheckBoxShip3 = new JCheckbox(GUI);
			elementCheckBoxShip4 = new JCheckbox(GUI);

			elementCheckBoxSpawner.setTextString("Spawner");
			elementCheckBoxShip1.setTextString("Ship1");
			elementCheckBoxShip2.setTextString("Ship2");
			elementCheckBoxShip3.setTextString("Ship3");
			elementCheckBoxShip4.setTextString("Ship4");

			JCheckboxGroup elementCheckboxGroup = new JCheckboxGroup();
			elementCheckboxGroup.AddBox(elementCheckBoxSpawner);
			elementCheckboxGroup.AddBox(elementCheckBoxShip1);
			elementCheckboxGroup.AddBox(elementCheckBoxShip2);
			elementCheckboxGroup.AddBox(elementCheckBoxShip3);
			elementCheckboxGroup.AddBox(elementCheckBoxShip4);

			elementListContainer.addElement(elementCheckBoxSpawner);
			elementListContainer.addElement(elementCheckBoxShip1);
			elementListContainer.addElement(elementCheckBoxShip2);
			elementListContainer.addElement(elementCheckBoxShip3);
			elementListContainer.addElement(elementCheckBoxShip4);

			elementContainer.addElement(elementListContainer, JBorderLayout.CENTER);

			// Exit Save add

			JContainer exitSaveAddContainer = new JContainer(GUI);
			exitSaveAddContainer.Layout = new JLayout(exitSaveAddContainer);

			JButton addButton = new JButton(GUI);
			addButton.setTextString("ADD");
			addButton.Something += delegate ()
			{
				addElement();
			};
			exitSaveAddContainer.addElement(addButton);

			JButton saveButton = new JButton(GUI);
			saveButton.setTextString("SAVE");
			exitSaveAddContainer.addElement(saveButton);

			JButton exitButton = new JButton(GUI);
			exitButton.setTextString("EXIT");
			exitSaveAddContainer.addElement(exitButton);

			elementContainer.addElement(exitSaveAddContainer, JBorderLayout.BOTTOM);

			MainRightContainer.addElement(elementContainer);

			MainEditContainer.addElement(MainCenterContainer, JBorderLayout.CENTER);
			MainEditContainer.addElement(MainRightContainer, JBorderLayout.RIGHT);

			GUI.RootContainer = MainEditContainer;
		}

		public void addElement()
		{
			if (elementCheckBoxSpawner.IsSelected)
			{
				addSpawner();
			}
			else
			{
				addShip();
			}
		}

		public void addSpawner()
		{
			Console.WriteLine("addSpawner");
		}

		public void addShip()
		{
			Console.WriteLine("addShip");
		}

		public override void OnLevelLoad()
		{
			base.OnLevelLoad();
			initEditor();
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
