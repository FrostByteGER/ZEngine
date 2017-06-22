using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using SFML.System;

namespace SFML_Engine.Engine.Utility
{
	/// <summary>
	/// Wrapper class for SFML Vector2i. Allows conversion to Vector2i and Vector2 as well as many operations like +, -, *, /, ==, !=, >, <, >= and <=
	/// </summary>
	public class TVector2i
	{

		[JsonIgnore]
		public Vector2i Vec2i { get; set; }


		public int X
		{
			get => Vec2i.X;
			set => Vec2i = new Vector2i(value, Vec2i.Y);
		}

		public int Y
		{
			get => Vec2i.Y;
			set => Vec2i = new Vector2i(Vec2i.X, value);
		}

		public static TVector2i LocalUp { get; } = new TVector2i(0, -1);
		public static TVector2i LocalForward { get; } = new TVector2i(1, 0);
		public static int UnitX { get; } = 1;
		public static int UnitY { get; } = 1;

		[JsonIgnore]
		public float Length => (float)Math.Sqrt(Vec2i.X * Vec2i.X + Vec2i.Y * Vec2i.Y);

		[JsonIgnore]
		public float LengthSquared => Vec2i.X * Vec2i.X + Vec2i.Y * Vec2i.Y;

		public TVector2i()
		{
			Vec2i = new Vector2i(0, 0);
		}
		public TVector2i(int x)
		{
			Vec2i = new Vector2i(x, x);
		}

		public TVector2i(int x, int y)
		{
			Vec2i = new Vector2i(x, y);
		}

		public TVector2i(Vector2i vec2I)
		{
			Vec2i = vec2I;
		}

		public TVector2i(TVector2i vec)
		{
			Vec2i = vec.Vec2i;
		}

		public TVector2i(Vector2 vec)
		{
			Vec2i = new Vector2i((int) vec.X, (int) vec.Y);
		}


		protected bool Equals(TVector2i other)
		{
			return Vec2i.Equals(other.Vec2i);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == this.GetType() && Equals((TVector2i)obj);
		}

		public override int GetHashCode()
		{
			return Vec2i.GetHashCode();
		}

		public static implicit operator Vector2i(TVector2i vec)
		{
			return vec.Vec2i;
		}

		public static implicit operator TVector2i(Vector2i vec)
		{
			return new TVector2i(vec);
		}

		public static implicit operator TVector2i(Vector2 vec)
		{
			return new TVector2i(vec);
		}

		public static implicit operator Vector2(TVector2i vec)
		{
			return new Vector2(vec.Vec2i.X, vec.Vec2i.Y);
		}

		public static TVector2i operator +(TVector2i a, int b)
		{
			return new TVector2i(a.Vec2i.X + b, a.Vec2i.Y + b);
		}

		public static TVector2i operator +(int a, TVector2i b)
		{
			return new TVector2i(a + b.Vec2i.X, a + b.Vec2i.Y);
		}

		public static TVector2i operator +(TVector2i a, TVector2i b)
		{
			return new TVector2i(a.Vec2i + b.Vec2i);
		}

		public static TVector2i operator +(TVector2i a, Vector2i b)
		{
			return new TVector2i(a.Vec2i + b);
		}

		public static TVector2i operator +(Vector2i a, TVector2i b)
		{
			return new TVector2i(a + b.Vec2i);
		}

		public static TVector2i operator +(TVector2i a, Vector2 b)
		{
			return new TVector2i((int) (a.X + b.X), (int) (a.Y + b.Y));
		}

		public static TVector2i operator +(Vector2 a, TVector2i b)
		{
			return new TVector2i((int) (a.X + b.X), (int) (a.Y + b.Y));
		}

		public static TVector2i operator -(TVector2i a, int b)
		{
			return new TVector2i(a.Vec2i.X - b, a.Vec2i.Y - b);
		}

