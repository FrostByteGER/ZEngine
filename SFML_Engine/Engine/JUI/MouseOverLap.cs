using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	public class MouseOverLap
	{

		public bool Overlaping(JElement element, Vector2i mousePosition)
		{
			if (element.Position.X < mousePosition.X && element.Position.X + element.Size.X > mousePosition.X &&
				element.Position.Y < mousePosition.Y && element.Position.Y + element.Size.Y > mousePosition.Y)
			{
				return true;
			}

			return false;
		}

	}
}
