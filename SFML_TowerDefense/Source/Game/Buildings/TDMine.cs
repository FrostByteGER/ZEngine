using System;
using System.Linq;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_TowerDefense.Source.Game.Player;
using SFML_TowerDefense.Source.Game.TileMap;
using SFML_TowerDefense.Source.GUI;

namespace SFML_TowerDefense.Source.Game.Buildings
{
	public class TDMine : TDBuilding
	{

		public float MineTime { get; set; } = 0;
		public float MineSpeed { get; set; } = 5;

		public uint MineAmount { get; set; } = 99;

		public TDResource ResourceField { get; set; }

		public TDPlayerController Owner { get; set; }
		public TDMineState MineState { get; set; } = TDMineState.Mining;
		
		public Text PopupText { get; set; }

		public TDMine(Level level) : base(level)
		{
			var mineSprite = new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("OreRefinery")));
			SetRootComponent(mineSprite);
			Origin = mineSprite.Origin;
			PopupText = new Text("0", LevelReference.EngineReference.AssetManager.LoadFont("MainGameFont"), 13);
		}

		public void MineResource()
		{
			if (ResourceField == null || Owner == null) return;
			if (ResourceField.ResourceAmount > 0)
			{
				Owner.Gold += ResourceField.Mine(MineAmount);
				AddComponent(new TDPopupTextComponent(PopupText));
			}
			else
			{
				MineState = TDMineState.Depleted;
				CanTick = false; // Disable ticking, we don't need it anymore!
			}
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			Owner = LevelReference.FindPlayer<TDPlayerController>(0);
			ResourceField = TDLevelRef.GetTileByTileCoords(TilePosition).FieldActors.OfType<TDResource>().FirstOrDefault();
			if (ResourceField == null) Console.WriteLine(GenerateFullName() + " at " + TilePosition + " has no valid Resource Field!");
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (MineTime <= 0)
			{
				MineResource();
				MineTime += MineSpeed;
			}
			MineTime -= deltaTime;
		}
	}

	public enum TDMineState
	{
		Mining,
		Depleted
	}
}