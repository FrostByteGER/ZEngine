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
		public RectangleShape Checkbox { get; set; } = new RectangleShape();
		public Color CheckBoxColor { get; set; } = new Color(32,64,128);
		public Color HoverColor { get; set; } = new Color(128, 128, 255);
		public Color SelectColor { get; set; } = new Color(255, 255, 255);
		public bool IsSelected { get; set; } = false;

		public bool IsHovered { set; get; } = false;

		public JCheckbox(GUI gui) : base(gui)
		{
			Checkbox.FillColor = CheckBoxColor;
		}

		public override void Entered()
		{
			base.Entered();
			IsHovered = true;
			if (!IsSelected)
			{
				Checkbox.FillColor = HoverColor;
			}
		}

		public override void Leave()
		{
			base.Leave();
			IsHovered = false;
			if (!IsSelected)
			{
				Checkbox.FillColor = CheckBoxColor;
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
					Checkbox.FillColor = HoverColor;
				}
				else
				{
					Checkbox.FillColor = CheckBoxColor;
				}
			}
			else
			{
				IsSelected = true;
				Checkbox.FillColor = SelectColor;
				
			}
		}

		public override void ReSize(Vector2f position, Vector2f size)
		{

			base.ReSize(position, size);

			Checkbox.Position = position;
			Checkbox.Size = new Vector2f(size.Y * 0.25f, size.Y * 0.25f);

			//this.Position = position + new Vector2f(size.Y, 0);
			//this.Size = new Vector2f(size.X-size.Y, size.Y);

			//base.ReSize(position + new Vector2f(size.Y * 0.25f, 0), new Vector2f(size.X - size.Y, size.Y));

		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			if (Visable)
			{
				Box.Draw(target, states);
				Checkbox.Draw(target, states);
				Text.Draw(target, states);
			}
		}
	}
}
