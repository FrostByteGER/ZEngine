using System;
using SFML.Graphics;
using SFML.Window;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine.IO
{
    public class InputManager : ITickable
    {

		public bool MouseLeftPressed { get; set; }
		public bool MouseRightPressed { get; set; }
		public bool MouseMiddlePressed { get; set; }
		public bool MouseXButton1Pressed { get; set; }
		public bool MouseXButton2Pressed { get; set; }
		public bool MouseWheelVerticalMoved { get; set; }
		public bool MouseWheelHorizontalMoved { get; set; }

		public bool TouchPressed { get; set; }

		public EventHandler<MouseButtonEventArgs> MouseButtonPressed = delegate { };
		public EventHandler<MouseButtonEventArgs> MouseButtonReleased = delegate { };
	    public EventHandler<MouseMoveEventArgs> MouseMoved = delegate { };
	    public EventHandler<MouseWheelScrollEventArgs> MouseScrolled = delegate { };

	    public EventHandler<KeyEventArgs> KeyPressed = delegate { };
		public EventHandler<KeyEventArgs> KeyDown = delegate { };
		public EventHandler<KeyEventArgs> KeyReleased = delegate { };

		public EventHandler<JoystickConnectEventArgs> JoystickConnected = delegate { };
		public EventHandler<JoystickConnectEventArgs> JoystickDisconnected = delegate { };
		public EventHandler<JoystickButtonEventArgs> JoystickButtonPressed = delegate { };
	    public EventHandler<JoystickButtonEventArgs> JoystickButtonReleased = delegate { };
		public EventHandler<JoystickMoveEventArgs> JoystickMoved = delegate { };

	    public EventHandler<TouchEventArgs> TouchBegan = delegate { };
	    public EventHandler<TouchEventArgs> TouchEnded = delegate { };
	    public EventHandler<TouchEventArgs> TouchMoved = delegate { };

	    public bool CanTick { get; set; } = true;

	    public uint RegisteredKeyInputs { get; private set; } = 0;
	    public uint RegisteredMouseInputs { get; private set; } = 0;

		public virtual void Tick(float deltaTime)
		{
			if(RegisteredKeyInputs > 0) KeyDown(this, null);

			if (RegisteredMouseInputs > 0) MouseButtonPressed(this, null);
		}

		public void RegisterMouseInput(EventHandler<MouseButtonEventArgs> onMouseButtonPressed, EventHandler<MouseButtonEventArgs> onMouseButtonReleased
			, EventHandler<MouseMoveEventArgs> onMouseMoved, EventHandler<MouseWheelScrollEventArgs> onMouseScrolled)
		{
			MouseButtonPressed += onMouseButtonPressed;
			MouseButtonReleased += onMouseButtonReleased;
			MouseMoved += onMouseMoved;
			MouseScrolled += onMouseScrolled;
		}

		public void UnregisterMouseInput(EventHandler<MouseButtonEventArgs> onMouseButtonPressed, EventHandler<MouseButtonEventArgs> onMouseButtonReleased
			, EventHandler<MouseMoveEventArgs> onMouseMoved, EventHandler<MouseWheelScrollEventArgs> onMouseScrolled)
		{
			MouseButtonPressed -= onMouseButtonPressed;
			MouseButtonReleased -= onMouseButtonReleased;
			MouseMoved -= onMouseMoved;
			MouseScrolled -= onMouseScrolled;
		}

		public void RegisterKeyInput(EventHandler<KeyEventArgs> onKeyPressed, EventHandler<KeyEventArgs> onKeyDown, EventHandler<KeyEventArgs> onKeyReleased)
		{
			KeyPressed += onKeyPressed;
			KeyDown += onKeyDown;
			KeyReleased += onKeyReleased;
		}

		public void UnregisterKeyInput(EventHandler<KeyEventArgs> onKeyPressed, EventHandler<KeyEventArgs> onKeyDown, EventHandler<KeyEventArgs> onKeyReleased)
		{
			KeyPressed -= onKeyPressed;
			KeyDown -= onKeyDown;
			KeyReleased -= onKeyReleased;
		}

		public void RegisterJoystickInput(EventHandler<JoystickConnectEventArgs> onJoystickConnected
			, EventHandler<JoystickConnectEventArgs> onJoystickDisconnected, EventHandler<JoystickButtonEventArgs> onJoystickButtonPressed
			, EventHandler<JoystickButtonEventArgs> onJoystickButtonReleased, EventHandler<JoystickMoveEventArgs> onJoystickMoved)
	    {
			JoystickConnected += onJoystickConnected;
			JoystickDisconnected += onJoystickDisconnected;
			JoystickButtonPressed += onJoystickButtonPressed;
			JoystickButtonReleased += onJoystickButtonReleased;
			JoystickMoved += onJoystickMoved;
		}

		public void UnregisterJoystickInput(EventHandler<JoystickConnectEventArgs> onJoystickConnected
			, EventHandler<JoystickConnectEventArgs> onJoystickDisconnected, EventHandler<JoystickButtonEventArgs> onJoystickButtonPressed
			, EventHandler<JoystickButtonEventArgs> onJoystickButtonReleased, EventHandler<JoystickMoveEventArgs> onJoystickMoved)
		{
			JoystickConnected -= onJoystickConnected;
			JoystickDisconnected -= onJoystickDisconnected;
			JoystickButtonPressed -= onJoystickButtonPressed;
			JoystickButtonReleased -= onJoystickButtonReleased;
			JoystickMoved -= onJoystickMoved;
		}

		public void RegisterTouchInput(EventHandler<TouchEventArgs> onTouchBegan, EventHandler<TouchEventArgs> onTouchEnded, EventHandler<TouchEventArgs> onTouchMoved)
		{
			TouchBegan += onTouchBegan;
			TouchEnded += onTouchEnded;
			TouchMoved += onTouchMoved;
		}

		public void UnregisterTouchInput(EventHandler<TouchEventArgs> onTouchBegan, EventHandler<TouchEventArgs> onTouchEnded, EventHandler<TouchEventArgs> onTouchMoved)
		{
			TouchBegan -= onTouchBegan;
			TouchEnded -= onTouchEnded;
			TouchMoved -= onTouchMoved;
		}

		public void RegisterInput(EventHandler<MouseButtonEventArgs> onMouseButtonPressed, EventHandler<MouseButtonEventArgs> onMouseButtonReleased
			, EventHandler<MouseMoveEventArgs> onMouseMoved, EventHandler<MouseWheelScrollEventArgs> onMouseScrolled
			, EventHandler<KeyEventArgs> onKeyPressed, EventHandler<KeyEventArgs> onKeyDown, EventHandler<KeyEventArgs> onKeyReleased
			, EventHandler<JoystickConnectEventArgs> onJoystickConnected, EventHandler<JoystickConnectEventArgs> onJoystickDisconnected
			, EventHandler<JoystickButtonEventArgs> onJoysticButtonPressed, EventHandler<JoystickButtonEventArgs> onJoysticButtonReleased
			, EventHandler<JoystickMoveEventArgs> onJoysticButtonMoved, EventHandler<TouchEventArgs> onTouchBegan
			, EventHandler<TouchEventArgs> onTouchEnded, EventHandler<TouchEventArgs> onTouchMoved)
	    {
			RegisterMouseInput(onMouseButtonPressed, onMouseButtonReleased, onMouseMoved, onMouseScrolled);
		    RegisterKeyInput(onKeyPressed, onKeyDown, onKeyReleased);
			RegisterJoystickInput(onJoystickConnected, onJoystickDisconnected, onJoysticButtonPressed, onJoysticButtonReleased, onJoysticButtonMoved);
			RegisterTouchInput(onTouchBegan, onTouchEnded, onTouchMoved);
	    }

	    public void UnregisterInput(EventHandler<MouseButtonEventArgs> onMouseButtonPressed, EventHandler<MouseButtonEventArgs> onMouseButtonReleased
			, EventHandler<MouseMoveEventArgs> onMouseMoved, EventHandler<MouseWheelScrollEventArgs> onMouseScrolled
			, EventHandler<KeyEventArgs> onKeyPressed, EventHandler<KeyEventArgs> onKeyDown, EventHandler<KeyEventArgs> onKeyReleased
			, EventHandler<JoystickConnectEventArgs> onJoystickConnected, EventHandler<JoystickConnectEventArgs> onJoystickDisconnected
			, EventHandler<JoystickButtonEventArgs> onJoysticButtonPressed, EventHandler<JoystickButtonEventArgs> onJoysticButtonReleased
			, EventHandler<JoystickMoveEventArgs> onJoysticButtonMoved, EventHandler<TouchEventArgs> onTouchBegan
			, EventHandler<TouchEventArgs> onTouchEnded, EventHandler<TouchEventArgs> onTouchMoved)
		{
			UnregisterMouseInput(onMouseButtonPressed, onMouseButtonReleased, onMouseMoved, onMouseScrolled);
			UnregisterKeyInput(onKeyPressed, onKeyDown, onKeyReleased);
			UnregisterJoystickInput(onJoystickConnected, onJoystickDisconnected, onJoysticButtonPressed, onJoysticButtonReleased, onJoysticButtonMoved);
			UnregisterTouchInput(onTouchBegan, onTouchEnded, onTouchMoved);
		}

		internal void RegisterEngineInput(ref RenderWindow engineWindow)
		{
			engineWindow.MouseButtonPressed += OnMouseButtonPressed;
			engineWindow.MouseButtonReleased += OnMouseButtonReleased;
			engineWindow.MouseMoved += OnMouseMoved;
			engineWindow.MouseWheelScrolled += OnMouseScrolled;

			engineWindow.KeyPressed += OnKeyPressed;
			engineWindow.KeyPressed += OnKeyDown;
			engineWindow.KeyReleased += OnKeyReleased;

			engineWindow.JoystickConnected += OnJoystickConnected;
			engineWindow.JoystickDisconnected += OnJoystickDisconnected;
			engineWindow.JoystickButtonPressed += OnJoystickButtonPressed;
			engineWindow.JoystickButtonReleased += OnJoystickButtonReleased;
			engineWindow.JoystickMoved += OnJoystickMoved;

			engineWindow.TouchBegan += OnTouchBegan;
			engineWindow.TouchEnded += OnTouchEnded;
			engineWindow.TouchMoved += OnTouchMoved;
		}

	    private void OnMouseButtonPressed(object sender, MouseButtonEventArgs mouseButtonEventArgs)
	    {
		    //++RegisteredMouseInputs;
			switch (mouseButtonEventArgs.Button)
			{
				case Mouse.Button.Left:
					MouseLeftPressed = true;
					break;
				case Mouse.Button.Right:
					MouseRightPressed = true;
					break;
				case Mouse.Button.Middle:
					MouseMiddlePressed = true;
					break;
				case Mouse.Button.XButton1:
					MouseXButton1Pressed = true;
					break;
				case Mouse.Button.XButton2:
					MouseXButton2Pressed = true;
					break;
				case Mouse.Button.ButtonCount:
					throw new NotImplementedException();
					break;
			}
		    MouseButtonPressed(sender, mouseButtonEventArgs);
			//Console.WriteLine("Input Event: Mouse Button Pressed: " + mouseButtonEventArgs.Button + " at X: " + mouseButtonEventArgs.X + " Y: " + mouseButtonEventArgs.Y);
		}

		private void OnMouseButtonReleased(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			//--RegisteredMouseInputs;
			switch (mouseButtonEventArgs.Button)
			{
				case Mouse.Button.Left:
					MouseLeftPressed = false;
					break;
				case Mouse.Button.Right:
					MouseRightPressed = false;
					break;
				case Mouse.Button.Middle:
					MouseMiddlePressed = false;
					break;
				case Mouse.Button.XButton1:
					MouseXButton1Pressed = false;
					break;
				case Mouse.Button.XButton2:
					MouseXButton2Pressed = false;
					break;
				case Mouse.Button.ButtonCount:
					throw new NotImplementedException();
					break;
			}
			MouseButtonReleased(sender, mouseButtonEventArgs);
			//Console.WriteLine("Input Event: Mouse Button Released: " + mouseButtonEventArgs.Button + " at X: " + mouseButtonEventArgs.X + " Y: " + mouseButtonEventArgs.Y);
		}

		private void OnMouseMoved(object sender, MouseMoveEventArgs mouseMoveEventArgs)
		{
			MouseMoved(sender, mouseMoveEventArgs);
			//Console.WriteLine("Input Event: Mouse Moved to X: " + mouseMoveEventArgs.X + " Y: " +  mouseMoveEventArgs.Y);
		}

		private void OnMouseScrolled(object sender, MouseWheelScrollEventArgs mouseWheelScrollEventArgs)
		{
			MouseScrolled(sender, mouseWheelScrollEventArgs);
			//Console.WriteLine("Input Event: Mouse Scrolled Wheel: " + mouseWheelScrollEventArgs.Wheel + " at X: " + mouseWheelScrollEventArgs.X + " Y: " + mouseWheelScrollEventArgs.Y + " by Scroll Amount: " + mouseWheelScrollEventArgs.Delta);
		}

		private void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			KeyPressed(sender, keyEventArgs);
			//Console.WriteLine("Input Event: Keyboard Key Pressed: " + keyEventArgs.Code);
		}

		public bool IsKeyPressed(Keyboard.Key key)
		{
			return Keyboard.IsKeyPressed(key);
		}

		private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
		{
			++RegisteredKeyInputs;
			KeyDown(sender, keyEventArgs);
			//Console.WriteLine("Input Event: Keyboard Key Down: " + keyEventArgs.Code);
		}

		public bool IsKeyDown(Keyboard.Key key)
	    {
		    return Keyboard.IsKeyPressed(key);
	    }

		private void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			--RegisteredKeyInputs;
			KeyReleased(sender, keyEventArgs);
			//Console.WriteLine("Input Event: Keyboard Key Released: " + keyEventArgs.Code);
		}

		private void OnJoystickConnected(object sender, JoystickConnectEventArgs joystickConnectEventArgs)
		{
			JoystickConnected(sender, joystickConnectEventArgs);
			//Console.WriteLine("Input Event: Joystick Connected: ID: " + joystickConnectEventArgs.JoystickId);
		}

		private void OnJoystickDisconnected(object sender, JoystickConnectEventArgs joystickConnectEventArgs)
		{
			JoystickDisconnected(sender, joystickConnectEventArgs);
			//Console.WriteLine("Input Event: Joystick Disconnected: ID: " + joystickConnectEventArgs.JoystickId);
		}

		private void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			JoystickButtonPressed(sender, joystickButtonEventArgs);
			//Console.WriteLine("Input Event: Joystick Button Pressed: ID: " + joystickButtonEventArgs.JoystickId + " Button: " + joystickButtonEventArgs.Button);
		}

		private void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			JoystickButtonReleased(sender, joystickButtonEventArgs);
			//Console.WriteLine("Input Event: Joystick Button Released: ID: " + joystickButtonEventArgs.JoystickId + " Button: " + joystickButtonEventArgs.Button);
		}

		private void OnJoystickMoved(object sender, JoystickMoveEventArgs joystickMoveEventArgs)
		{
			JoystickMoved(sender, joystickMoveEventArgs);
			//Console.WriteLine("Input Event: Joystick Moved: ID: " + joystickMoveEventArgs.JoystickId + " Axis: " + joystickMoveEventArgs.Axis + " to Position: " + joystickMoveEventArgs.Position);
		}

		private void OnTouchBegan(object sender, TouchEventArgs touchEventArgs)
	    {
		    TouchPressed = true;
		    TouchBegan(sender, touchEventArgs);
			//Console.WriteLine("Input Event: Touch Pressed: Finger: " + touchEventArgs.Finger + " at X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}

		private void OnTouchEnded(object sender, TouchEventArgs touchEventArgs)
		{
			TouchPressed = false;
			TouchEnded(sender, touchEventArgs);
			//Console.WriteLine("Input Event: Touch Released: Finger: " + touchEventArgs.Finger + " at X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}

		private void OnTouchMoved(object sender, TouchEventArgs touchEventArgs)
		{
			TouchMoved(sender, touchEventArgs);
			//Console.WriteLine("Input Event: Touch Moved: Finger: " + touchEventArgs.Finger + " to X: " + touchEventArgs.X + " Y: " + touchEventArgs.Y);
		}
	}
}