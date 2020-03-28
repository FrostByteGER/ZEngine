using System;
using SFML.Graphics;
using SFML.System;

namespace ZEngine.Engine.JUI
{
	public class JLabel : JElement
	{

		public int Orientation = 0;
		public static int OrientationLeft = 0;
		public static int OrientationCenter = 1;
		public static int OrientationRight = 2;

		public Text Text { get; set; } = new Text();

		public JLabel(JGUI gui) : base(gui)
		{
			Text.Font = gui.GUIFont;
			Text.FillColor = gui.DefaultTextColor;
		}

		public override void setPosition(Vector2 Position)
		{
			base.setPosition(Position);
			Text.Position = Position;
		}

		public override void setSize(Vector2 Size)
		{
			base.setSize(Size);
		}

		public void setTextString(String text)
		{
			Text.DisplayedString = text;
			ReSize();
		}

		public override void ReSize(Vector2 position, Vector2 size)
		{
			base.ReSize(position, size);
			FloatRect textSize = Text.GetLocalBounds();

			// auto resize from the Charsize
			/*
			if (Math.Abs(textSize.Width / size.X) > Math.Abs(textSize.Height / size.Y))
			{
				Text.CharacterSize = (uint)(((float)Text.CharacterSize) * (size.X / textSize.Width));
			}
			else
			{
				Text.CharacterSize = (uint)(((float)Text.CharacterSize) * (size.Y / textSize.Height));
			}
			*/
			/*
			Text.Scale = new Vector2((size.X / textSize.Width),(size.Y / textSize.Height));

			textSize = Text.GetLocalBounds();
			*/

			Text.Position = new Vector2(position.X + size.X / 2f - textSize.Width / 2f, position.Y + size.Y / 2f - textSize.Height / 2f);

			Text.Origin = new Vector2(Text.GetLocalBounds().Left, Text.GetLocalBounds().Top);
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			if (IsVisable)
			{
				base.Draw(target, states);
				Text.Draw(target, states);
			}
		}
	}
}
