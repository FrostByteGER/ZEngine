using SFML_Engine.Engine.JUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML_Engine.Engine.IO;
using SFML_TowerDefense.Source.Game;
using SFML.System;
using SFML_TowerDefense.Source.Game.Buildings;
using SFML_TowerDefense.Source.Game.Buildings.Towers;

namespace SFML_TowerDefense.Source.GUI
{
	public class GameHud : JGUI
	{
		public TDFieldActor _SelectedField;
		public TDFieldActor SelectedField {
			get => _SelectedField;
			set
			{
				_SelectedField = value;
				ChangeSelectedField(SelectedField);
			}
		}

		// Contains PlayerInfo Money etc.
		public JContainer InfoContainer;

		// Contains Info of Selected Field
		public JContainer FieldContainer;

		public JContainer GeneralFieldContainer;
		public JContainer MineContainer;
		public JContainer ResouceContainer;
		public JContainer TowerContainer;
		public JContainer BuildingContainer;
		public JContainer BuildingFieldContainer;


		public JButton wave;

		public GameHud(Font font, RenderWindow renderwindow, InputManager inputManager) : base(font, renderwindow, inputManager)
		{

			GUISpace.Position = new Vector2f(0,0);
			GUISpace.Size = new Vector2f(800,800);

			InfoContainer = InitInfoContainer();
			FieldContainer = InitFieldContainer();

			GeneralFieldContainer = InitGeneralFieldContainer();
			MineContainer = InitMineContainer();
			ResouceContainer = InitResouceContainer();
			TowerContainer = InitTowerContainer();
			BuildingContainer = InitBuildingCointainer();
			BuildingFieldContainer = InitBuildingFieldContainer();

			RootContainer = new JContainer(this);
			RootContainer.setBackgroundColor(new Color(255,255,255,0));

			JBorderLayout layout = new JBorderLayout(RootContainer);
			layout.TopSize = 0.1f;
			layout.BottemSize = 0.2f;
			RootContainer.Layout = layout;
			RootContainer.addElement(InfoContainer, JBorderLayout.TOP);
			RootContainer.addElement(BuildingFieldContainer, JBorderLayout.BOTTOM);
		}

		private JContainer InitInfoContainer()
		{
			JContainer container = new JContainer(this);

			JGridLayout layout = new JGridLayout(container);
			layout.Rows = 7;

			container.Layout = layout;

			wave = new JButton(this);
			wave.Text.CharacterSize = 12;
			wave.setTextString("WAVE X of X");
			container.addElement(wave);

			JButton enemieRemaining = new JButton(this);
			enemieRemaining.Text.CharacterSize = 12;
			enemieRemaining.setTextString("Enemies: X");
			container.addElement(enemieRemaining);

			JButton health = new JButton(this);
			health.Text.CharacterSize = 12;
			health.setTextString("Health: X");
			container.addElement(health);

			JButton gold = new JButton(this);
			gold.Text.CharacterSize = 12;
			gold.setTextString("Gold: X");
			container.addElement(gold);

			JButton score = new JButton(this);
			score.Text.CharacterSize = 12;
			score.setTextString("Score: X");
			container.addElement(score);

			container.addElement(null);

			JButton menu = new JButton(this);
			menu.Text.CharacterSize = 12;
			menu.setTextString("Menu");
			container.addElement(menu);

			return container;
		}

		private JContainer InitFieldContainer()
		{
			JContainer container = new JContainer(this);

			return container;
		}

		private JContainer InitBuildingCointainer()
		{
			JContainer container = new JContainer(this);

			return container;
		}

		private JContainer InitGeneralFieldContainer()
		{
			JContainer container = new JContainer(this);

			return container;
		}

		private JContainer InitMineContainer()
		{
			JContainer container = new JContainer(this);

			return container;
		}

		private JContainer InitTowerContainer()
		{
			JContainer container = new JContainer(this);

			return container;
		}

		private JContainer InitResouceContainer()
		{
			JContainer container = new JContainer(this);

			return container;
		}

		private JContainer InitBuildingFieldContainer()
		{
			JContainer container = new JContainer(this);

			JBorderLayout layout = new JBorderLayout(container);
			layout.LeftSize = 0.2f;

			JContainer leftContainer = new JContainer(this);
			leftContainer.Layout = new JLayout(leftContainer);
			container.addElement(leftContainer, JBorderLayout.LEFT);

			JCheckbox tower1 = new JCheckbox(this);
			tower1.setTextString("Tower1");
			tower1.Select();
			leftContainer.addElement(tower1);

			JCheckbox tower2 = new JCheckbox(this);
			tower2.setTextString("Tower2");
			leftContainer.addElement(tower2);

			JCheckbox tower3 = new JCheckbox(this);
			tower3.setTextString("Tower3");
			leftContainer.addElement(tower3);

			JButton build = new JButton(this);
			build.setTextString("Build");
			leftContainer.addElement(build);

			JCheckboxGroup towerGroup = new JCheckboxGroup();
			towerGroup.AddBox(tower1);
			towerGroup.AddBox(tower2);
			towerGroup.AddBox(tower3);

			JLabel stats = new JLabel(this);
			stats.setTextString("TowerStats");
			container.addElement(stats, JBorderLayout.CENTER);

			return container;
		}

		private void ChangeSelectedField(TDFieldActor fieldActor)
		{
			if (fieldActor == null)
			{
				FieldContainer = null;
				return;
			}
			else if(fieldActor is TDTower){
				FieldContainer = TowerContainer;
			}
			else if (fieldActor is TDMine)
			{
				FieldContainer = MineContainer;
			}
			else if (fieldActor is TDResource)
			{
				FieldContainer = ResouceContainer;
			}
			else if (fieldActor is TDBuilding)
			{
				FieldContainer = BuildingContainer;
			}
			else if(fieldActor is TDFieldActor)
			{
				FieldContainer = GeneralFieldContainer;
			}
			UpdateFieldContainer();
		}

		//TODO
		private void UpdateFieldContainer()
		{
			if (SelectedField == null)
			{
				return;
			}
			else if (SelectedField is TDTower)
			{
				TDTower fieldActor = (TDTower)SelectedField;

			}
			else if (SelectedField is TDMine)
			{
			}
			else if (SelectedField is TDResource)
			{
			}
			else if (SelectedField is TDBuilding)
			{
			}
			else if (SelectedField is TDFieldActor)
			{
			}
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			UpdateFieldContainer();
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{	
			base.Draw(target, states);
		}
	}
}
