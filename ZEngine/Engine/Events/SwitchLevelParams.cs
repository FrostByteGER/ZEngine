using ZEngine.Engine.Game;
using ZEngine.Engine.Game.Level;

namespace ZEngine.Engine.Events
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