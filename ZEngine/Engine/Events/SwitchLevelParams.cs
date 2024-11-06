using ZEngine.Engine.Game.Level;

namespace ZEngine.Engine.Events
{
	public class SwitchLevelParams : EngineEventParams
	{

		public Level_OLD NewLevelOld { get; set; }
		public bool DestroyPrevious { get; set; } = true;

		public SwitchLevelParams(object instigator, Level_OLD newLevelOld, bool destroyPrevious) : base(instigator)
		{
			NewLevelOld = newLevelOld;
			DestroyPrevious = destroyPrevious;
		}
	}
}