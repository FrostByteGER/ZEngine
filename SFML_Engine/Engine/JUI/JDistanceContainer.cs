using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	public class JDistanceContainer
	{
		public float Top { get; set; } = 0.0f;
		public float Bottem { get; set; } = 0.0f;
		public float Left { get; set; } = 0.0f;
		public float Right { get; set; } = 0.0f;

		public Vector2f GetSizeWithDistanceTopLeft(Vector2f size)
		{
			return new Vector2f(size.X * Top, size.Y * Left);
		}

		public Vector2f GetSizeWithDistanceBottemRight(Vector2f size)
		{
			return new Vector2f(size.X * Bottem, size.Y * Right);
		}

		public void setAll(float value)
		{
			Top = value;
			Bottem = value;
			Left = value;
			Right = value;
		}
	}
}
