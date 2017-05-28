using SFML.Graphics;
using SFML.System;
using System;
using Text = SFML_Engine.Engine.SFML.Graphics.Text;

namespace SFML_Engine.Engine.JUI
{
	class JLabel : JElement
	{
		public Text Text { get; set; } = new Text();

		public JLabel(GUI gui) : base(gui)
		{
			Text.Font = gui.GUIFont;
			Text.Color = gui.DefaultTextColor;
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
			base.Draw(target, states);
			Text.Draw(target, states);
		}
	}
}
