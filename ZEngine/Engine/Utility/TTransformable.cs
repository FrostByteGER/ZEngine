using SFML.Graphics;
using SFML.System;

namespace ZEngine.Engine.Utility
{
	public class TTransformable : Transformable
	{

		public static TTransformable operator +(TTransformable a, TTransformable b)
		{
		    TTransformable t = new TTransformable
		    {
		        Position = a.Position + b.Position,
		        Rotation = a.Rotation + b.Rotation,
		        Scale = new Vector2f(a.Scale.X * b.Scale.X, a.Scale.Y * b.Scale.Y),
		        Origin = b.Origin
		    };
		    //TODO: Verify!
		    return t;
		}

		public static TTransformable operator +(TTransformable a, Transformable b)
		{
		    TTransformable t = new TTransformable
		    {
		        Position = a.Position + b.Position,
		        Rotation = a.Rotation + b.Rotation,
		        Scale = new Vector2f(a.Scale.X * b.Scale.X, a.Scale.Y * b.Scale.Y),
		        Origin = b.Origin
		    };
		    //TODO: Verify!
		    return t;
		}

		public static TTransformable operator +(Transformable a, TTransformable b)
		{
		    TTransformable t = new TTransformable
		    {
		        Position = a.Position + b.Position,
		        Rotation = a.Rotation + b.Rotation,
		        Scale = new Vector2f(a.Scale.X * b.Scale.X, a.Scale.Y * b.Scale.Y),
		        Origin = b.Origin
		    };
		    //TODO: Verify!
		    return t;
		}
	}
}