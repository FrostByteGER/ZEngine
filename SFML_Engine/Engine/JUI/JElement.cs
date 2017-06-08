using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine.JUI
{
	public class JElement : Drawable
	{

		public JGUI gui { get; private set; }
		public Vector2f Position { get; set; } = new Vector2f(0, 0);
		public Vector2f Size { get; set; } = new Vector2f(0, 0);
		public Color BackGroundColor { get; set; }
		public RectangleShape Box { get; set; } = new RectangleShape();
		public int UI_ID = -1;
		public bool IsVisable { get; set; } = true;
		public bool IsEnabled { get; set; } = true;
		public bool IsHovered { get; set; } = false;
		public bool IsPressed { get; set; } = false;
		public delegate void doSomething();
		public event doSomething Something;

		public JElement(JGUI gui)
		{
			this.gui = gui;
			Something += gui.Interact;
			setBackgroundColor(gui.DefaultBackgroundColor);
		}

		public virtual void setPosition(Vector2f position)
		{
			this.Position = position;
			Box.Position = position;
		}

		public virtual void setSize(Vector2f size)
		{
			this.Size = size;
			Box.Size = size;
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

		public virtual void ReSize(Vector2f position, Vector2f size)
		{
			setPosition(position);
			setSize(size);
		}

		public virtual void Pressed() {
			IsPressed = true;
		}

		public virtual void Released() {
			IsPressed = false;
		}

		public virtual void Entered() {
		}

		public virtual void Leave() {
			IsPressed = false;
		}

		public void Execute()
		{
			if (Something != null)
			{
				Something();
			}
		}

		public virtual void OnMouseMoved(object sender, Vector2i position)
		{

		}

		public virtual void Drag(object sender, Vector2i position)
		{

		}
	}
}
