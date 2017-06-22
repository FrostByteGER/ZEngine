using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.IO;

namespace SFML_TowerDefense.Source.Game
{
	public class TDMapTestLevel : TDLevel
	{
		protected override void InitLevel()
		{
			base.InitLevel();
			var tdMap = new TDMap(this);

			var pc = new TDPlayerController();
			RegisterActor(tdMap);
			RegisterPlayer(pc);
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