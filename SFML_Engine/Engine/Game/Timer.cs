namespace SFML_Engine.Engine.Game
{
	public class Timer
	{

		public uint TimerID { get; set; }
		public bool AutoReset { get; set; } = false;

		public bool Enabled { get; set; } = false;

		public bool CanActivateEvents { get; set; } = true;

		/// <summary>
		/// Time from which the timer counts down in Seconds.
		/// </summary>
		public float Interval { get; set; }

		/// <summary>
		/// Remaining Time in Seconds.
		/// </summary>
		public float Remaining { get; internal set; }

		/// <summary>
		/// Elapsed Time in Seconds.
		/// </summary>
		public float Elapsed => Interval - Remaining;

		public delegate void TimerHandler();

		public event TimerHandler Executed;


		public void StartTimer()
		{
			if (!Enabled)
			{
				Remaining = Interval;
			}
			Enabled = true;

		}

		public void StopTimer()
		{
			Enabled = false;
		}

		internal void Execute()
		{
			Executed?.Invoke();
		}
	}


}