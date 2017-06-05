using SFML.Graphics;

namespace SFML_Engine.Engine.JUI
{
	public class JButton : JLabel
	{

		public Color HoverColor { get; set; }
		public Color SelectColor { get; set; }

		public JButton(JGUI gui) : base(gui)
		{
			HoverColor = gui.DefaultEffectColor1;
			SelectColor = gui.DefaultEffectColor2;
		}

		public override void Entered()
		{
			base.Entered();
			IsHovered = true;
			Box.FillColor = HoverColor;
		}

		public override void Leave()
		{
			base.Leave();
			IsHovered = false;
			Box.FillColor = BackGroundColor;
		}

		public override void Pressed()
		{
			if (IsEnabled)
			{
				base.Pressed();
				Box.FillColor = SelectColor;
				this.Execute();
			}		
		}

		public override void Released()
		{
			base.Released();
			if (IsHovered)
			{
				Box.FillColor = HoverColor;
			}
			else
			{
				Box.FillColor = BackGroundColor;
			}
		}
	}
}
