using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	public class JCheckbox : JLabel
	{
		public Color CheckBoxColor { get; set; }
		public Color HoverColor { get; set; }
		public Color SelectColor { get; set; }

		public JCheckboxGroup Group { get; set; }
		public bool IsSelected { get; set; } = false;

		public JCheckbox(JGUI gui) : base(gui)
		{
			CheckBoxColor = gui.DefaultEffectColor3;
			HoverColor = gui.DefaultEffectColor1;
			SelectColor = gui.DefaultEffectColor2;
			Box.FillColor = CheckBoxColor;
		}

		public void Select()
		{
			IsSelected = true;
			OnSelect();
			if (Group != null)
			{
				Group.Update(this);
			}
			Box.FillColor = SelectColor;
		}

		public void Deselect()
		{
			IsSelected = false;
			OnDeselect();
			if (IsHovered)
			{
				Box.FillColor = HoverColor;
			}
			else
			{
				Box.FillColor = CheckBoxColor;
			}
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
			if (IsEnabled)
			{
				if (IsSelected)
				{
					Deselect();
				}
				else
				{
					Select();
				}
			}
			base.Pressed();

		}

		public virtual void OnSelect()
		{
			Execute();
		}

		public virtual void OnDeselect()
		{
			Execute();
		}

		public override void ReSize(Vector2f position, Vector2f size)
		{
			base.ReSize(position, size);
		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
		}
	}
}
