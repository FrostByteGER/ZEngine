using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine.JUI
{
	class JButton : JLabel
	{

		public Color HoverColor { get; set; } = new Color(128, 128, 255);
		public Color SelectColor { get; set; } = new Color(255, 255, 255);

		public JButton(GUI gui) : base(gui)
		{
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
			base.Pressed();
			Box.FillColor = SelectColor;
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
