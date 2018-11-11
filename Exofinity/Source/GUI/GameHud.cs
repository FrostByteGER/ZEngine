using ZEngine.Engine.JUI;
using System;
using Exofinity.Source.Game.Buildings;
using Exofinity.Source.Game.Buildings.Towers;
using Exofinity.Source.Game.Core;
using Exofinity.Source.Game.TileMap;
using SFML.Audio;
using SFML.Graphics;
using ZEngine.Engine.IO;
using SFML.System;

namespace Exofinity.Source.GUI
{
	public class GameHud : JGUI
	{
		public RTile _SelectedField;
		public RTile SelectedField {
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

		public JContainer LostContainer;
		public JContainer WinContainer;

		//Tower
		public JCheckbox LaserTower;
		public JCheckbox PlasmaTower;
		public JCheckbox RailgunTower;

		public JLabel stats;

		//public TDGameInfo GameInfoHud;
		public RGameMode GameModeRef;
		

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

			LostContainer = InitLostContainer();
			WinContainer = InitWinContainer();

			RootContainer = new JContainer(this);
			RootContainer.setBackgroundColor(Color.Transparent);

			JBorderLayout layout = new JBorderLayout(RootContainer);
			layout.TopSize = 0.05f;
			layout.BottemSize = 0.2f;
			layout.RightSize = 0.2f;
			RootContainer.Layout = layout;
			RootContainer.addElement(InfoContainer, JBorderLayout.TOP);
			RootContainer.addElement(BuildingFieldContainer, JBorderLayout.BOTTOM);
			//RootContainer.addElement(LostContainer, JBorderLayout.CENTER);
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

		private JContainer InitLostContainer()
		{
			JContainer rap = new JContainer(this);
			rap.setBackgroundColor(Color.Transparent);
			rap.Layout = new JLayout(rap);
			JDistanceContainer dis = new JDistanceContainer();
			dis.SetHorizontal(0.2f);
			dis.SetVertikal(0.35f);
			rap.Margin = dis;

			JContainer end = new JContainer(this);
			end.setBackgroundColor(Color.Transparent);
			rap.addElement(end);

			JBorderLayout layout = new JBorderLayout(end);
			layout.TopSize = 0.2f;
			end.Layout = layout;

			JLabel lable = new JLabel(this);
			lable.Text.CharacterSize = 16;
			lable.setTextString("Defeated!");
			end.addElement(lable, JBorderLayout.TOP);

			JContainer info = new JContainer(this);
			end.addElement(info, JBorderLayout.CENTER);

			JGridLayout infoLayout = new JGridLayout(info);
			infoLayout.Rows = 2;
			infoLayout.Columns = 2;
			info.Layout = infoLayout;

			JLabel score = new JLabel(this);
			score.Text.CharacterSize = 12;
			// GameModeHud.PlayerScore
			score.setTextString("Score : 000" );
			info.addElement(score);

			JButton retry = new JButton(this);
			retry.Text.CharacterSize = 12;
			retry.setTextString("Retry");
			retry.OnExecute += Retry;
			info.addElement(retry);

			JLabel highscore = new JLabel(this);
			highscore.Text.CharacterSize = 12;
			highscore.setTextString("Highscore : need Highscore");
			info.addElement(highscore);

			JButton exit = new JButton(this);
			exit.Text.CharacterSize = 12;
			exit.setTextString("Exit to Menu");
			exit.OnExecute += ExitGame;
			info.addElement(exit);

			return rap;
		}

		private JContainer InitWinContainer()
		{
			JContainer rap = new JContainer(this);
			rap.setBackgroundColor(Color.Transparent);
			rap.Layout = new JLayout(rap);
			JDistanceContainer dis = new JDistanceContainer();
			dis.SetHorizontal(0.2f);
			dis.SetVertikal(0.35f);
			rap.Margin = dis;

			JContainer end = new JContainer(this);
			end.setBackgroundColor(Color.Transparent);
			rap.addElement(end);

			JBorderLayout layout = new JBorderLayout(end);
			layout.TopSize = 0.2f;
			end.Layout = layout;

			JLabel lable = new JLabel(this);
			lable.Text.CharacterSize = 16;
			lable.setTextString("Won!");
			end.addElement(lable, JBorderLayout.TOP);

			JContainer info = new JContainer(this);
			end.addElement(info, JBorderLayout.CENTER);

			JGridLayout infoLayout = new JGridLayout(info);
			infoLayout.Rows = 2;
			infoLayout.Columns = 2;
			info.Layout = infoLayout;

			JLabel score = new JLabel(this);
			score.Text.CharacterSize = 12;
			// GameModeHud.PlayerScore
			score.setTextString("Score : 000");
			info.addElement(score);

			JButton next = new JButton(this);
			next.Text.CharacterSize = 12;
			next.setTextString("Next Mission");
			next.OnExecute += NextLevel;
			info.addElement(next);

			JLabel highscore = new JLabel(this);
			highscore.Text.CharacterSize = 12;
			highscore.setTextString("Highscore : need Highscore");
			info.addElement(highscore);

			JButton exit = new JButton(this);
			exit.Text.CharacterSize = 12;
			exit.setTextString("Exit to Menu");
			exit.OnExecute += ExitGame;
			info.addElement(exit);

			return rap;
		}

		private void ChangeSelectedField(RTile tile)
		{
			JContainer end = null;

			foreach (RFieldActor actor in tile.FieldActors)
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
			if (SelectedField == GameModeRef.Player.CurrentlySelectedTile) return;
			SelectedField = GameModeRef.Player.CurrentlySelectedTile;

		}

		private void UpdateInfoContainer()
		{

			// GameInfoHud
			wave.setTextString("WAVE :"+ GameModeRef.CurrentWave+" of " + GameModeRef.WaveCount);
			enemieRemaining.setTextString("Enemie :"+GameModeRef.EnemiesLeftInCurrentWave);
			health.setTextString("Health :"+GameModeRef.PlayerHealth);
			gold.setTextString("Gold :" + GameModeRef.PlayerGold);
			score.setTextString("Score :" +GameModeRef.PlayerScore);

		}

		private void ExitGame()
		{
			Console.WriteLine("Exit Gamelevel");
			LevelRef.EngineReference.LoadLevel(new MenuLevel());
			IsActive = false;
		}

		private void OpenMenu()
		{
			RootContainer.addElement(MenuDropDownContainer, JBorderLayout.RIGHT);
		}

		private void CloseMenu()
		{
			RootContainer.addElement(null, JBorderLayout.RIGHT);
		}

		private void Retry()
		{
			Console.WriteLine("Retry");
		}

		private void NextLevel()
		{
			Console.WriteLine("NextLevel");
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
			var laserTower = LevelRef.SpawnActor<RLaserTower>();
			laserTower.TilePosition = GameModeRef.Player.CurrentlySelectedTileCoords;
			if(GameModeRef.ConstructionComplete.Status != SoundStatus.Playing) GameModeRef.ConstructionComplete.Play();
		}

		private void BuildPlasmaTower()
		{
			Console.WriteLine("BuildPlasmaTower");
			var laserTower = LevelRef.SpawnActor<RPlasmaTower>();
			laserTower.TilePosition = GameModeRef.Player.CurrentlySelectedTileCoords;
			if (GameModeRef.ConstructionComplete.Status != SoundStatus.Playing) GameModeRef.ConstructionComplete.Play();
		}

		private void BuildRailgunTower()
		{
			Console.WriteLine("BuildRailgunTower");
			var railgunTower = LevelRef.SpawnActor<RRailgunTower>();
			railgunTower.TilePosition = GameModeRef.Player.CurrentlySelectedTileCoords;
			if (GameModeRef.ConstructionComplete.Status != SoundStatus.Playing) GameModeRef.ConstructionComplete.Play();
		}

		private void BuildMine()
		{
			Console.WriteLine("BuildMine");
			var mine = LevelRef.SpawnActor<RMine>();
			mine.TilePosition = GameModeRef.Player.CurrentlySelectedTileCoords;
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
			if (GameModeRef == null) GameModeRef = (RGameMode)LevelRef.GameMode;

			UpdateFieldContainer();
			UpdateInfoContainer();
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{	
			base.Draw(target, states);
		}
	}
}
