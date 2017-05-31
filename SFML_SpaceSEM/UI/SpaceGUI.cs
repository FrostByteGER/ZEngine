using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML_Engine.Engine;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.JUI;

namespace SFML_SpaceSEM
{
	class SpaceGUI : JGUI
	{
		public SpaceGUI(Font font, RenderWindow renderwindow, InputManager inputManager) : base(font, renderwindow, inputManager)
		{
		}
	}
}
