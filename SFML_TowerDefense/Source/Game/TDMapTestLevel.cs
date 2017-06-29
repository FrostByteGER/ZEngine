using SFML.Graphics;
using SFML_TowerDefense.Source.Game.Player;

namespace SFML_TowerDefense.Source.Game
{
	public class TDMapTestLevel : TDLevel
	{
		protected override void InitLevel()
		{
			base.InitLevel();
			Map = new TDMap("test" ,this);

			var pc = new TDPlayerController();
			RegisterActor(Map);
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