		public static TVector2i operator -(int a, TVector2i b)
		{
			return new TVector2i(a - b.Vec2i.X, a - b.Vec2i.Y);
		}

		public static TVector2i operator -(TVector2i a, TVector2i b)
		{
			return new TVector2i(a.Vec2i - b.Vec2i);
		}

		public static TVector2i operator -(TVector2i a, Vector2i b)
		{
			return new TVector2i(a.Vec2i - b);
		}

		public static TVector2i operator -(Vector2i a, TVector2i b)
		{
			return new TVector2i(a - b.Vec2i);
		}

		public static TVector2i operator -(TVector2i a, Vector2 b)
		{
			return new TVector2i((int) (a.X - b.X), (int) (a.Y - b.Y));
		}

		public static TVector2i operator -(Vector2 a, TVector2i b)
		{
			return new TVector2i((int) (a.X - b.X), (int) (a.Y - b.Y));
		}

		public static TVector2i operator /(TVector2i a, int b)
		{
			return new TVector2i(a.Vec2i.X / b, a.Vec2i.Y / b);
		}

		public static TVector2i operator /(int a, TVector2i b)
		{
			return new TVector2i(a / b.Vec2i.X, a / b.Vec2i.Y);
		}

		public static TVector2i operator /(TVector2i a, TVector2i b)
		{
			return new TVector2i(a.Vec2i.X / b.Vec2i.X, a.Vec2i.Y / b.Vec2i.Y);
		}

		public static TVector2i operator /(TVector2i a, Vector2i b)
		{
			return new TVector2i(a.X / b.X, a.Y / b.Y);
		}

		public static TVector2i operator /(Vector2i a, TVector2i b)
		{
			return new TVector2i(a.X / b.X, a.Y / b.Y);
		}

		public static TVector2i operator /(TVector2i a, Vector2 b)
		{
			return new TVector2i((int) (a.X / b.X), (int) (a.Y / b.Y));
		}

		public static TVector2i operator /(Vector2 a, TVector2i b)
		{
			return new TVector2i((int) (a.X / b.X), (int) (a.Y / b.Y));
		}

		public static TVector2i operator *(TVector2i a, int b)
		{
			return new TVector2i(a.Vec2i.X * b, a.Vec2i.Y * b);
		}

		public static TVector2i operator *(int a, TVector2i b)
		{
			return new TVector2i(a * b.Vec2i.X, a * b.Vec2i.Y);
		}

		public static TVector2i operator *(TVector2i a, TVector2i b)
		{
			return new TVector2i(a.X * b.X, a.Y * b.Y);
		}

		public static TVector2i operator *(TVector2i a, Vector2i b)
		{
			return new TVector2i(a.X * b.X, a.Y * b.Y);
		}

		public static TVector2i operator *(Vector2i a, TVector2i b)
		{
			return new TVector2i(a.X * b.X, a.Y * b.Y);
		}

		public static TVector2i operator *(TVector2i a, Vector2 b)
		{
			return new TVector2i((int) (a.X * b.X), (int) (a.Y * b.Y));
		}

		public static TVector2i operator *(Vector2 a, TVector2i b)
		{
			return new TVector2i((int) (a.X * b.X), (int) (a.Y * b.Y));
		}

		public static bool operator ==(int a, TVector2i b)
		{
			return a == b.X && a == b.Y;
		}

		public static bool operator ==(TVector2i a, int b)
		{
			return a.X == b && a.Y == b;
		}

