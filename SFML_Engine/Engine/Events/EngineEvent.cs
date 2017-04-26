using System;

namespace SFML_Engine.Engine.Events
{

	public abstract class EngineEvent
	{
		/// <summary>
		/// Allows to Revoke an previously registered event so it doesn't get executed.
		/// </summary>
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