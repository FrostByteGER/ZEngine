using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using SFML.System;

namespace SFML_Engine.Engine.Utility
{
	/// <summary>
	/// Wrapper class for SFML Vector2f. Allows conversion to Vector2f and Vector2 as well as many operations like +, -, *, /, ==, !=, >, <, >= and <=
	/// </summary>
	public class TVector2f
	{

		[JsonIgnore]
		public Vector2f Vec2f { get; set; }


		public float X
		{
			get => Vec2f.X;
			set => Vec2f = new Vector2f(value, Vec2f.Y);
		}

		public float Y
		{
			get => Vec2f.Y;
			set => Vec2f = new Vector2f(Vec2f.X, value);
		}

		public static TVector2f LocalUp { get; } = new TVector2f(0.0f, -1.0f);
		public static TVector2f LocalForward { get; } = new TVector2f(1.0f, 0.0f);
		public static float UnitX { get; } = 1.0f;
		public static float UnitY { get; } = 1.0f;
		public static float Epsilon { get; set; } = 0.00001f; //TODO: Implement

		[JsonIgnore]
		public float Length => (float) Math.Sqrt(Vec2f.X * Vec2f.X + Vec2f.Y * Vec2f.Y);

		[JsonIgnore]
		public float LengthSquared => Vec2f.X * Vec2f.X + Vec2f.Y * Vec2f.Y;

		public TVector2f()
		{
			Vec2f = new Vector2f(0.0f, 0.0f);
		}
		public TVector2f(float x)
		{
			Vec2f = new Vector2f(x, x);
		}

		public TVector2f(float x, float y)
		{
			Vec2f = new Vector2f(x,y);
		}

		public TVector2f(Vector2f vec2F)
		{
			Vec2f = vec2F;
		}

		public TVector2f(TVector2f vec)
		{
			Vec2f = vec.Vec2f;
		}

		public TVector2f(Vector2 vec)
		{
			Vec2f = new Vector2f(vec.X, vec.Y);
		}

		public TVector2f Up(float angleDegrees)
		{
			return Rotate(LocalUp, angleDegrees);
		}

		public TVector2f Forward(float angleDegrees)
		{
			return Rotate(LocalForward, angleDegrees);
		}

		public void Rotate(float angleDegrees)
		{
			var angleRadians = EngineMath.DegreesToRadians(angleDegrees);
			X = X * (float)Math.Cos(angleRadians) - Y * (float)Math.Sin(angleRadians);
			Y = X * (float)Math.Sin(angleRadians) + Y * (float)Math.Cos(angleRadians);
		}

		public TVector2f Rotate(TVector2f toRotate, float angleDegrees)
		{
			var rotatedVector = new TVector2f();
			var angleRadians = EngineMath.DegreesToRadians(angleDegrees);
			rotatedVector.X = toRotate.X * (float)Math.Cos(angleRadians) - toRotate.Y * (float)Math.Sin(angleRadians);
			rotatedVector.Y = toRotate.X * (float)Math.Sin(angleRadians) + toRotate.Y * (float)Math.Cos(angleRadians);
			return rotatedVector;
		}


		protected bool Equals(TVector2f other)
		{
			return Vec2f.Equals(other.Vec2f);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == this.GetType() && Equals((TVector2f)obj);
		}

		public override int GetHashCode()
		{
			return Vec2f.GetHashCode();
		}

		public static implicit operator Vector2f(TVector2f vec)
		{
			return vec.Vec2f;
		}

		public static implicit operator TVector2f(Vector2f vec)
		{
			return new TVector2f(vec);
		}

		public static implicit operator TVector2f(Vector2 vec)
		{
			return new TVector2f(vec);
		}

		public static implicit operator Vector2(TVector2f vec)
		{
			return new Vector2(vec.Vec2f.X, vec.Vec2f.Y);
		}

		public static TVector2f operator +(TVector2f a, float b)
		{
			return new TVector2f(a.Vec2f.X + b, a.Vec2f.Y + b);
		}

		public static TVector2f operator +(float a, TVector2f b)
		{
			return new TVector2f(a+ b.Vec2f.X, a + b.Vec2f.Y);
		}

		public static TVector2f operator +(TVector2f a, TVector2f b)
		{
			return new TVector2f(a.Vec2f + b.Vec2f);
		}

