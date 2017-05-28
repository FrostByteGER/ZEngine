using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine.JUI
{
	class JElement : Drawable
	{

		public GUI gui { get; private set; }

		public Vector2f Position { get; set; } = new Vector2f(0, 0);

		public Vector2f Size { get; set; } = new Vector2f(0, 0);

		public Color BackGroundColor { get; set; }

		public RectangleShape Box { get; set; } = new RectangleShape();

		public bool Visable { get; set; } = true;

		public bool IsHovered { get; set; } = false;

		public bool IsPressed { get; set; } = false;

		public JElement(GUI gui)
		{
			this.gui = gui;

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
			if (Visable)
			{
				Box.Draw(target, states);
			}
		}

		public virtual void ReSize(Vector2f position, Vector2f size)
		{
			setPosition(position);
			setSize(size);
		}

		public virtual void Pressed(){
			Console.WriteLine("Pressed" + Position);
			IsPressed = true;
		}

		public virtual void Released() {
			Console.WriteLine("Released" + Position);
			IsPressed = false;
		}

		public virtual void Entered(){
			Console.WriteLine("Entered" + Position);
		}

		public virtual void Leave(){
			Console.WriteLine("Leave" + Position);
			IsPressed = false;
		}

		public virtual void OnMouseMoved(object sender, MouseMoveEventArgs mouseMoveEventArgs)
		{
			
		}

		public virtual void Drag(object sender, MouseMoveEventArgs mouseMoveEventArgs)
		{
			
		}

	}
}
