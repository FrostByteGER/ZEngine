using System;
using System.Linq;
using Exofinity.Source.Game.Player;
using Exofinity.Source.Game.TileMap;
using Exofinity.Source.GUI;
using SFML.Graphics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Graphics;

namespace Exofinity.Source.Game.Buildings
{
	public class RMine : RBuilding
	{

		public float MineTime { get; set; } = 0;
		public float MineSpeed { get; set; } = 5;

		public uint MineAmount { get; set; } = 5;

		public RResource ResourceField { get; set; }

		public RPlayerController Owner { get; set; }
		public TDMineState MineState { get; set; } = TDMineState.Mining;
		

		public RMine(Level level) : base(level)
		{
			var mineSprite = new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("OreRefinery")));
			SetRootComponent(mineSprite);
			Origin = mineSprite.Origin;
		}

		public void MineResource()
		{
			if (ResourceField == null || Owner == null) return;
			if (ResourceField.ResourceAmount > 0)
			{
				var minedAmount = ResourceField.Mine(MineAmount);
				Owner.Gold += minedAmount;
				var popupText = new Text(minedAmount.ToString(), RGameModeRef.GameFont, 16);
				var textComp = new RPopupTextComponent(popupText);
				AddComponent(textComp);
				textComp.TargetPosition = textComp.LocalPosition - textComp.TargetPosition;
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
			Owner = LevelReference.FindPlayer<RPlayerController>(0);
			ResourceField = RLevelRef.GetTileByTileCoords(TilePosition).FieldActors.OfType<RResource>().FirstOrDefault();
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