		public static TVector2f operator +(TVector2f a, Vector2f b)
		{
			return new TVector2f(a.Vec2f + b);
		}

		public static TVector2f operator +(Vector2f a, TVector2f b)
		{
			return new TVector2f(a + b.Vec2f);
		}

		public static TVector2f operator +(TVector2f a, Vector2 b)
		{
			return new TVector2f(a.X + b.X, a.Y + b.Y);
		}

		public static TVector2f operator +(Vector2 a, TVector2f b)
		{
			return new TVector2f(a.X + b.X, a.Y + b.Y);
		}

		public static TVector2f operator -(TVector2f a, float b)
		{
			return new TVector2f(a.Vec2f.X - b, a.Vec2f.Y - b);
		}

		public static TVector2f operator -(float a, TVector2f b)
		{
			return new TVector2f(a - b.Vec2f.X, a - b.Vec2f.Y);
		}

		public static TVector2f operator -(TVector2f a, TVector2f b)
		{
			return new TVector2f(a.Vec2f - b.Vec2f);
		}

		public static TVector2f operator -(TVector2f a, Vector2f b)
		{
			return new TVector2f(a.Vec2f - b);
		}

		public static TVector2f operator -(Vector2f a, TVector2f b)
		{
			return new TVector2f(a - b.Vec2f);
		}

		public static TVector2f operator -(TVector2f a, Vector2 b)
		{
			return new TVector2f(a.X - b.X, a.Y - b.Y);
		}

		public static TVector2f operator -(Vector2 a, TVector2f b)
		{
			return new TVector2f(a.X - b.X, a.Y - b.Y);
		}

		public static TVector2f operator /(TVector2f a, float b)
		{
			return new TVector2f(a.Vec2f.X / b, a.Vec2f.Y / b);
		}

		public static TVector2f operator /(float a, TVector2f b)
		{
			return new TVector2f(a / b.Vec2f.X, a / b.Vec2f.Y);
		}

		public static TVector2f operator /(TVector2f a, TVector2f b)
		{
			return new TVector2f(a.Vec2f.X / b.Vec2f.X, a.Vec2f.Y / b.Vec2f.Y);
		}

		public static TVector2f operator /(TVector2f a, Vector2f b)
		{
			return new TVector2f(a.X / b.X, a.Y / b.Y);
		}

		public static TVector2f operator /(Vector2f a, TVector2f b)
		{
			return new TVector2f(a.X / b.X, a.Y / b.Y);
		}

		public static TVector2f operator /(TVector2f a, Vector2 b)
		{
			return new TVector2f(a.X / b.X, a.Y / b.Y);
		}

		public static TVector2f operator /(Vector2 a, TVector2f b)
		{
			return new TVector2f(a.X / b.X, a.Y / b.Y);
		}

		public static TVector2f operator *(TVector2f a, float b)
		{
			return new TVector2f(a.Vec2f.X * b, a.Vec2f.Y * b);
		}

		public static TVector2f operator *(float a, TVector2f b)
		{
			return new TVector2f(a * b.Vec2f.X, a * b.Vec2f.Y);
		}

		public static TVector2f operator *(TVector2f a, TVector2f b)
		{
			return new TVector2f(a.X * b.X, a.Y * b.Y);
		}

		public static TVector2f operator *(TVector2f a, Vector2f b)
		{
			return new TVector2f(a.X * b.X, a.Y * b.Y);
		}

		public static TVector2f operator *(Vector2f a, TVector2f b)
		{
			return new TVector2f(a.X * b.X, a.Y * b.Y);
		}

		public static TVector2f operator *(TVector2f a, Vector2 b)
		{
			return new TVector2f(a.X * b.X, a.Y * b.Y);
		}

		public static TVector2f operator *(Vector2 a, TVector2f b)
		{
			return new TVector2f(a.X * b.X, a.Y * b.Y);
		}

		public static bool operator ==(float a, TVector2f b)
		{
			return a == b.X && a == b.Y;
		}

		public static bool operator ==(TVector2f a, float b)
		{
			return a.X == b && a.Y == b;
		}

