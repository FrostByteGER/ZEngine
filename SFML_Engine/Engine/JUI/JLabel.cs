using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	public class JLabel : JElement
	{
		public Text Text { get; set; } = new Text();

		public JLabel(JGUI gui) : base(gui)
		{
			Text.Font = gui.GUIFont;
			Text.FillColor = gui.DefaultTextColor;
		}

		public override void setPosition(Vector2f Position)
		{
			base.setPosition(Position);
			Text.Position = Position;
		}

		public override void setSize(Vector2f Size)
		{
			base.setSize(Size);
		}

		public override void ReSize(Vector2f position, Vector2f size)
		{
			base.ReSize(position, size);
			FloatRect textSize = Text.GetLocalBounds();

			Text.Position = new Vector2f(position.X, position.Y + size.Y/2 - textSize.Height/2);

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
