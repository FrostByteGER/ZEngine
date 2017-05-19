using System;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.Utility
{
	public static class EngineMath
	{

		public static Random EngineRandom { get; set; } = new Random();

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
		/// Converts the given rotation vector from degrees to radians as a BulletSharp float vector3.
		/// </summary>
		/// <param name="degAngles"></param>
		/// <returns></returns>
		public static Vector3 DegreesToRadians(Vector3 degAngles)
		{
			float x = (float)Math.PI * degAngles.X / 180.0f;
			float y = (float)Math.PI * degAngles.Y / 180.0f;
			float z = (float)Math.PI * degAngles.Z / 180.0f;
			return new Vector3(x, y, z);
		}

		/// <summary>
		/// Converts the given rotation vector from degrees to radians as a SFML float vector2.
		/// </summary>
		/// <param name="degAngles"></param>
		/// <returns></returns>
		public static Vector2f DegreesToRadians(Vector2f degAngles)
		{
			float x = (float)Math.PI * degAngles.X / 180.0f;
			float y = (float)Math.PI * degAngles.Y / 180.0f;
			return new Vector2f(x, y);
		}

		/// <summary>
		/// Converts the given rotation vector from degrees to radians as a float TVector2.
		/// </summary>
		/// <param name="degAngles"></param>
		/// <returns></returns>
		public static TVector2f DegreesToRadians(TVector2f degAngles)
		{
			float x = (float)Math.PI * degAngles.X / 180.0f;
			float y = (float)Math.PI * degAngles.Y / 180.0f;
			return new TVector2f(x, y);
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
		/// Converts the given rotation vector from radians to degrees as a BulletSharp float vector3.
		/// </summary>
		/// <param name="radAngles"></param>
		/// <returns></returns>
		public static Vector3 RadiansToDegrees(Vector3 radAngles)
		{
			float x = (float) (radAngles.X * (180.0 / Math.PI));
			float y = (float) (radAngles.Y * (180.0 / Math.PI));
			float z = (float) (radAngles.Z * (180.0 / Math.PI));
			return new Vector3(x, y, z);
		}

		/// <summary>
		/// Converts the given rotation vector from radians to degrees as a SFML float vector2.
		/// </summary>
		/// <param name="radAngles"></param>
		/// <returns></returns>
		public static Vector2f RadiansToDegrees(Vector2f radAngles)
		{
			float x = (float) (radAngles.X * (180.0 / Math.PI));
			float y = (float) (radAngles.Y * (180.0 / Math.PI));
			return new Vector2f(x, y);
		}

		/// <summary>
		/// Converts the given rotation vector from radians to degrees as a float TVector2.
		/// </summary>
		/// <param name="radAngles"></param>
		/// <returns></returns>
		public static TVector2f RadiansToDegrees(TVector2f radAngles)
		{
			float x = (float)(radAngles.X * (180.0 / Math.PI));
			float y = (float)(radAngles.Y * (180.0 / Math.PI));
			return new TVector2f(x, y);
		}

		/// <summary>
		/// Converts the given X, Y and Z rotation axis from radians to degrees as a BulletSharp float vector3.
		/// </summary>
		/// <param name="radX"></param>
		/// <param name="radY"></param>
		/// <param name="radZ"></param>
		/// <returns></returns>
		public static Vector3 RadiansToDegreesBt(float radX, float radY, float radZ)
		{
			float x = (float) (radX * (180.0 / Math.PI));
			float y = (float) (radY * (180.0 / Math.PI));
			float z = (float) (radZ * (180.0 / Math.PI));
			return new Vector3(x, y, z);
		}

		/// <summary>
		/// Converts the given X and Y rotation axis from radians to degrees as a SFML float vector2.
		/// </summary>
		/// <param name="radX"></param>
		/// <param name="radY"></param>
		/// <returns></returns>
		public static Vector2f RadiansToDegreesSf(float radX, float radY)
		{
			float x = (float) (radX * (180.0 / Math.PI));
			float y = (float) (radY * (180.0 / Math.PI));
			return new Vector2f(x, y);
		}

		/// <summary>
		/// Converts the given X and Y rotation axis from radians to degrees as a float TVector2.
		/// </summary>
		/// <param name="radX"></param>
		/// <param name="radY"></param>
		/// <returns></returns>
		public static TVector2f RadiansToDegrees(float radX, float radY)
		{
			float x = (float)(radX * (180.0 / Math.PI));
			float y = (float)(radY * (180.0 / Math.PI));
			return new TVector2f(x, y);
		}

		/// <summary>
		/// Converts a BulletSharp float vector to SFML float vector2.
		/// </summary>
		/// <param name="source">The vector to convert</param>
		/// <returns></returns>
		public static Vector2f Vec3ToVec2f(Vector3 source)
		{
			return new Vector2f(source.X, source.Y);
		}

		/// <summary>
		/// Converts a SFML float vector to BulletSharp float Vector3.
		/// </summary>
		/// <param name="source">The vector to convert</param>
		/// <returns></returns>
		public static Vector3 Vec2fToVec3(Vector2f source)
		{
			return new Vector3(source.X, source.Y, 0.0f);
		}

		/// <summary>
		/// Converts a BulletSharp Quaternion to Euler angles in degrees. Returns all 3 axis as a BulletSharp vector.
		/// </summary>
		/// <param name="quat">The quaternion to convert</param>
		public static Vector3 QuatToEulerDegreesBt(Quaternion quat)
		{
			double w = quat.W;
			double x = quat.X;
			double y = quat.Y;
			double z = quat.Z;
			double sqw = w * w;
			double sqx = x * x;
			double sqy = y * y;
			double sqz = z * z;
			float ez = (float) Math.Atan2(2.0 * (x * y + z * w), sqx - sqy - sqz + sqw);
			float ex = (float) Math.Atan2(2.0 * (y * z + x * w), -sqx - sqy + sqz + sqw);
			float ey = (float) Math.Asin(-2.0 * (x * z - y * w));
			return RadiansToDegreesBt(ex, ey, ez);
		}

		/// <summary>
		/// Converts a BulletSharp Quaternion to Euler angles in radians. Returns all 3 axis as a BulletSharp vector.
		/// </summary>
		/// <param name="quat">The quaternion to convert</param>
		public static Vector3 QuatToEulerRadiansBt(Quaternion quat)
		{
			double w = quat.W;
			double x = quat.X;
			double y = quat.Y;
			double z = quat.Z;
			double sqw = w * w;
			double sqx = x * x;
			double sqy = y * y;
			double sqz = z * z;
			float ez = (float) Math.Atan2(2.0 * (x * y + z * w), sqx - sqy - sqz + sqw);
			float ex = (float) Math.Atan2(2.0 * (y * z + x * w), -sqx - sqy + sqz + sqw);
			float ey = (float) Math.Asin(-2.0 * (x * z - y * w));
			return new Vector3(ex, ey, ez);
		}

		/// <summary>
		/// Converts a BulletSharp Quaternion to Euler angles in degrees. Only returns the Z axis as in SFML theres only one rotation axis.
		/// </summary>
		/// <param name="quat">The quaternion to convert</param>
		public static float QuatToEulerDegrees(Quaternion quat)
		{
			double w = quat.W;
			double x = quat.X;
			double y = quat.Y;
			double z = quat.Z;
			double sqw = w * w;
			double sqx = x * x;
			double sqy = y * y;
			double sqz = z * z;
			float ez = (float) Math.Atan2(2.0 * (x * y + z * w), sqx - sqy - sqz + sqw);
			return RadiansToDegrees(ez);
		}

		/// <summary>
		/// Converts a BulletSharp Quaternion to Euler angles in radians. Only returns the Z axis as in SFML theres only one rotation axis.
		/// </summary>
		/// <param name="quat">The quaternion to convert</param>
		/// <returns></returns>
		public static float QuatToEulerRadians(Quaternion quat)
		{
			double w = quat.W;
			double x = quat.X;
			double y = quat.Y;
			double z = quat.Z;
			double sqw = w * w;
			double sqx = x * x;
			double sqy = y * y;
			double sqz = z * z;
			float ez = (float) Math.Atan2(2.0 * (x * y + z * w), sqx - sqy - sqz + sqw);
			return ez;
		}

		/// <summary>
		/// Generates a transform matrix from a SFML Vector2f position, scale and a angle in degrees.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="angle"></param>
		/// <param name="scale"></param>
		/// <returns></returns>
		public static Matrix TransformFromPosRotScaleBt(Vector2f position, float angle, Vector2f scale)
		{
			return Matrix.Translation(position.X, position.Y, 0.0f) * Matrix.RotationZ(DegreesToRadians(angle)) *
			       Matrix.Scaling(scale.X, scale.Y, 1.0f);
		}

		/// <summary>
		/// Generates a SFML transform matrix from a SFML Vector2f position, scale and a angle in degrees.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="angle"></param>
		/// <param name="scale"></param>
		/// <returns></returns>
		public static Transform TransformFromPosRotScale(Vector2f position, float angle, Vector2f scale)
		{
			var t = new Transform();
			t.Translate(position);
			t.Rotate(angle);
			t.Scale(scale);
			return t;
		}

		/// <summary>
		/// Generates a SFML transform matrix from a TVector2f position, scale and a angle in degrees.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="angle"></param>
		/// <param name="scale"></param>
		/// <returns></returns>
		public static Transform TransformFromPosRotScale(TVector2f position, float angle, TVector2f scale)
		{
			var t = new Transform();
			t.Translate(position);
			t.Rotate(angle);
			t.Scale(scale);
			return t;
		}

		/// <summary>
		/// Generates a SFML transformable from a SFML Vector2f position, scale and a angle in degrees.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="angle"></param>
		/// <param name="scale"></param>
		/// <returns></returns>
		public static Transformable TransformableFromPosRotScale(Vector2f position, float angle, Vector2f scale)
		{
			var t = new Transformable
			{
				Position = position,
				Rotation = angle,
				Scale = scale
			};
			return t;
		}

		/// <summary>
		/// Generates a SFML transformable from a TVector2f position, scale and a angle in degrees.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="angle"></param>
		/// <param name="scale"></param>
		/// <returns></returns>
		public static Transformable TransformableFromPosRotScale(TVector2f position, float angle, TVector2f scale)
		{
			var t = new Transformable
			{
				Position = position,
				Rotation = angle,
				Scale = scale
			};
			return t;
		}
	}
}