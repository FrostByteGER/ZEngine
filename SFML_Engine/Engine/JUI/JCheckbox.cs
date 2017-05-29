using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	class JCheckbox : JLabel
	{
		public Color CheckBoxColor { get; set; }
		public Color HoverColor { get; set; }
		public Color SelectColor { get; set; }

		public JChackboxGroup Group { get; set; }
		public bool IsSelected { get; set; } = false;

		public JCheckbox(GUI gui) : base(gui)
		{
			CheckBoxColor = gui.DefaultEffectColor3;
			HoverColor = gui.DefaultEffectColor1;
			SelectColor = gui.DefaultEffectColor2;
			Box.FillColor = CheckBoxColor;
		}

		public override void Entered()
		{
			base.Entered();
			IsHovered = true;
			if (!IsSelected)
			{
				Box.FillColor = HoverColor;
			}
		}

		public override void Leave()
		{
			base.Leave();
			IsHovered = false;
			if (!IsSelected)
			{
				Box.FillColor = CheckBoxColor;
			}	
		}

		public override void Pressed()
		{
			base.Pressed();
			if (IsSelected)
			{
				IsSelected = false;
				if (IsHovered)
				{
					Box.FillColor = HoverColor;
				}
				else
				{
					Box.FillColor = CheckBoxColor;
				}
			}
			else
			{
				IsSelected = true;
				if (Group != null)
				{
					Group.Update(this);
				}
				Box.FillColor = SelectColor;
				
			}
		}

		public override void ReSize(Vector2f position, Vector2f size)
		{
			base.ReSize(position, size);
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			if (Visable)
			{
				Box.Draw(target, states);
				Text.Draw(target, states);

			}
		}
	}
}
