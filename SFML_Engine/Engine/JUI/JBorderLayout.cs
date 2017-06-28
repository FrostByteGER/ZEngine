using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	public class JBorderLayout : JLayout
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
			for (int i = 0; 5 >= container.Elements.Count; i++)
			{
				Container.Elements.Insert(i,null);
			}
		}

		public override void ReSize()
		{
			if (Container.Elements[TOP] != null)
			{
				//Container.Elements[TOP].ReSize(Container.Position
				//	, new Vector2f(Container.Size.X, Container.Size.Y * TopSize));

				setElementSizeAndPosition(TOP, Container.Position, new Vector2f(Container.Size.X, Container.Size.Y * TopSize));
			}
			if (Container.Elements[BOTTOM] != null)
			{
				//Container.Elements[BOTTOM].ReSize(new Vector2f(Container.Position.X, Container.Position.Y + Container.Size.Y * (1 - BottemSize)),
				//	new Vector2f(Container.Size.X, Container.Size.Y * BottemSize));

				setElementSizeAndPosition(BOTTOM, new Vector2f(Container.Position.X, Container.Position.Y + Container.Size.Y * (1 - BottemSize)), new Vector2f(Container.Size.X, Container.Size.Y * BottemSize));
			}
			if (Container.Elements[LEFT] != null)
			{
				//Container.Elements[LEFT].ReSize(new Vector2f(Container.Position.X, Container.Position.Y + Container.Size.Y * TopSize),
				//	new Vector2f(Container.Size.X * LeftSize, Container.Size.Y - Container.Size.Y * TopSize - Container.Size.Y * BottemSize));
				setElementSizeAndPosition(LEFT, new Vector2f(Container.Position.X, Container.Position.Y + Container.Size.Y * TopSize), new Vector2f(Container.Size.X * LeftSize, Container.Size.Y - Container.Size.Y * TopSize - Container.Size.Y * BottemSize));
			}
			if (Container.Elements[RIGHT] != null)
			{
				//Container.Elements[RIGHT].ReSize(new Vector2f(Container.Position.X + Container.Size.X * (1 - RightSize), Container.Position.Y + Container.Size.Y * TopSize)
				//	, new Vector2f(Container.Size.X * RightSize, Container.Size.Y - Container.Size.Y * TopSize - Container.Size.Y * BottemSize));
				setElementSizeAndPosition(RIGHT, new Vector2f(Container.Position.X + Container.Size.X * (1 - RightSize), Container.Position.Y + Container.Size.Y * TopSize), new Vector2f(Container.Size.X * RightSize, Container.Size.Y - Container.Size.Y * TopSize - Container.Size.Y * BottemSize));
			}
			if (Container.Elements[CENTER] != null)
			{
				//Container.Elements[CENTER].ReSize(new Vector2f(Container.Position.X + Container.Size.X * LeftSize, Container.Position.Y + Container.Size.Y * TopSize),
				//	new Vector2f(Container.Size.X * (1 - (LeftSize + RightSize)), Container.Size.Y * (1 - (TopSize + BottemSize))));
				setElementSizeAndPosition(CENTER, new Vector2f(Container.Position.X + Container.Size.X * LeftSize, Container.Position.Y + Container.Size.Y * TopSize),
					new Vector2f(Container.Size.X * (1 - (LeftSize + RightSize)), Container.Size.Y * (1 - (TopSize + BottemSize))));
			}
		}

	}
}