		//TODO: Add Tolerance!
		public static bool operator ==(TVector2i a, TVector2i b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator ==(Vector2i a, TVector2i b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator ==(TVector2i a, Vector2i b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator ==(Vector2 a, TVector2i b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator ==(TVector2i a, Vector2 b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator !=(int a, TVector2i b)
		{
			return a != b.X || a != b.Y;
		}

		public static bool operator !=(TVector2i a, int b)
		{
			return a.X != b || a.Y != b;
		}

		public static bool operator !=(TVector2i a, TVector2i b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static bool operator !=(Vector2i a, TVector2i b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static bool operator !=(TVector2i a, Vector2i b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static bool operator !=(Vector2 a, TVector2i b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static bool operator !=(TVector2i a, Vector2 b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		public static bool operator >(int a, TVector2i b)
		{
			return a > b.X && a > b.Y;
		}

		public static bool operator >(TVector2i a, int b)
		{
			return a.X > b && a.Y > b;
		}

		public static bool operator >(TVector2i a, TVector2i b)
		{
			return a.X > b.X && a.Y > b.Y;
		}

		public static bool operator >(Vector2i a, TVector2i b)
		{
			return a.X > b.X && a.Y > b.Y;
		}

		public static bool operator >(TVector2i a, Vector2i b)
		{
			return a.X > b.X && a.Y > b.Y;
		}

		public static bool operator >(Vector2 a, TVector2i b)
		{
			return a.X > b.X && a.Y > b.Y;
		}

		public static bool operator >(TVector2i a, Vector2 b)
		{
			return a.X > b.X && a.Y > b.Y;
		}

		public static bool operator <(int a, TVector2i b)
		{
			return a < b.X && a < b.Y;
		}

		public static bool operator <(TVector2i a, int b)
		{
			return a.X < b && a.Y < b;
		}

		public static bool operator <(TVector2i a, TVector2i b)
		{
			return a.X < b.X && a.Y < b.Y;
		}

		public static bool operator <(Vector2i a, TVector2i b)
		{
			return a.X < b.X && a.Y < b.Y;
		}

		public static bool operator <(TVector2i a, Vector2i b)
		{
			return a.X < b.X && a.Y < b.Y;
		}

		public static bool operator <(Vector2 a, TVector2i b)
		{
			return a.X < b.X && a.Y < b.Y;
		}

		public static bool operator <(TVector2i a, Vector2 b)
		{
			return a.X < b.X && a.Y < b.Y;
		}

		public static bool operator >=(int a, TVector2i b)
		{
			return a >= b.X && a >= b.Y;
		}

		public static bool operator >=(TVector2i a, int b)
		{
			return a.X >= b && a.Y >= b;
		}

		public static bool operator >=(TVector2i a, TVector2i b)
		{
			return a.X >= b.X && a.Y >= b.Y;
		}

		public static bool operator >=(Vector2i a, TVector2i b)
		{
			return a.X >= b.X && a.Y >= b.Y;
		}

		public static bool operator >=(TVector2i a, Vector2i b)
		{
			return a.X >= b.X && a.Y >= b.Y;
		}

		public static bool operator >=(Vector2 a, TVector2i b)
		{
			return a.X >= b.X && a.Y >= b.Y;
		}

		public static bool operator >=(TVector2i a, Vector2 b)
		{
			return a.X >= b.X && a.Y >= b.Y;
		}

		public static bool operator <=(int a, TVector2i b)
		{
			return a <= b.X && a <= b.Y;
		}

		public static bool operator <=(TVector2i a, int b)
		{
			return a.X <= b && a.Y <= b;
		}

		public static bool operator <=(TVector2i a, TVector2i b)
		{
			return a.X <= b.X && a.Y <= b.Y;
		}

		public static bool operator <=(Vector2i a, TVector2i b)
		{
			return a.X <= b.X && a.Y <= b.Y;
		}

		public static bool operator <=(TVector2i a, Vector2i b)
		{
			return a.X <= b.X && a.Y <= b.Y;
		}

		public static bool operator <=(Vector2 a, TVector2i b)
		{
			return a.X <= b.X && a.Y <= b.Y;
		}

		public static bool operator <=(TVector2i a, Vector2 b)
		{
			return a.X <= b.X && a.Y <= b.Y;
		}

		public override string ToString()
		{
			return "[X: " + X + " | Y: " + Y + "]";
		}
	}
}