using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.Utility
{
	public class TTransformable : Transformable
	{

		public static TTransformable operator +(TTransformable a, TTransformable b)
		{
			TTransformable t = new TTransformable();
			t.Position = a.Position + b.Position;
			t.Rotation = a.Rotation + b.Rotation;
			t.Scale = new Vector2f(a.Scale.X * b.Scale.X, a.Scale.Y * b.Scale.Y); //TODO: Verify!
			t.Origin = b.Origin;
			return t;
		}

		public static TTransformable operator +(TTransformable a, Transformable b)
		{
			TTransformable t = new TTransformable();
			t.Position = a.Position + b.Position;
			t.Rotation = a.Rotation + b.Rotation;
			t.Scale = new Vector2f(a.Scale.X * b.Scale.X, a.Scale.Y * b.Scale.Y); //TODO: Verify!
			t.Origin = b.Origin;
			return t;
		}

		public static TTransformable operator +(Transformable a, TTransformable b)
		{
			TTransformable t = new TTransformable();
			t.Position = a.Position + b.Position;
			t.Rotation = a.Rotation + b.Rotation;
			t.Scale = new Vector2f(a.Scale.X * b.Scale.X, a.Scale.Y * b.Scale.Y); //TODO: Verify!
			t.Origin = b.Origin;
			return t;
		}
	}
}