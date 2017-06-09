using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;
using SFML_SpaceSEM.IO;

namespace SFML_SpaceSEM.Game
{
	public class SpaceLevel : Level
	{
		public SpaceLevel()
		{
			PhysicsEngine.Gravity = new TVector2f();
		}


		public static SpaceLevelDataWrapper LoadSpaceLevel(string levelName)
		{
			return LoadSpaceLevel(levelName, true);
		}

		public static SpaceLevelDataWrapper LoadSpaceLevel(string levelName, bool destroyPrevious)
		{
			if (string.IsNullOrWhiteSpace(levelName)) return null;

			var wrapperData = JSONManager.LoadObject<SpaceLevelDataWrapper>(AssetManager.LevelsPath + levelName);

			//return EngineReference.LoadLevel(level, destroyPrevious);
			return wrapperData;
		}

		public static void SaveSpaceLevel(string levelName, SpaceLevelDataWrapper data)
		{
			JSONManager.SaveObject(AssetManager.LevelsPath + levelName, data);
		}
	}
}