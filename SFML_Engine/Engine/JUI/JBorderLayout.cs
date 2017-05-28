using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine.JUI
{
	class JBorderLayout : JLayout
	{

		public static int CENTER = 0;
		public static int TOP = 1;
		public static int BOTTOM = 2;
		public static int LEFT = 3;
		public static int RIGHT = 4;

		public float TopSize { get; set; } = 0;
		public float BottemSize { get; set; } = 0;
		public float LeftSize { get; set; } = 0;
		public float RightSize { get; set; } = 0;

		public JBorderLayout(JContainer container) : base(container)
		{
			for (int i = 0; i <= 6 - container.Elements.Count; i++)
			{
				Container.Elements.Add(null);
			}
		}

		public override void ReSize()
		{

			//base.ReSize();

			if (Container.Elements[TOP] != null)
			{
				//Container.Elements[TOP].setPosition(Container.Position);
				//Container.Elements[TOP].setSize(new Vector2f(Container.Size.X, Container.Size.Y * TopSize));

				Container.Elements[TOP].ReSize(Container.Position
					, new Vector2f(Container.Size.X, Container.Size.Y * TopSize));
			}
			if (Container.Elements[BOTTOM] != null)
			{
				//Container.Elements[BOTTOM].setPosition(new Vector2f(Container.Position.X, Container.Position.Y + Container.Size.Y * (1-BottemSize)));
				//Container.Elements[BOTTOM].setSize(new Vector2f(Container.Size.X, Container.Size.Y * BottemSize));

				Container.Elements[BOTTOM].ReSize(new Vector2f(Container.Position.X, Container.Position.Y + Container.Size.Y * (1 - BottemSize)),
					new Vector2f(Container.Size.X, Container.Size.Y * BottemSize));
			}
			if (Container.Elements[LEFT] != null)
			{
				//Container.Elements[LEFT].setPosition(new Vector2f(Container.Position.X, Container.Position.Y + Container.Size.Y * TopSize));
				//Container.Elements[LEFT].setSize(new Vector2f(Container.Size.X * LeftSize, Container.Size.Y - Container.Size.Y * TopSize - Container.Size.Y * BottemSize));
				Container.Elements[LEFT].ReSize(new Vector2f(Container.Position.X, Container.Position.Y + Container.Size.Y * TopSize),
					new Vector2f(Container.Size.X * LeftSize, Container.Size.Y - Container.Size.Y * TopSize - Container.Size.Y * BottemSize));

			}
			if (Container.Elements[RIGHT] != null)
			{
				//Container.Elements[RIGHT].setPosition(new Vector2f(Container.Position.X + Container.Size.X * (1-RightSize), Container.Position.Y + Container.Size.Y * (1-TopSize)));
				//Container.Elements[RIGHT].setSize(new Vector2f(Container.Size.X * RightSize, Container.Size.Y - Container.Size.Y * TopSize - Container.Size.Y * BottemSize));
				Container.Elements[RIGHT].ReSize(new Vector2f(Container.Position.X + Container.Size.X * (1 - RightSize), Container.Position.Y + Container.Size.Y * TopSize)
					, new Vector2f(Container.Size.X * RightSize, Container.Size.Y - Container.Size.Y * TopSize - Container.Size.Y * BottemSize));
			}
			if (Container.Elements[CENTER] != null)
			{
				//Container.Elements[CENTER].setPosition(new Vector2f(Container.Position.X + Container.Size.X * LeftSize, Container.Position.Y + Container.Size.Y * TopSize));
				//Container.Elements[CENTER].setSize(new Vector2f(Container.Size.X * (1-(LeftSize + RightSize)), Container.Size.Y * (1-(TopSize + BottemSize))));


				Container.Elements[CENTER].ReSize(new Vector2f(Container.Position.X + Container.Size.X * LeftSize, Container.Position.Y + Container.Size.Y * TopSize),
					new Vector2f(Container.Size.X * (1 - (LeftSize + RightSize)), Container.Size.Y * (1 - (TopSize + BottemSize))));
			}
		}

	}
}
