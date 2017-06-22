using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	public class JSlider : JElement
	{
		public int DisplayTyp = 0;
		public static int HORIZONTAL { get; } = 0;
		public static int VERTICAL { get; } = 1;

		public Color SilderColor { get; set; }
		public RectangleShape Slider = new RectangleShape();
		public RectangleShape Cross = new RectangleShape();
		public float SliderSize = 15f;
		public float CrossSize = 5f;
		public float SliderValue = 0.5f;

		public JSlider(JGUI gui) : base(gui)
		{
			SilderColor = gui.DefaultEffectColor1;
			Slider.FillColor = gui.DefaultElementColor;
			Cross.FillColor = gui.DefaultElementColor;
		}

		public override void Drag(Vector2i position)
		{
			base.Drag(position);
			if (DisplayTyp == HORIZONTAL)
			{
				SliderValue = (((float)position.X) - Position.X) / Size.X;

				if (SliderValue < 0)
				{
					SliderValue = 0;
				}
				else if(SliderValue > 1)
				{
					SliderValue = 1;
				}

				Slider.Position = new Vector2f(Position.X + Size.X * SliderValue - Slider.Size.X / 2f + SliderSize / 2f - (SliderSize * SliderValue), Position.Y);
			}
			else if(DisplayTyp == VERTICAL)
			{
				SliderValue = (((float)position.Y) - Position.Y) / Size.Y;

				if (SliderValue < 0)
				{
					SliderValue = 0;
				}
				else if (SliderValue > 1)
				{
					SliderValue = 1;
				}

				Slider.Position = new Vector2f(Position.X, Position.Y + Size.Y * SliderValue - Slider.Size.Y / 2f + SliderSize / 2f - (SliderSize * SliderValue));
			}
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);

			Slider.Draw(target, states);
			Cross.Draw(target, states);
		}

		public override void ReSize(Vector2f position, Vector2f size)
		{
			base.ReSize(position, size);
			if (DisplayTyp == HORIZONTAL)
			{
				Slider.Size = new Vector2f(SliderSize, Size.Y);
				Slider.Position = new Vector2f(Position.X + Size.X * SliderValue - Slider.Size.X / 2f, Position.Y);
				Cross.Size = new Vector2f(Size.X, CrossSize);
				Cross.Position = new Vector2f(position.X, position.Y + size.Y/2f - Cross.Size.Y);
			}
			else if(DisplayTyp == VERTICAL)
			{
				Slider.Size = new Vector2f(Size.X, SliderSize);
				Slider.Position = new Vector2f(position.X, position.Y + Size.Y * SliderValue - Slider.Size.Y / 2f);
				Cross.Size = new Vector2f(CrossSize, Size.Y);
				Cross.Position = new Vector2f(position.X + size.X / 2f - Cross.Size.X, position.Y);
			}
		}
	}
}
