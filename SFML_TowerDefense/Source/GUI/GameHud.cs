using SFML_Engine.Engine.JUI;
using System;
using SFML.Graphics;
using SFML_Engine.Engine.IO;
using SFML.System;
using SFML_TowerDefense.Source.Game.Core;
using SFML_TowerDefense.Source.Game.TileMap;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;
using SFML_TowerDefense.Source.Game.Buildings.Towers;

namespace SFML_TowerDefense.Source.GUI
{
	public class GameHud : JGUI
	{
		public TDTile _SelectedField;
		public TDTile SelectedField {
			get => _SelectedField;
			set
			{
				_SelectedField = value;
				ChangeSelectedField(SelectedField);
			}
		}

		// Contains PlayerInfo Money etc.
		public JContainer InfoContainer;
		public JLabel wave;
		public JLabel enemieRemaining;
		public JLabel health;
		public JLabel gold;
		public JLabel score;

		// Contains Info of Selected Field
		public JContainer FieldContainer;

		public JContainer MineContainer;
		public JContainer ResouceContainer;
		public JContainer TowerContainer;
		public JContainer BuildingFieldContainer;
		public JContainer NexusContainer;

		public JContainer MenuDropDownContainer;

		//Tower
		public JCheckbox LaserTower;
		public JCheckbox PlasmaTower;
		public JCheckbox RailgunTower;

		public JLabel stats;

		//public TDGameInfo GameInfoHud;
		public TDGameMode GameModeHud;

		public GameHud(Font font, RenderWindow renderwindow, InputManager inputManager) : base(font, renderwindow, inputManager)
		{

			GUISpace.Position = new Vector2f(0,0);
			GUISpace.Size = new Vector2f(800,800);

			InfoContainer = InitInfoContainer();

			//GeneralFieldContainer = InitGeneralFieldContainer();
			MineContainer = InitMineContainer();
			ResouceContainer = InitResouceContainer();
			TowerContainer = InitTowerContainer();
			BuildingFieldContainer = InitBuildingFieldContainer();
			NexusContainer = InitNexusContainer();

			MenuDropDownContainer = InitMenuDropDownContainer();

			RootContainer = new JContainer(this);
			RootContainer.setBackgroundColor(Color.Transparent);

			JBorderLayout layout = new JBorderLayout(RootContainer);
			layout.TopSize = 0.05f;
			layout.BottemSize = 0.2f;
			layout.RightSize = 0.2f;
			RootContainer.Layout = layout;
			RootContainer.addElement(InfoContainer, JBorderLayout.TOP);
			RootContainer.addElement(BuildingFieldContainer, JBorderLayout.BOTTOM);
		}

		private JContainer InitInfoContainer()
		{
			JContainer container = new JContainer(this);
			container.setBackgroundColor(Color.Transparent);

			JGridLayout layout = new JGridLayout(container);
			layout.Rows = 7;

			container.Layout = layout;

			wave = new JLabel(this);
			wave.Text.CharacterSize = 12;
			wave.setTextString("WAVE X of X");
			container.addElement(wave);

			enemieRemaining = new JLabel(this);
			enemieRemaining.Text.CharacterSize = 12;
			enemieRemaining.setTextString("Enemies: X");
			container.addElement(enemieRemaining);

			health = new JLabel(this);
			health.Text.CharacterSize = 12;
			health.setTextString("Health: X");
			container.addElement(health);

			gold = new JLabel(this);
			gold.Text.CharacterSize = 12;
			gold.setTextString("Gold: X");
			container.addElement(gold);

			score = new JLabel(this);
			score.Text.CharacterSize = 12;
			score.setTextString("Score: X");
			container.addElement(score);

			container.addElement(null);

			JButton menu = new JButton(this);
			menu.Text.CharacterSize = 12;
			menu.setTextString("Menu");
			menu.OnExecute += OpenMenu;

			container.addElement(menu);

			return container;
		}
		
		

		private JContainer InitFieldContainer()
		{
			JContainer container = new JContainer(this);

			return container;
		}

		private JContainer InitGeneralFieldContainer()
		{
			JContainer container = new JContainer(this);
			container.Layout = new JLayout(container);

			JLabel label = new JLabel(this);
			label.setTextString("Field");
			container.addElement(label);

			return container;
		}

		private JContainer InitMineContainer()
		{
			JContainer container = new JContainer(this);
			container.Layout = new JLayout(container);

			JLabel label = new JLabel(this);
			label.setTextString("Mine\nProducest 5 Gold for every Second");
			label.Text.CharacterSize = 14;
			container.addElement(label);

			return container;
		}

