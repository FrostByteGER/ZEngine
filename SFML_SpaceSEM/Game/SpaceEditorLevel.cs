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
			MainLayout.RightSize = 0.3f;
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


			EditorSlider levelSlider = new EditorSlider(GUI);
			MainCenterContainer.addElement(levelSlider, JBorderLayout.BOTTOM);
			
	
			// Main Right
			
			JContainer MainRightContainer = new JContainer(GUI);

			JGridLayout RightLayout = new JGridLayout(MainRightContainer);
			RightLayout.Rows = 2;

			JElement test1 = new JElement(GUI);
			JElement test2 = new JElement(GUI);

			test1.Box.FillColor = Color.Green;
			test2.Box.FillColor = Color.Blue;

			MainRightContainer.addElement(test1);
			MainRightContainer.addElement(test2);



			MainEditContainer.addElement(MainCenterContainer, JBorderLayout.CENTER);
			MainEditContainer.addElement(MainRightContainer, JBorderLayout.RIGHT);

			GUI.RootContainer = MainEditContainer;
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
