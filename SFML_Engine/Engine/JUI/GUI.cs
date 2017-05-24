using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Engine.Engine.JUI
{
	class GUI : ITickable, Drawable
	{
		public Font GUIFont { get; set; }
		public JContainer RootContainer { get; set; }

		public InputManager InputManager { get; set; }

		public RenderWindow renderwindow;

		private MouseOverLap MOL = new MouseOverLap();

		private JElement HoverElement;

		private JElement LastSelectedElement;

		public GUI(Font font, RenderWindow renderwindow, InputManager inputManager)
		{
			GUIFont = font;
			this.renderwindow = renderwindow;
			this.InputManager = inputManager;
			InputManager.RegisterEngineInput(ref this.renderwindow);
		}

		public void Tick(float deltaTime)
		{
			if (InputManager != null)
			{
				JElement element = getSelectedElement(RootContainer);

				if (InputManager.MouseLeftPressed && HoverElement != null && LastSelectedElement != HoverElement)
				{
					HoverElement.Pressed();
					LastSelectedElement = HoverElement;
				}

				if (LastSelectedElement != null && !InputManager.MouseLeftPressed)
				{
					LastSelectedElement.Released();
					LastSelectedElement = null;
				}

				if (element != HoverElement)
				{
					if (element != null)
					{
						element.Entered();
					}
					if (HoverElement != null)
					{
						HoverElement.Leave();
					}

					HoverElement = element;
				}
			}
		}

		private JElement getSelectedElement(JContainer container)
		{

			Vector2i v = Mouse.GetPosition(renderwindow);

			JElement tempElement;

			foreach (JElement e in container.Elements)
			{
				if (e is JContainer)
				{
					tempElement = getSelectedElement((JContainer)e);
					if (tempElement == null){}
					else if (MOL.Overlaping(tempElement, v))
					{
						return tempElement;
					}

				} else if(e is JElement)
				{
					if (MOL.Overlaping(e, v))
					{
						return e;
					}
				}
			}
			if (MOL.Overlaping(container, v))
			{
				return container;
			}
			return null;
		}

		public void Draw(RenderTarget target, RenderStates states)
		{
			if (RootContainer != null)
			{
				if (RootContainer.Layout != null)
				{
					RootContainer.Layout.ReSize();
				}
				
				RootContainer.Draw(target, states);
			}
		}
	}
}
