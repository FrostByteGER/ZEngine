using SFML.Graphics;
using SFML.System;
using System;
using Text = SFML_Engine.Engine.SFML.Graphics.Text;

namespace SFML_Engine.Engine.JUI
{
	class JLabel : JElement
	{
		public JLabel(GUI gui) : base(gui)
		{
			Text.Font = gui.GUIFont;
		}

		public Text Text { get; set; } = new Text();

		public override void setPosition(Vector2f Position)
		{
			base.setPosition(Position);
			Text.Position = Position;
		}

		public override void setSize(Vector2f Size)
		{
			base.setSize(Size);
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			Text.Draw(target, states);
		}
	}
}
