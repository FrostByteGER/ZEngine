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
		public JContainer BuildingCointainer;
		public JContainer MineContainer;
		public JContainer TowerContainer;
		public JContainer ResouceContainer;

		public JButton wave;

		public GameHud(Font font, RenderWindow renderwindow, InputManager inputManager) : base(font, renderwindow, inputManager)
		{

			GUISpace.Position = new Vector2f(0,0);
			GUISpace.Size = new Vector2f(800,800);

			InfoContainer = InitInfoContainer();
			FieldContainer = InitFieldContainer();

			GeneralFieldContainer = InitGeneralFieldContainer();
			BuildingCointainer = InitBuildingCointainer();
			MineContainer = InitMineContainer();
			TowerContainer = InitTowerContainer();
			ResouceContainer = InitResouceContainer();

			RootContainer = new JContainer(this);
			RootContainer.setBackgroundColor(new Color(255,255,255,0));

			JBorderLayout layout = new JBorderLayout(RootContainer);
			layout.TopSize = 0.2f;

			RootContainer.Layout = layout;
		
			RootContainer.addElement(InfoContainer, JBorderLayout.TOP);
		}

		private JContainer InitInfoContainer()
		{
			JContainer container = new JContainer(this);

			JGridLayout layout = new JGridLayout(container);
			layout.Rows = 2;

			container.Layout = layout;

			wave = new JButton(this);
			wave.setTextString("WAVE X / X");

			wave.OnEnter += delegate ()
			{
				Console.WriteLine("OnEnter");
			};

			container.addElement(wave);

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
				FieldContainer = BuildingCointainer;
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
