using SFML_Engine.Engine.JUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML_Engine.Engine.IO;
using SFML_TowerDefense.Source.Game;

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

		public GameHud(Font font, RenderWindow renderwindow, InputManager inputManager) : base(font, renderwindow, inputManager)
		{

			InfoContainer = InitInfoContainer();
			FieldContainer = InitFieldContainer();

			GeneralFieldContainer = InitGeneralFieldContainer();
			BuildingCointainer = InitBuildingCointainer();
			MineContainer = InitMineContainer();
			TowerContainer = InitTowerContainer();
			ResouceContainer = InitResouceContainer();

		}

		private JContainer InitInfoContainer()
		{
			JContainer container = new JContainer(this);

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
			if (FieldContainer == null)
			{
				return;
			}
			else if (FieldContainer is TDTower)
			{
			}
			else if (FieldContainer is TDMine)
			{
			}
			else if (FieldContainer is TDResource)
			{
			}
			else if (FieldContainer is TDBuilding)
			{
			}
			else if (FieldContainer is TDFieldActor)
			{
			}
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			UpdateFieldContainer();
		}
	}
}
