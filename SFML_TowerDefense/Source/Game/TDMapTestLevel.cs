using SFML.Graphics;
using SFML.System;
using SFML_TowerDefense.Source.Game.Player;
using SFML_TowerDefense.Source.GUI;

namespace SFML_TowerDefense.Source.Game
{
	public class TDMapTestLevel : TDLevel
	{

		protected override void InitLevel()
		{
			base.InitLevel();
			Map = new TDMap("test" ,this);

			RegisterActor(Map);
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