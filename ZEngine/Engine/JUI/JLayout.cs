using SFML.System;

namespace ZEngine.Engine.JUI
{
	public class JLayout
	{
		public JContainer Container { get; set; }

		public JLayout(JContainer container)
		{
			Container = container;
			Container.Layout = this;
		}

		protected void setElementSizeAndPosition(int eindex, Vector2f p, Vector2f s)
		{

			Container.Elements[eindex].ReSize(p + Container.Margin.GetSizeWithDistanceTopLeft(s), s - (Container.Margin.GetSizeWithDistanceTopLeft(s) + Container.Margin.GetSizeWithDistanceBottemRight(s)));

		}

		public virtual void ReSize()
		{
			Vector2f size = new Vector2f(Container.Box.Size.X, Container.Box.Size.Y/ Container.Elements.Count);

			for (int i = 0; i < Container.Elements.Count; i++)
			{
				if (Container.Elements[i] != null)
				{
					//Container.Elements[i].ReSize(Container.Box.Position + new Vector2f(0, size.Y * i), size);

					setElementSizeAndPosition(i, Container.Box.Position + new Vector2f(0, size.Y * i), size);
				}
			}
		}
	}
}
