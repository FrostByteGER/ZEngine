using System;

namespace ZEngine.Engine.Events
{

	public abstract class EngineEvent
	{
		public uint EventID { get; internal set; } = 0;

		/// <summary>
		/// Allows to Revoke an previously registered event so it doesn't get executed.
		/// </summary>
		public bool Revoked { get; set; } = false;
		public abstract void ExecuteEvent();

		protected bool Equals(EngineEvent other)
		{
			return EventID == other.EventID;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((EngineEvent) obj);
		}

		public override int GetHashCode()
		{
			return (int) EventID;
		}

		public static bool operator ==(EngineEvent left, EngineEvent right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(EngineEvent left, EngineEvent right)
		{
			return !Equals(left, right);
		}
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