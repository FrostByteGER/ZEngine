using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Game;

namespace SFML_SpaceSEM.Game
{
	public class SpaceGameMode : GameMode
	{
		private uint _enemiesRemaining;

		public SpaceGameLevel GameLevel { get; set; }

		public uint EnemiesRemaining
		{
			get => _enemiesRemaining;
			set
			{
				_enemiesRemaining = value;
				if (value == 0) EnterNextLevel();
			}
		}

		public SpaceGameMode()
		{
		}


		private void EnterNextLevel()
		{
			var engineRef = GameLevel.EngineReference;
			if (GameLevel.SpaceLevelID == 4)
				engineRef.RegisterEvent(
					new SwitchLevelEvent<SwitchLevelParams>(new SwitchLevelParams(this, engineRef.LevelStack[0], true)));
			var newLevel = new SpaceGameLevel();
			newLevel.SpaceLevelID = ++GameLevel.SpaceLevelID;
			engineRef.RegisterEvent(new SwitchLevelEvent<SwitchLevelParams>(
				new SwitchLevelParams(this, newLevel, true)));
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			GameLevel = LevelReference as SpaceGameLevel;
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameResume()
		{
			base.OnGameResume();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}