using SFML.Graphics;
using SFML.System;

namespace ZEngine.Engine.JUI
{
	public class JElement : Drawable
	{

		public JGUI gui { get; private set; }
		public virtual Vector2 Position { get; set; } = new Vector2(0, 0);
		public virtual Vector2 Size { get; set; } = new Vector2(0, 0);
		public Color BackGroundColor { get; set; }
		public virtual RectangleShape Box { get; set; } = new RectangleShape();
		public bool IsVisable { get; set; } = true;
		public bool IsEnabled { get; set; } = true;
		public bool IsHovered { get; set; } = false;
		public bool IsPressed { get; set; } = false;

		public JDistanceContainer Padding { get; set; } = new JDistanceContainer();

		public delegate void DoExecute();
		public event DoExecute OnExecute;

		public delegate void DoHover();
		public event DoHover OnHover;

		public delegate void DoDrag();
		public event DoDrag OnDrag;

		public delegate void DoPressed();
		public event DoPressed OnPressed;

		public delegate void DoReleased();
		public event DoReleased OnReleased;

		public delegate void DoEnter();
		public event DoEnter OnEnter;

		public delegate void DoLeave();
		public event DoLeave OnOnLeave;

		public JElement(JGUI gui)
		{
			this.gui = gui;
			OnExecute += gui.Interact;
			setBackgroundColor(gui.DefaultBackgroundColor);
		}

		public virtual void setPosition(Vector2 position)
		{
			this.Position = position;
		}

		public virtual void setSize(Vector2 size)
		{
			this.Size = size;
		}

		public virtual void setBackgroundColor(Color color)
		{
			this.BackGroundColor = color;
			Box.FillColor = color;
		}

		public virtual void Draw(RenderTarget target, RenderStates states)
		{
			if (IsVisable)
			{
				Box.Draw(target, states);
			}
		}

		public void ReSize()
		{
			ReSize(Position, Size);
		}

		public virtual void ReSize(Vector2 position, Vector2 size)
		{

			Position = position;
			Size = size;

			Box.Position = position + Padding.GetSizeWithDistanceTopLeft(size);
			Box.Size = size - (Padding.GetSizeWithDistanceTopLeft(size) + Padding.GetSizeWithDistanceBottemRight(size));
		}

		public virtual void Pressed() {
			IsPressed = true;
			if (OnPressed != null && IsEnabled) OnPressed();
		}

		public virtual void Released() {
			IsPressed = false;
			if (OnReleased != null && IsEnabled) OnReleased();
		}

		public virtual void Entered() {
			IsHovered = true;
			if (OnEnter != null && IsEnabled) OnEnter();
		}

		public virtual void Leave() {
			IsPressed = false;
			IsHovered = false;
			if (OnOnLeave != null && IsEnabled) OnOnLeave();
		}

		public void Execute()
		{
			if (OnExecute != null && IsEnabled) OnExecute();
		}

		public virtual void Hover(Vector2i position)
		{
			if (OnHover != null && IsEnabled) OnHover();
		}

		public virtual void Drag(Vector2i position)
		{
			if (OnDrag != null && IsEnabled) OnDrag();
		}
	}
}
