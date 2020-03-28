using SFML.System;

namespace ZEngine.Engine.JUI
{
	public class JDistanceContainer
	{
		public float Top { get; set; } = 0.0f;
		public float Bottem { get; set; } = 0.0f;
		public float Left { get; set; } = 0.0f;
		public float Right { get; set; } = 0.0f;

		public Vector2 GetSizeWithDistanceTopLeft(Vector2 size)
		{
			return new Vector2(size.X * Left, size.Y * Top);
		}

		public Vector2 GetSizeWithDistanceBottemRight(Vector2 size)
		{
			return new Vector2(size.X * Right, size.Y * Bottem);
		}

		public void setAll(float value)
		{
			Top = value;
			Bottem = value;
			Left = value;
			Right = value;
		}

		public void SetHorizontal(float value)
		{
			Left = value;
			Right = value;
		}

		public void SetVertikal(float value)
		{
			Top = value;
			Bottem = value;
		}
	}
}
