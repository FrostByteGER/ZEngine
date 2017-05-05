using System.Diagnostics;

namespace SFML_Engine.Engine.Utility
{

	/// <summary>
	/// Modified Copy from BulletSharp Examples: https://github.com/AndresTraks/BulletSharp/blob/master/demos/Generic/DemoFramework/Clock.cs
	/// 
	/// LICENSE:
	/// Copyright (c) 2009-2017 Kevin Kuegler
	/// <para>Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:</para>
	/// <para>The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.</para>
	/// <para>THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.</para>
	/// </summary>
	public class EngineClock
	{
		Stopwatch physicsTimer = new Stopwatch();
		Stopwatch updateTimer = new Stopwatch();
		Stopwatch renderTimer = new Stopwatch();
		Stopwatch frameTimer = new Stopwatch();

		public long FrameCount { get; private set; }

		public float PhysicsAverage
		{
			get
			{
				if (FrameCount == 0) return 0;
				return (((float)physicsTimer.ElapsedTicks / Stopwatch.Frequency) / FrameCount) * 1000.0f;
			}
		}

		public float UpdateAverage
		{
			get
			{
				if (FrameCount == 0) return 0;
				return (((float)updateTimer.ElapsedTicks / Stopwatch.Frequency) / FrameCount) * 1000.0f;
			}
		}

		public float RenderAverage
		{
			get
			{
				if (FrameCount == 0) return 0;
				return (((float)renderTimer.ElapsedTicks / Stopwatch.Frequency) / FrameCount) * 1000.0f;
			}
		}

		public void StartPhysics()
		{
			physicsTimer.Start();
		}

		public void StopPhysics()
		{
			physicsTimer.Stop();
		}

		public void StartUpdate()
		{
			updateTimer.Start();
		}

		public void StopUpdate()
		{
			updateTimer.Stop();
		}

		public void StartRender()
		{
			renderTimer.Start();
		}

		public void StopRender()
		{
			renderTimer.Stop();
		}

		public float GetFrameDelta()
		{
			FrameCount++;

			float delta = (float)frameTimer.ElapsedTicks / Stopwatch.Frequency;
			frameTimer.Restart();
			return delta;
		}

		public void Reset()
		{
			FrameCount = 0;
			physicsTimer.Reset();
			updateTimer.Reset();
			renderTimer.Reset();
		}
	}
}