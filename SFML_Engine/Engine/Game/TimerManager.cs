using System.Collections.Generic;

namespace SFML_Engine.Engine.Game
{
	public class TimerManager : ITickable
	{

		private List<Timer> Timers { get; set; } = new List<Timer>();
		internal uint TimerIDCounter { get; set; } = 0;

		public bool CanTick { get; set; } = true;


		public void RemoveTimer(int index)
		{
			Timers.RemoveAt(index);
		}

		public void RemoveTimer(Timer t)
		{
			Timers.Remove(t);
		}

		public Timer GetTimer(int index)
		{
			return Timers[index];
		}

		public T GetTimer<T>(int index) where T : Timer
		{
			return Timers[index] as T;
		} 

		public void AddTimer(Timer t)
		{
			if (Timers.Find(x => x.TimerID == t.TimerID) != null) return;
			Timers.Add(t);
			t.TimerID = ++TimerIDCounter;
		}

		public void Tick(float deltaTime)
		{
			foreach (var timer in Timers)
			{
				if (!timer.Enabled) continue;
				timer.Remaining -= deltaTime;
				if (timer.Remaining > 0.0f) continue;

				timer.Remaining = 0.0f;
				if (timer.CanActivateEvents)
				{
					timer.Execute();
				}
				timer.StopTimer();
				if (timer.AutoReset)
				{
					timer.StartTimer();
				}
				else
				{
					Timers.Remove(timer);
				}
			}
		}


	}
}