using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.Events
{
	public class SwitchLevelParams : EngineEventParams
	{

		public Level NewLevel { get; set; }
		public bool DestroyPrevious { get; set; } = true;

		public SwitchLevelParams(object instigator, Level newLevel, bool destroyPrevious) : base(instigator)
		{
			NewLevel = newLevel;
			DestroyPrevious = destroyPrevious;
		}
	}
}