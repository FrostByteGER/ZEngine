using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	class JContainer : JElement
	{

		public JLayout Layout { set; get; }
		public List<JElement> Elements { set; get; } = new List<JElement>();

		public JContainer(GUI gui) : base(gui)
		{
		}

		public void addElement(JElement element)
		{
			Elements.Add(element);
			if (Layout != null)
			{
				Layout.ReSize();
			}
		}

		public bool removeElement(JElement element)
		{
			if (Elements.Contains(element))
			{
				Elements.Remove(element);
				if (Layout != null)
				{
					Layout.ReSize();
				}
				return true;
			}
			return false;
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			foreach (JElement element in Elements)
			{
				element.Draw(target, states);
			}
		}
	}
}
