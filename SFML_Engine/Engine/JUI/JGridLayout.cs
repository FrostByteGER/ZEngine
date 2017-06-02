using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	public class JGridLayout : JLayout
	{

		public int Rows { get; set; } = 1;
		public int Columns { get; set; } = 1;

		public JGridLayout(JContainer container) : base(container)
		{
		}

		public override void ReSize()
		{

			Vector2f size = new Vector2f(Container.Box.Size.X / Rows, Container.Box.Size.Y / Columns);

			for (int row = 0; row < Rows; row++)
			{
				for (int column = 0; column < Columns; column++)
				{
					if (row * Columns + column < Container.Elements.Count && Container.Elements[row * Columns + column] != null)
					{
						Container.Elements[row * Columns + column].ReSize(Container.Box.Position + new Vector2f(size.X * row, size.Y * column), size);
					}
				}
			}
		}
	}
}