		private JContainer InitTowerContainer()
		{
			JContainer container = new JContainer(this);
			container.Layout = new JLayout(container);

			JLabel label = new JLabel(this);
			label.setTextString("Tower");
			container.addElement(label);

			return container;
		}

		private JContainer InitResouceContainer()
		{
			JContainer container = new JContainer(this);
			JBorderLayout layout = new JBorderLayout(container);
			layout.LeftSize = 0.2f;
			container.Layout = layout;

			JLabel info = new JLabel(this);
			info.setTextString("Cost :20 Gold");
			info.Text.CharacterSize = 14;
			container.addElement(info, JBorderLayout.CENTER);

			JButton buildButton = new JButton(this);
			buildButton.setTextString("Build \nMine");
			buildButton.Text.CharacterSize = 14;
			buildButton.OnExecute += BuildMine;
			container.addElement(buildButton, JBorderLayout.LEFT);

			return container;
		}

		private JContainer InitNexusContainer()
		{
			JContainer container = new JContainer(this);
			container.Layout = new JLayout(container);

			JLabel label = new JLabel(this);
			label.setTextString("Nexus\nIf it gets Hit by an enemy you lose Health");
			label.Text.CharacterSize = 14;
			container.addElement(label);

			return container;
		}

		private JContainer InitBuildingFieldContainer()
		{
			JContainer container = new JContainer(this);
			container.setBackgroundColor(Color.Transparent);

			JBorderLayout layout = new JBorderLayout(container);
			layout.LeftSize = 0.2f;

			JContainer leftContainer = new JContainer(this);
			leftContainer.Layout = new JLayout(leftContainer);
			container.addElement(leftContainer, JBorderLayout.LEFT);

			LaserTower = new JCheckbox(this);
			LaserTower.setTextString("Laser");
			LaserTower.Text.CharacterSize = 14;
			LaserTower.OnPressed += UpdateStats;
			leftContainer.addElement(LaserTower);

			PlasmaTower = new JCheckbox(this);
			PlasmaTower.setTextString("Plasma");
			PlasmaTower.Text.CharacterSize = 14;
			PlasmaTower.OnPressed += UpdateStats;
			leftContainer.addElement(PlasmaTower);

			RailgunTower = new JCheckbox(this);
			RailgunTower.setTextString("Railgun");
			RailgunTower.Text.CharacterSize = 14;
			RailgunTower.OnPressed += UpdateStats;
			leftContainer.addElement(RailgunTower);

			JButton build = new JButton(this);
			build.setTextString("Build");
			build.Text.CharacterSize = 14;
			build.OnExecute += BuildTower;
			leftContainer.addElement(build);

			JCheckboxGroup towerGroup = new JCheckboxGroup();
			towerGroup.AddBox(LaserTower);
			towerGroup.AddBox(PlasmaTower);
			towerGroup.AddBox(RailgunTower);

			stats = new JLabel(this);
			//stats.setTextString("None");
			stats.Text.CharacterSize = 16;
			stats.setBackgroundColor(Color.Transparent);
			container.addElement(stats, JBorderLayout.CENTER);

			return container;
		}

		private JContainer InitMenuDropDownContainer()
		{
			JContainer container = new JContainer(this);
			container.Layout = new JLayout(container);
			container.Padding.Bottem = 0.85f;
			container.Padding.Left = 0.3f;

			JButton resume = new JButton(this);
			resume.setTextString("RESUME");
			resume.Text.CharacterSize = 12;
			resume.OnExecute += CloseMenu;
			container.addElement(resume);

			JButton exit = new JButton(this);
			exit.setTextString("EXIT");
			exit.Text.CharacterSize = 12;
			exit.OnExecute += ExitGame;
			container.addElement(exit);

			return container;
		}

		private void ChangeSelectedField(TDTile tile)
		{
			JContainer end = null;

			foreach (TDFieldActor actor in tile.FieldActors)
			{
				if (actor.ActorName == "TDMine")
				{
					end = MineContainer;
					break;
				}
				else if(actor.ActorName == "TDResource")
				{
					end = ResouceContainer;					
				}
				else if (actor.ActorName == "TDTower")
				{
					end = TowerContainer;
					return;
				}
				else if (actor.ActorName == "TDNexus")
				{
					end = NexusContainer;
				}
			}
			if (end == null)
			{
				RootContainer.addElement(BuildingFieldContainer, JBorderLayout.BOTTOM);
			}
			else
			{
				RootContainer.addElement(end, JBorderLayout.BOTTOM);
			}
		}

