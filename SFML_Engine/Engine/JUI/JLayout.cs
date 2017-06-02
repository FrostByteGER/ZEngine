using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine.JUI
{
	public class JLayout
	{
		public JContainer Container { get; set; }

		public JLayout(JContainer container)
		{
			Container = container;
		}

		public virtual void ReSize()
		{
			Vector2f size = new Vector2f(Container.Box.Size.X, Container.Box.Size.Y/ Container.Elements.Count);

			for (int i = 0; i < Container.Elements.Count; i++)
			{
				if (Container.Elements[i] != null)
				{
					Container.Elements[i].ReSize(Container.Box.Position + new Vector2f(0, size.Y * i), size);
				}
			}
		}
	}
}