		//TODO: Add Tolerance!
		public static bool operator ==(TVector2f a, TVector2f b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator ==(Vector2f a, TVector2f b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator ==(TVector2f a, Vector2f b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator ==(Vector2 a, TVector2f b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator ==(TVector2f a, Vector2 b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator !=(float a, TVector2f b)
		{
			return a != b.X || a != b.Y;
		}

		public static bool operator !=(TVector2f a, float b)
		{
			return a.X != b || a.Y != b;
		}

		//TODO: Add Tolerance!
		public static bool operator !=(TVector2f a, TVector2f b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static bool operator !=(Vector2f a, TVector2f b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static bool operator !=(TVector2f a, Vector2f b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static bool operator !=(Vector2 a, TVector2f b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static bool operator !=(TVector2f a, Vector2 b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static bool operator >(float a, TVector2f b)
		{
			return a > b.X && a > b.Y;
		}

		public static bool operator >(TVector2f a, float b)
		{
			return a.X > b && a.Y > b;
		}

		public static bool operator >(TVector2f a, TVector2f b)
		{
			return a.X > b.X && a.Y > b.Y;
		}

		public static bool operator >(Vector2f a, TVector2f b)
		{
			return a.X > b.X && a.Y > b.Y;
		}

		public static bool operator >(TVector2f a, Vector2f b)
		{
			return a.X > b.X && a.Y > b.Y;
		}

		public static bool operator >(Vector2 a, TVector2f b)
		{
			return a.X > b.X && a.Y > b.Y;
		}

		public static bool operator >(TVector2f a, Vector2 b)
		{
			return a.X > b.X && a.Y > b.Y;
		}

		public static bool operator <(float a, TVector2f b)
		{
			return a < b.X && a < b.Y;
		}

		public static bool operator <(TVector2f a, float b)
		{
			return a.X < b && a.Y < b;
		}

		public static bool operator <(TVector2f a, TVector2f b)
		{
			return a.X < b.X && a.Y < b.Y;
		}

		public static bool operator <(Vector2f a, TVector2f b)
		{
			return a.X < b.X && a.Y < b.Y;
		}

		public static bool operator <(TVector2f a, Vector2f b)
		{
			return a.X < b.X && a.Y < b.Y;
		}

		public static bool operator <(Vector2 a, TVector2f b)
		{
			return a.X < b.X && a.Y < b.Y;
		}

		public static bool operator <(TVector2f a, Vector2 b)
		{
			return a.X < b.X && a.Y < b.Y;
		}

		public static bool operator >=(float a, TVector2f b)
		{
			return a >= b.X && a >= b.Y;
		}

		public static bool operator >=(TVector2f a, float b)
		{
			return a.X >= b && a.Y >= b;
		}

		public static bool operator >=(TVector2f a, TVector2f b)
		{
			return a.X >= b.X && a.Y >= b.Y;
		}

		public static bool operator >=(Vector2f a, TVector2f b)
		{
			return a.X >= b.X && a.Y >= b.Y;
		}

		public static bool operator >=(TVector2f a, Vector2f b)
		{
			return a.X >= b.X && a.Y >= b.Y;
		}

		public static bool operator >=(Vector2 a, TVector2f b)
		{
			return a.X >= b.X && a.Y >= b.Y;
		}

		public static bool operator >=(TVector2f a, Vector2 b)
		{
			return a.X >= b.X && a.Y >= b.Y;
		}

		public static bool operator <=(float a, TVector2f b)
		{
			return a <= b.X && a <= b.Y;
		}

		public static bool operator <=(TVector2f a, float b)
		{
			return a.X <= b && a.Y <= b;
		}

		public static bool operator <=(TVector2f a, TVector2f b)
		{
			return a.X <= b.X && a.Y <= b.Y;
		}

		public static bool operator <=(Vector2f a, TVector2f b)
		{
			return a.X <= b.X && a.Y <= b.Y;
		}

		public static bool operator <=(TVector2f a, Vector2f b)
		{
			return a.X <= b.X && a.Y <= b.Y;
		}

		public static bool operator <=(Vector2 a, TVector2f b)
		{
			return a.X <= b.X && a.Y <= b.Y;
		}

		public static bool operator <=(TVector2f a, Vector2 b)
		{
			return a.X <= b.X && a.Y <= b.Y;
		}

		public override string ToString()
		{
			return "[X: " + X + " | Y: " + Y + "]";
		}
	}
}