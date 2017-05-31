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

		//Default Color (i don want to handle NullpointerExceptions), lol i don't need a Default Color to avoid NullpointerExceptions ,but i want to see something.
		public Color DefaultElementColor { get; set; } = new Color(225, 225, 225);
		public Color DefaultBackgroundColor { get; set; }  = new Color(0, 0, 0);
		public Color DefaultTextColor { get; set; }  = new Color(255, 255, 255);
		public Color DefaultEffectColor1 { get; set; } = new Color(162,162,162);
		public Color DefaultEffectColor2 { get; set; } = new Color(128,128,128);
		public Color DefaultEffectColor3 { get; set; } = new Color(64,64,64);

		public GUI(Font font, RenderWindow renderwindow, InputManager inputManager)
		{
			GUIFont = font;
			this.renderwindow = renderwindow;
			this.InputManager = inputManager;
			InputManager.RegisterEngineInput(ref this.renderwindow);

			InputManager.RegisterInput(OnMouseButtonPressed, OnMouseButtonReleased, OnMouseMoved, OnMouseScrolled,
				OnKeyPressed, OnKeyReleased, OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved,
				OnTouchBegan, OnTouchEnded, OnTouchMoved);
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

		protected virtual void OnMouseButtonPressed(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Button Pressed: " + mouseButtonEventArgs.Button + " at X: " + mouseButtonEventArgs.X + " Y: " + mouseButtonEventArgs.Y);
		}

		protected virtual void OnMouseButtonReleased(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Button Released: " + mouseButtonEventArgs.Button + " at X: " + mouseButtonEventArgs.X + " Y: " + mouseButtonEventArgs.Y);
		}

		protected virtual void OnMouseMoved(object sender, MouseMoveEventArgs mouseMoveEventArgs)
		{
			//Console.WriteLine("OnMouseMoved");
			if (HoverElement != null)
			{
				if (HoverElement.IsPressed)
				{
					HoverElement.Drag(sender, mouseMoveEventArgs);
				}
				else
				{
					HoverElement.OnMouseMoved(sender, mouseMoveEventArgs);
				}		
			}
		}

		protected virtual void OnMouseScrolled(object sender, MouseWheelScrollEventArgs mouseWheelScrollEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Mouse Scrolled Wheel: " + mouseWheelScrollEventArgs.Wheel + " at X: " + mouseWheelScrollEventArgs.X + " Y: " + mouseWheelScrollEventArgs.Y + " by Scroll Amount: " + mouseWheelScrollEventArgs.Delta);
		}

		protected virtual void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Keyboard Key Pressed: " + keyEventArgs.Code);
		}

		protected virtual void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Keyboard Key Released: " + keyEventArgs.Code);
		}

		protected virtual void OnJoystickConnected(object sender, JoystickConnectEventArgs joystickConnectEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Joystick Connected: JoystickID: " + joystickConnectEventArgs.JoystickId);
		}

		protected virtual void OnJoystickDisconnected(object sender, JoystickConnectEventArgs joystickConnectEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Joystick Disconnected: JoystickID: " + joystickConnectEventArgs.JoystickId);
		}

		protected virtual void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + PlayerPawn.ActorID + " Input Event: Joystick Button Pressed: JoystickID: " + joystickButtonEventArgs.JoystickId + " Button: " + joystickButtonEventArgs.Button);
		}

		protected virtual void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Joystick Button Released: JoystickID: " + joystickButtonEventArgs.JoystickId + " Button: " + joystickButtonEventArgs.Button);
		}

		protected virtual void OnJoystickMoved(object sender, JoystickMoveEventArgs joystickMoveEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + PlayerPawn.ActorID + " Input Event: Joystick Moved: JoystickID: " + joystickMoveEventArgs.JoystickId + " Axis: " + joystickMoveEventArgs.Axis + " to Position: " + joystickMoveEventArgs.Position);
		}

		protected virtual void OnTouchBegan(object sender, TouchEventArgs touchEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Touch Pressed: Finger: " + touchEventArgs.Finger + " at X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}

		protected virtual void OnTouchEnded(object sender, TouchEventArgs touchEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Touch Released: Finger: " + touchEventArgs.Finger + " at X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}

		protected virtual void OnTouchMoved(object sender, TouchEventArgs touchEventArgs)
		{
			//Console.WriteLine("PlayerController: " + Name + "-" + ActorID + " Input Event: Touch Moved: Finger: " + touchEventArgs.Finger + " to X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}

		// Is Called if Something interacts with the GUI
		public virtual void Interact(JElement instigator)
		{
			if (instigator is JCheckbox)
			{
				Console.WriteLine("testIt >|" + ((JCheckbox)instigator).Text.DisplayedString + "|<");
			}
			else
			{
				Console.WriteLine("testIt >|" + instigator + "|<");
			}	
		}
	}
}
