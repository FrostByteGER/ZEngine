﻿using SFML.Graphics;
using SFML_Roguelike.Source.Game.Core;

namespace SFML_Roguelike.Source.Game.TileMap
{
	public class TDMapTestLevel : TDLevel
	{

		protected override void InitLevel()
		{
			base.InitLevel();
		}

		protected override void LevelTick(float deltaTime)
		{
			base.LevelTick(deltaTime);

		}

		protected override void LevelDraw(ref RenderWindow renderWindow)
		{ 
			base.LevelDraw(ref renderWindow);

		}

		public override void OnLevelLoad()
		{
			base.OnLevelLoad();
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}