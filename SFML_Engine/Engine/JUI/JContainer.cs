using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	public class JContainer : JElement
	{
		public JLayout Layout { set; get; }
		public List<JElement> Elements { set; get; } = new List<JElement>();
		public JDistanceContainer Margin { get; set; } = new JDistanceContainer();

		public JContainer(JGUI gui) : base(gui)
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

		public bool addElement(JElement element, int layoutinfo)
		{
			if (Layout != null)
			{
				Elements[layoutinfo] = element;
				Layout.ReSize();
				return true;
			}
			return false;
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

		public override void ReSize(Vector2f position, Vector2f size)
		{
			base.ReSize(position, size);
			if (Layout != null)
			{
				Layout.ReSize();
			}

		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			foreach (JElement element in Elements)
			{
				if (element != null)
				{
					element.Draw(target, states);
				}
			}
		}
	}
}
