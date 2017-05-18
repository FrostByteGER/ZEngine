using System;

namespace SFML_Engine.Engine.Events
{
	public class SwitchLevelParams : EngineEventParams
	{

		public Level NewLevel { get; set; }

		public SwitchLevelParams(object instigator, Level newLevel) : base(instigator)
		{
			NewLevel = newLevel;
		}
	}
}