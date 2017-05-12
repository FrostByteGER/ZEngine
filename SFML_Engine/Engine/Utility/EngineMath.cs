using SFML.System;
using System;

namespace SFML_Engine.Engine.Utility
{
	public static class EngineMath
	{

		public static Random EngineRandom { get; set; } = new Random();
		public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
		{
			if (val.CompareTo(min) < 0) return min;
			if (val.CompareTo(max) > 0) return max;
			return val;
		}

		public static Vector2f GetNorm(Vector2f vec)
		{
			return new Vector2f(vec.X / ((Math.Abs(vec.X) + Math.Abs(vec.Y))), vec.Y / ((Math.Abs(vec.X) + Math.Abs(vec.Y))));
		}

	}
}