		//TODO
		private void UpdateFieldContainer()
		{
			if (SelectedField == GameModeHud.Player.CurrentlySelectedTile) return;
			SelectedField = GameModeHud.Player.CurrentlySelectedTile;

		}

		private void UpdateInfoContainer()
		{

			// GameInfoHud
			wave.setTextString("WAVE :"+ GameModeHud.CurrentWave+" of " + GameModeHud.WaveCount);
			enemieRemaining.setTextString("Enemie :"+GameModeHud.EnemiesLeftInCurrentWave);
			health.setTextString("Health :"+GameModeHud.PlayerHealth);
			gold.setTextString("Gold :" + GameModeHud.PlayerGold);
			score.setTextString("Score :" +GameModeHud.PlayerScore);

		}

		private void ExitGame()
		{
			Console.WriteLine(LevelRef);
		}

		private void OpenMenu()
		{
			RootContainer.addElement(MenuDropDownContainer, JBorderLayout.RIGHT);
		}

		private void CloseMenu()
		{
			RootContainer.addElement(null, JBorderLayout.RIGHT);
		}

		private void BuildTower()
		{

			Console.WriteLine("BuildTower");

			// Build Tower
			if (LaserTower.IsSelected)
			{
				BuildLaserTower();
			}
			else if (PlasmaTower.IsSelected)
			{
				BuildPlasmaTower();
			}
			else if (RailgunTower.IsSelected)
			{
				BuildRailgunTower();
			}
		}

		private void BuildLaserTower()
		{
			Console.WriteLine("BuildLaserTower");
			TDLaserTower actor = LevelRef.SpawnActor<TDLaserTower>();

			/*
			TDLaserWeaponComponent gun = new TDLaserWeaponComponent(new Sprite(LevelRef.EngineReference.AssetManager.LoadTexture("TowerGunT3")));
			var attackArea = LevelRef.PhysicsEngine.ConstructCircleOverlapComponent(actor, true, new TVector2f(), 0, new TVector2f(1.0f), 1.0f, gun.WeaponRange, VelcroPhysics.Dynamics.BodyType.Static);
			var sprite = new SpriteComponent(new Sprite(LevelRef.EngineReference.AssetManager.LoadTexture("TowerBase")));
			*/

			actor.TilePosition = GameModeHud.Player.CurrentlySelectedTileCoords;

			/*
			actor.AddComponent(sprite);
			actor.AddComponent(gun);
			actor.CollisionCallbacksEnabled = true;
			
			attackArea.Visible = true;

			attackArea.CollisionBody.OnCollision += gun.OnOverlapBegin;
			attackArea.CollisionBody.OnSeparation += gun.OnOverlapEnd;

			gun.ParentTower = actor;
			*/
		}

		private void BuildPlasmaTower()
		{
			Console.WriteLine("BuildPlasmaTower");
		}

		private void BuildRailgunTower()
		{
			Console.WriteLine("BuildRailgunTower");
		}

		private void BuildMine()
		{
			Console.WriteLine("BuildMine");
		}

		public void UpdateStats()
		{
			//TODO
			Console.WriteLine("UpdateStats");
			if (LaserTower.IsSelected)
			{
				stats.setBackgroundColor(Color.Black);
				stats.IsVisable = true;
				stats.setTextString("Cost : LOL\n" +
									"Weapon-Type : Beam\n" +
									"Damage : 420inS\n" +
									"Range : -88\n" +
									"Element : Weed\n");
				//BuildLaserTower();
			}
			else if (PlasmaTower.IsSelected)
			{
				stats.setBackgroundColor(Color.Black);
				stats.IsVisable = true;
				stats.setTextString("Cost : LOL More\n" +
									"Weapon-Type : Beam\n" +
									"Damage : 420inS\n" +
									"Range : -88\n" +
									"Element : Weed\n");
				//BuildPlasmaTower();
			}
			else if (RailgunTower.IsSelected)
			{
				stats.setBackgroundColor(Color.Black);
				stats.IsVisable = true;
				stats.setTextString("Cost : LOL More More\n" +
									"Weapon-Type : Beam\n" +
									"Damage : 420inS\n" +
									"Range : -88\n" +
									"Element : Weed\n");
				//BuildRailgunTower();
			}
			else
			{
				stats.setBackgroundColor(Color.Transparent);
				stats.IsVisable = false;
			}
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (LevelRef.GameMode == null) return;
			if (GameModeHud == null) GameModeHud = (TDGameMode)LevelRef.GameMode;

			UpdateFieldContainer();
			UpdateInfoContainer();
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{	
			base.Draw(target, states);
		}
	}
}
