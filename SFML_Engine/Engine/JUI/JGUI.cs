using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.JUI
{
	public class JGUI : ITickable, Drawable
	{
		public Font GUIFont { get; set; }
		public JContainer RootContainer { get; set; }

		public InputManager InputManager { get; set; }

		public RenderWindow renderwindow;

		private MouseOverLap MOL = new MouseOverLap();

		private JElement HoverElement;

		private JElement LastSelectedElement;

		public CircleShape SelecterCircel { get; set; } = new CircleShape();
		public Vector2i SelecterPoint { get; set; } = new Vector2i(0,0);
		public Vector2i SelectorMovment { get; set; } = new Vector2i(0,0);
		public bool UseSelector { get; set; } = false;

		//Default Color (i don want to handle NullpointerExceptions), lol i don't need a Default Color to avoid NullpointerExceptions ,but i want to see something.
		public Color DefaultElementColor { get; set; } = new Color(225, 225, 225);
		public Color DefaultBackgroundColor { get; set; }  = new Color(0, 0, 0);
		public Color DefaultTextColor { get; set; }  = new Color(255, 255, 255);
		public Color DefaultEffectColor1 { get; set; } = new Color(162,162,162);
		public Color DefaultEffectColor2 { get; set; } = new Color(128,128,128);
		public Color DefaultEffectColor3 { get; set; } = new Color(64,64,64);

		public bool CanTick { get; set; } = true;

		private bool _isActive = false;

		public bool IsActive
		{
			get => _isActive;
			set
			{
				if (!value)
				{
					UnregisterInput();
				}
				else
				{
					RegisterInput();
				}

				_isActive = value;
			}
		}

		public virtual void RegisterInput()
		{
			InputManager.RegisterInput(OnMouseButtonPressed, OnMouseButtonReleased, OnMouseMoved, OnMouseScrolled,
				OnKeyPressed, OnKeyDown, OnKeyReleased, OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved,
				OnTouchBegan, OnTouchEnded, OnTouchMoved);
		}

		public virtual void UnregisterInput()
		{
			InputManager.UnregisterInput(OnMouseButtonPressed, OnMouseButtonReleased, OnMouseMoved, OnMouseScrolled,
				OnKeyPressed, OnKeyDown, OnKeyReleased, OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved,
				OnTouchBegan, OnTouchEnded, OnTouchMoved);
			
		}

		public JGUI(Font font, RenderWindow renderwindow, InputManager inputManager)
		{
			GUIFont = font;
			this.renderwindow = renderwindow;
			this.InputManager = inputManager;
			InputManager.RegisterEngineInput(ref this.renderwindow);

			InputManager.RegisterInput(OnMouseButtonPressed, OnMouseButtonReleased, OnMouseMoved, OnMouseScrolled,
				OnKeyPressed, OnKeyDown, OnKeyReleased, OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved,
				OnTouchBegan, OnTouchEnded, OnTouchMoved);

			SelecterCircel.FillColor = Color.Red;
			SelecterCircel.Radius  = 6f;
		}

		public void Tick(float deltaTime)
		{
			if (InputManager != null)
			{
				if (UseSelector)
				{
					SelecterPoint += SelectorMovment;
				}

				JElement element = getSelectedElement(RootContainer);
				
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

		private void PressElement(JElement element)
		{
			element.Pressed();
		}

		private void ReleasElement(JElement element)
		{
			element.Released();
		}

		private JElement getSelectedElement(JContainer container)
		{

			JElement tempElement;

			foreach (JElement e in container.Elements)
			{
				if (e == null || !e.IsVisable)
				{
					continue;
				}
				if (e is JContainer)
				{
					tempElement = getSelectedElement((JContainer)e);
					if (tempElement == null){}
					else if (MOL.Overlaping(tempElement, SelecterPoint))
					{
						return tempElement;
					}

				} else if(e is JElement)
				{
					if (MOL.Overlaping(e, SelecterPoint))
					{
						return e;
					}
				}
			}
			if (MOL.Overlaping(container, SelecterPoint))
			{
				return container;
			}
			return null;
		}

		public void Draw(RenderTarget target, RenderStates states)
		{
			target.SetView(target.DefaultView);

			SelecterCircel.Position = (Vector2f)SelecterPoint;

			if (RootContainer != null)
			{
				if (RootContainer.Layout != null)
				{
					RootContainer.Layout.ReSize();
				}
				
				RootContainer.Draw(target, states);
			}
			if (UseSelector)
			{
				SelecterCircel.Draw(target, states);
			}
		}

		protected virtual void OnMouseButtonPressed(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			if (InputManager.MouseLeftPressed && HoverElement != null && LastSelectedElement != HoverElement)
			{
				HoverElement.Pressed();
				LastSelectedElement = HoverElement;
			}
		}

		protected virtual void OnMouseButtonReleased(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			if (LastSelectedElement != null && !InputManager.MouseLeftPressed)
			{
				LastSelectedElement.Released();
				LastSelectedElement = null;
			}
		}

		protected virtual void OnMouseMoved(object sender, MouseMoveEventArgs mouseMoveEventArgs)
		{
			UseSelector = false;
			SelecterPoint = new Vector2i(mouseMoveEventArgs.X - (int)(SelecterCircel.Radius / 2f), mouseMoveEventArgs.Y - (int)(SelecterCircel.Radius / 2f));

			if (HoverElement != null)
			{
				if (HoverElement.IsPressed)
				{
					HoverElement.Drag(SelecterPoint);
				}
				else
				{
					HoverElement.Hover(SelecterPoint);
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

		protected virtual void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
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

			// A
			if (joystickButtonEventArgs.Button == 1)
			{
				if (HoverElement != null && LastSelectedElement != HoverElement)
				{
					HoverElement.Pressed();
					LastSelectedElement = HoverElement;
				}
			}
		}

		protected virtual void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			// A
			if (joystickButtonEventArgs.Button == 1)
			{
				if (LastSelectedElement != null)
				{
					LastSelectedElement.Released();
					LastSelectedElement = null;
				}
			}
		}

		protected virtual void OnJoystickMoved(object sender, JoystickMoveEventArgs joystickMoveEventArgs)
		{
			UseSelector = true;

			if (joystickMoveEventArgs.Axis == Joystick.Axis.X || joystickMoveEventArgs.Axis == Joystick.Axis.U)
			{
				SelectorMovment = new Vector2i((int)(joystickMoveEventArgs.Position/10f), SelectorMovment.Y);
			}
			else if(joystickMoveEventArgs.Axis == Joystick.Axis.Y || joystickMoveEventArgs.Axis == Joystick.Axis.R)
			{
				SelectorMovment = new Vector2i(SelectorMovment.X, (int)(joystickMoveEventArgs.Position/10f));
			}
			if (HoverElement != null)
			{
				if (HoverElement.IsPressed)
				{
					HoverElement.Drag(SelecterPoint);
				}
				else
				{
					HoverElement.Hover(SelecterPoint);
				}
			}
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
		public virtual void Interact()
		{
		}

		public void Dispose()
		{
			if (GUIFont != null)
			{
				GUIFont.Dispose();

				DisposeAllElements(RootContainer);

			}
		}

		private void DisposeAllElements(JElement lastElement)
		{
			//TODO
		}
	}
}
