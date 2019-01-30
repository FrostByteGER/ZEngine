using Exofinity.Source.Game.Core;
using SFML.Graphics;

namespace Exofinity.Source.Game.TileMap
{
	public class RMapTestLevel : RLevel
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

        protected override void OnGameStart()
		{
			base.OnGameStart();
		}

        protected override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}