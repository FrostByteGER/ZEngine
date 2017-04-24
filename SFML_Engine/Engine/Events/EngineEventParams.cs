using System;

namespace SFML_Engine.Engine.Events
{
	public class EngineEventParams
	{
		public object Instigator { get; }

		public EngineEventParams(object instigator)
		{
			Instigator = instigator ?? throw new ArgumentNullException(nameof(instigator));
		}
	}
}