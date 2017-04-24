using System;

namespace SFML_Engine.Engine.Events
{

	public abstract class EngineEvent
	{
		public bool Revoked { get; set; } = false;
		public abstract void ExecuteEvent();
	}

	public abstract class EngineEvent<T> : EngineEvent where T : EngineEventParams
	{
		public T Parameters { get; set; }

		protected EngineEvent(T parameters)
		{
			Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
		}


	}
}