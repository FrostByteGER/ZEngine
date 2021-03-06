﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using ZEngine.Engine.Game;
using ZEngine.Engine.IO;

namespace ZEngine.Engine.JUI
{

	public class JGUI : ITickable, Drawable
	{
		public Level LevelRef;
		public Font GUIFont { get; set; }
		private JContainer _RootContainer { get; set; }

		public JContainer RootContainer {
			get => _RootContainer;
			set
			{
				_RootContainer = value;
				_RootContainer.setPosition(GUISpace.Position);
				_RootContainer.setSize(GUISpace.Size);
				_RootContainer.ReSize();
			}
		}

		public RectangleShape GUISpace { get; set; } = new RectangleShape();

		private InputManager _InputManager { get; set; }
		public InputManager InputManager {
			get => _InputManager;
			set
			{
				if(value == null) return;
				if (InputManager != null)
				{
					UnregisterInput();
				}

				_InputManager = value;

				Console.WriteLine(InputManager+" Input Manager");

				RegisterInput();
			}
		}

		public RenderWindow Renderwindow;

		private MouseOverLap MOL = new MouseOverLap();

		private JElement HoverElement;

		private JElement LastSelectedElement;

		public View GuiView { get; set; }

		public CircleShape SelecterCircel { get; set; } = new CircleShape();
		public Vector2i SelecterPoint { get; set; } = new Vector2i(0, 0);
		public Vector2i SelectorMovment { get; set; } = new Vector2i(0, 0);
		public bool UseSelector { get; set; } = false;

		//Default Color (i don want to handle NullpointerExceptions), lol i don't need a Default Color to avoid NullpointerExceptions ,but i want to see something.
		public Color DefaultElementColor { get; set; } = new Color(225, 225, 225);
		public Color DefaultBackgroundColor { get; set; } = new Color(0, 0, 0);
		public Color DefaultTextColor { get; set; } = new Color(255, 255, 255);
		public Color DefaultEffectColor1 { get; set; } = new Color(162, 162, 162);
		public Color DefaultEffectColor2 { get; set; } = new Color(128, 128, 128);
		public Color DefaultEffectColor3 { get; set; } = new Color(64, 64, 64);

		public bool CanTick { get; set; } = true;

		public bool IsHovered = false;

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
			this.Renderwindow = renderwindow;
			this.InputManager = inputManager;

			SelecterCircel.FillColor = Color.Red;
			SelecterCircel.Radius = 6f;

			renderwindow.Resized += Renderwindow_Resized;

			GuiView = renderwindow.DefaultView;
		}

		public JGUI(Font font, RenderWindow renderwindow)
		{
			GUIFont = font;
			this.Renderwindow = renderwindow;

			SelecterCircel.FillColor = Color.Red;
			SelecterCircel.Radius = 6f;

			renderwindow.Resized += Renderwindow_Resized;

			GuiView = renderwindow.DefaultView;
		}
			

		private void Renderwindow_Resized(object sender, SizeEventArgs e)
		{
			//RootContainer.ReSize(new Vector2(0, 0), new Vector2(e.Width,e.Height));

			GuiView.Size = new Vector2(e.Width, e.Height);
			GuiView.Center = new Vector2(e.Width/2f, e.Height/2f);

			RootContainer.Position = new Vector2(50, 50);
			RootContainer.Size = new Vector2(e.Width - 100, e.Height - 100);

		}

		public virtual void Tick(float deltaTime)
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

			if (HoverElement != null && HoverElement.BackGroundColor == Color.Transparent)
			{
				IsHovered = false;
			}
			else
			{
				IsHovered = true;
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

		public virtual void Draw(RenderTarget target, RenderStates states)
		{
			if (Renderwindow == null) return;

			Renderwindow.SetView(GuiView);

			SelecterCircel.Position = (Vector2)SelecterPoint - new Vector2(SelecterCircel.Radius/2f, SelecterCircel.Radius/2f);

			if (RootContainer != null)
			{
				if (RootContainer.Layout != null)
				{
					RootContainer.Layout.ReSize();
				}

				Renderwindow.Draw(RootContainer);

				//RootContainer.Draw(target, states);
			}
			if (UseSelector)
			{
				Renderwindow.Draw(SelecterCircel);
				//SelecterCircel.Draw(target, states);
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

			UseSelector = true;

			// new Vector2(mouseMoveEventArgs.X - (int)(SelecterCircel.Radius / 2f), mouseMoveEventArgs.Y - (int)(SelecterCircel.Radius / 2f))

			SelecterPoint = new Vector2i(mouseMoveEventArgs.X - (int)(SelecterCircel.Radius / 2f), mouseMoveEventArgs.Y - (int)(SelecterCircel.Radius / 2f));

			//SelecterPoint = new Vector2i(mouseMoveEventArgs.X - (int)(SelecterCircel.Radius / 2f), mouseMoveEventArgs.Y - (int)(SelecterCircel.Radius / 2f));

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
