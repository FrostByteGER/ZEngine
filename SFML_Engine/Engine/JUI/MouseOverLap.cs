using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine.JUI
{
	class MouseOverLap
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
