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

		public JChackboxGroup Group { get; set; }
		public bool IsSelected { get; set; } = false;

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
				if (Group != null)
				{
					Group.Update(this);
				}
				Checkbox.FillColor = SelectColor;
				
			}
		}

		public override void ReSize(Vector2f position, Vector2f size)
		{
			if (size.Y < size.X)
			{
				base.ReSize(new Vector2f(position.X + size.Y * 0.25f, position.Y), new Vector2f(size.X - size.Y * 0.25f, size.Y));
			}
			else
			{
				base.ReSize(new Vector2f(position.X + size.X * 0.25f, position.Y), new Vector2f(size.X - size.X * 0.25f, size.Y));
			}

			

			Box.Position = position;
			Box.Size = size;

			Position = position;
			Size = size;
			if (size.Y < size.X)
			{
				Checkbox.Size = new Vector2f(size.Y * 0.25f, size.Y * 0.25f);
			}
			else
			{
				Checkbox.Size = new Vector2f(size.X * 0.25f, size.X * 0.25f);
			}
			
			Checkbox.Position = new Vector2f(position.X, position.Y + size.Y/2f - Checkbox.Size.Y/2f);
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
