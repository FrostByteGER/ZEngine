using System;
using System.Numerics;

namespace ZEngine.Engine.Utility
{
	public static class EngineMath
    {
        public static float Epsilon { get; } = 0.00001f;
		public static Random EngineRandom { get; set; } = new();


		public static Vector2 VInterpTo(Vector2 currentPosition, Vector2 targetPosition, float deltaTime, float interpSpeed)
		{
			if (interpSpeed <= 0.0f) return targetPosition;

			var distance = targetPosition - currentPosition;
			
			if (distance.LengthSquared() < Epsilon) return targetPosition;
			
			var deltaMove = distance * Clamp(deltaTime * interpSpeed, 0.0f, 1.0f);
			return currentPosition + deltaMove;
		}

		public static Vector2 VInterpToConstant(Vector2 currentPosition, Vector2 targetPosition, float deltaTime, float interpSpeed)
		{
			var delta = targetPosition - currentPosition;
			var deltaM = delta.Length();
			var maxStep = interpSpeed * deltaTime;

			if (deltaM > maxStep)
			{
				if (maxStep > 0.0f)
				{
					var deltaN = delta / deltaM;
					return currentPosition + deltaN * maxStep;
				}
				return currentPosition;
			}

			return targetPosition;
		}

		/// <summary>
		/// TODO: Check if Inclusive bounds.
		/// Clamps the given value between min and max.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="val"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
		{
			if (val.CompareTo(min) < 0) return min;
			if (val.CompareTo(max) > 0) return max;
			return val;
		}

		/// <summary>
		/// Converts a short to a binary string.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ShortToBinary(short value)
		{
			return Convert.ToString(value, 2).PadLeft(16, '0');
		}

		/// <summary>
		/// Converts the given rotation angle from degrees to radians.
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static float DegreesToRadians(float angle)
		{
			return (float)Math.PI * angle / 180.0f;
		}

		/// <summary>
		/// Converts the given rotation vector from degrees to radians.
		/// </summary>
		/// <param name="degAngles"></param>
		/// <returns></returns>
		public static Vector2 DegreesToRadians(Vector2 degAngles)
		{
			var x = (float)Math.PI * degAngles.X / 180.0f;
			var y = (float)Math.PI * degAngles.Y / 180.0f;
			return new Vector2(x, y);
		}

        /// <summary>
		/// Converts the given rotation angle from radians to degrees.
		/// </summary>
		/// <param name="radAngle"></param>
		/// <returns></returns>
		public static float RadiansToDegrees(float radAngle)
		{
			return (float) (radAngle * (180.0 / Math.PI));
		}

		/// <summary>
		/// Converts the given rotation vector from radians to degrees.
		/// </summary>
		/// <param name="radAngles"></param>
		/// <returns></returns>
		public static Vector2 RadiansToDegrees(Vector2 radAngles)
		{
			var x = (float) (radAngles.X * (180.0 / Math.PI));
			var y = (float) (radAngles.Y * (180.0 / Math.PI));
			return new Vector2(x, y);
		}

        /// <summary>
		/// Converts the given X and Y rotation axis from radians to degrees as a float Vector2.
		/// </summary>
		/// <param name="radX"></param>
		/// <param name="radY"></param>
		/// <returns></returns>
		public static Vector2 RadiansToDegrees(float radX, float radY)
		{
			var x = (float)(radX * (180.0 / Math.PI));
			var y = (float)(radY * (180.0 / Math.PI));
			return new Vector2(x, y);
		}
	}
}
 