using System;
using SFML.Graphics;
using SFML.Window;

namespace SFML_Engine.Engine.IO
{
    public class InputManager
    {

		public bool MouseLeftPressed { get; set; }
		public bool MouseRightPressed { get; set; }
		public bool MouseMiddlePressed { get; set; }
		public bool MouseXButton1Pressed { get; set; }
		public bool MouseXButton2Pressed { get; set; }
		public bool MouseWheelVerticalMoved { get; set; }
		public bool MouseWheelHorizontalMoved { get; set; }

		public bool F1Pressed { get; set; }
        public bool F2Pressed { get; set; }
        public bool F3Pressed { get; set; }
        public bool F4Pressed { get; set; }
        public bool F5Pressed { get; set; }
        public bool F6Pressed { get; set; }
        public bool F7Pressed { get; set; }
        public bool F8Pressed { get; set; }
        public bool F9Pressed { get; set; }
        public bool F10Pressed { get; set; }
        public bool F11Pressed { get; set; }
        public bool F12Pressed { get; set; }

        public bool ZeroPressed { get; set; }
        public bool OnePressed { get; set; }
        public bool TwoPressed { get; set; }
        public bool ThreePressed { get; set; }
        public bool FourPressed { get; set; }
        public bool FivePressed { get; set; }
        public bool SixPressed { get; set; }
        public bool SevenPressed { get; set; }
        public bool EightPressed { get; set; }
        public bool NinePressed { get; set; }

        public bool LControlPressed { get; set; }
        public bool LShiftPressed { get; set; }
		public bool LSystemPressed { get; set; }
		public bool LAltPressed { get; set; }
        public bool RControlPressed { get; set; }
        public bool RShiftPressed { get; set; }
		public bool RSystemPressed { get; set; }
		public bool RAltPressed { get; set; }
        public bool CapslockPressed { get; set; }
        public bool TabPressed { get; set; }
        public bool SpacePressed { get; set; }
        public bool EscPressed { get; set; }
        public bool EnterPressed { get; set; }
        public bool BackspacePressed { get; set; }
        public bool UpPressed { get; set; }
        public bool LeftPressed { get; set; }
        public bool DownPressed { get; set; }
        public bool RightPressed { get; set; }

        public bool APressed { get; set; }
        public bool BPressed { get; set; }
        public bool CPressed { get; set; }
        public bool DPressed { get; set; }
        public bool EPressed { get; set; }
        public bool FPressed { get; set; }
        public bool GPressed { get; set; }
        public bool HPressed { get; set; }
        public bool IPressed { get; set; }
        public bool JPressed { get; set; }
        public bool KPressed { get; set; }
        public bool LPressed { get; set; }
        public bool MPressed { get; set; }
        public bool NPressed { get; set; }
        public bool OPressed { get; set; }
        public bool PPressed { get; set; }
        public bool QPressed { get; set; }
        public bool RPressed { get; set; }
        public bool SPressed { get; set; }
        public bool TPressed { get; set; }
        public bool UPressed { get; set; }
        public bool VPressed { get; set; }
        public bool WPressed { get; set; }
        public bool XPressed { get; set; }
        public bool YPressed { get; set; }
        public bool ZPressed { get; set; }

		public bool TouchPressed { get; set; }

		public EventHandler<MouseButtonEventArgs> MouseButtonPressed = delegate { };
		public EventHandler<MouseButtonEventArgs> MouseButtonReleased = delegate { };
	    public EventHandler<MouseMoveEventArgs> MouseMoved = delegate { };
	    public EventHandler<MouseWheelScrollEventArgs> MouseScrolled = delegate { };

	    public EventHandler<KeyEventArgs> KeyPressed = delegate { };
		public EventHandler<KeyEventArgs> KeyReleased = delegate { };

		public EventHandler<JoystickConnectEventArgs> JoystickConnected = delegate { };
		public EventHandler<JoystickConnectEventArgs> JoystickDisconnected = delegate { };
		public EventHandler<JoystickButtonEventArgs> JoystickButtonPressed = delegate { };
	    public EventHandler<JoystickButtonEventArgs> JoystickButtonReleased = delegate { };
		public EventHandler<JoystickMoveEventArgs> JoystickMoved = delegate { };

	    public EventHandler<TouchEventArgs> TouchBegan = delegate { };
	    public EventHandler<TouchEventArgs> TouchEnded = delegate { };
	    public EventHandler<TouchEventArgs> TouchMoved = delegate { };

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

		public void RegisterKeyInput(EventHandler<KeyEventArgs> onKeyPressed, EventHandler<KeyEventArgs> onKeyReleased)
		{
			KeyPressed += onKeyPressed;
			KeyReleased += onKeyReleased;
		}

		public void UnregisterKeyInput(EventHandler<KeyEventArgs> onKeyPressed, EventHandler<KeyEventArgs> onKeyReleased)
		{
			KeyPressed -= onKeyPressed;
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
			, EventHandler<KeyEventArgs> onKeyPressed, EventHandler<KeyEventArgs> onKeyReleased
			, EventHandler<JoystickConnectEventArgs> onJoystickConnected, EventHandler<JoystickConnectEventArgs> onJoystickDisconnected
			, EventHandler<JoystickButtonEventArgs> onJoysticButtonPressed, EventHandler<JoystickButtonEventArgs> onJoysticButtonReleased
			, EventHandler<JoystickMoveEventArgs> onJoysticButtonMoved, EventHandler<TouchEventArgs> onTouchBegan
			, EventHandler<TouchEventArgs> onTouchEnded, EventHandler<TouchEventArgs> onTouchMoved)
	    {
			RegisterMouseInput(onMouseButtonPressed, onMouseButtonReleased, onMouseMoved, onMouseScrolled);
		    RegisterKeyInput(onKeyPressed, onKeyReleased);
			RegisterJoystickInput(onJoystickConnected, onJoystickDisconnected, onJoysticButtonPressed, onJoysticButtonReleased, onJoysticButtonMoved);
			RegisterTouchInput(onTouchBegan, onTouchEnded, onTouchMoved);
	    }

	    public void UnregisterInput(EventHandler<MouseButtonEventArgs> onMouseButtonPressed, EventHandler<MouseButtonEventArgs> onMouseButtonReleased
			, EventHandler<MouseMoveEventArgs> onMouseMoved, EventHandler<MouseWheelScrollEventArgs> onMouseScrolled
			, EventHandler<KeyEventArgs> onKeyPressed, EventHandler<KeyEventArgs> onKeyReleased
			, EventHandler<JoystickConnectEventArgs> onJoystickConnected, EventHandler<JoystickConnectEventArgs> onJoystickDisconnected
			, EventHandler<JoystickButtonEventArgs> onJoysticButtonPressed, EventHandler<JoystickButtonEventArgs> onJoysticButtonReleased
			, EventHandler<JoystickMoveEventArgs> onJoysticButtonMoved, EventHandler<TouchEventArgs> onTouchBegan
			, EventHandler<TouchEventArgs> onTouchEnded, EventHandler<TouchEventArgs> onTouchMoved)
		{
			UnregisterMouseInput(onMouseButtonPressed, onMouseButtonReleased, onMouseMoved, onMouseScrolled);
			UnregisterKeyInput(onKeyPressed, onKeyReleased);
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
			switch (keyEventArgs.Code)
			{
				case Keyboard.Key.Unknown:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.A:
					APressed = true;
					break;
				case Keyboard.Key.B:
					BPressed = true;
					break;
				case Keyboard.Key.C:
					CPressed = true;
					break;
				case Keyboard.Key.D:
					DPressed = true;
					break;
				case Keyboard.Key.E:
					EPressed = true;
					break;
				case Keyboard.Key.F:
					FPressed = true;
					break;
				case Keyboard.Key.G:
					GPressed = true;
					break;
				case Keyboard.Key.H:
					HPressed = true;
					break;
				case Keyboard.Key.I:
					IPressed = true;
					break;
				case Keyboard.Key.J:
					JPressed = true;
					break;
				case Keyboard.Key.K:
					KPressed = true;
					break;
				case Keyboard.Key.L:
					LPressed = true;
					break;
				case Keyboard.Key.M:
					MPressed = true;
					break;
				case Keyboard.Key.N:
					NPressed = true;
					break;
				case Keyboard.Key.O:
					OPressed = true;
					break;
				case Keyboard.Key.P:
					PPressed = true;
					break;
				case Keyboard.Key.Q:
					QPressed = true;
					break;
				case Keyboard.Key.R:
					RPressed = true;
					break;
				case Keyboard.Key.S:
					SPressed = true;
					break;
				case Keyboard.Key.T:
					TPressed = true;
					break;
				case Keyboard.Key.U:
					UPressed = true;
					break;
				case Keyboard.Key.V:
					VPressed = true;
					break;
				case Keyboard.Key.W:
					WPressed = true;
					break;
				case Keyboard.Key.X:
					XPressed = true;
					break;
				case Keyboard.Key.Y:
					YPressed = true;
					break;
				case Keyboard.Key.Z:
					ZPressed = true;
					break;
				case Keyboard.Key.Num0:
					ZeroPressed = true;
					break;
				case Keyboard.Key.Num1:
					OnePressed = true;
					break;
				case Keyboard.Key.Num2:
					TwoPressed = true;
					break;
				case Keyboard.Key.Num3:
					ThreePressed = true;
					break;
				case Keyboard.Key.Num4:
					FourPressed = true;
					break;
				case Keyboard.Key.Num5:
					FivePressed = true;
					break;
				case Keyboard.Key.Num6:
					SixPressed = true;
					break;
				case Keyboard.Key.Num7:
					SevenPressed = true;
					break;
				case Keyboard.Key.Num8:
					EightPressed = true;
					break;
				case Keyboard.Key.Num9:
					NinePressed = true;
					break;
				case Keyboard.Key.Escape:
					EscPressed = true;
					break;
				case Keyboard.Key.LControl:
					LControlPressed = true;
					break;
				case Keyboard.Key.LShift:
					LShiftPressed = true;
					break;
				case Keyboard.Key.LAlt:
					LAltPressed = true;
					break;
				case Keyboard.Key.LSystem:
					LSystemPressed = true;
					break;
				case Keyboard.Key.RControl:
					RControlPressed = true;
					break;
				case Keyboard.Key.RShift:
					RShiftPressed = true;
					break;
				case Keyboard.Key.RAlt:
					RAltPressed = true;
					break;
				case Keyboard.Key.RSystem:
					RSystemPressed = true;
					break;
				case Keyboard.Key.Menu:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.LBracket:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.RBracket:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.SemiColon:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Comma:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Period:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Quote:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Slash:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.BackSlash:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Tilde:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Equal:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Dash:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Space:
					SpacePressed = true;
					break;
				case Keyboard.Key.Return:
					EnterPressed = true;
					break;
				case Keyboard.Key.BackSpace:
					BackspacePressed = true;
					break;
				case Keyboard.Key.Tab:
					TabPressed = true;
					break;
				case Keyboard.Key.PageUp:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.PageDown:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.End:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Home:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Insert:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Delete:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Add:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Subtract:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Multiply:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Divide:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Left:
					LeftPressed = true;
					break;
				case Keyboard.Key.Right:
					RightPressed = true;
					break;
				case Keyboard.Key.Up:
					UpPressed = true;
					break;
				case Keyboard.Key.Down:
					DownPressed = true;
					break;
				case Keyboard.Key.Numpad0:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad1:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad2:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad3:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad4:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad5:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad6:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad7:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad8:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad9:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.F1:
					F1Pressed = true;
					break;
				case Keyboard.Key.F2:
					F2Pressed = true;
					break;
				case Keyboard.Key.F3:
					F3Pressed = true;
					break;
				case Keyboard.Key.F4:
					F4Pressed = true;
					break;
				case Keyboard.Key.F5:
					F5Pressed = true;
					break;
				case Keyboard.Key.F6:
					F6Pressed = true;
					break;
				case Keyboard.Key.F7:
					F7Pressed = true;
					break;
				case Keyboard.Key.F8:
					F8Pressed = true;
					break;
				case Keyboard.Key.F9:
					F9Pressed = true;
					break;
				case Keyboard.Key.F10:
					F10Pressed = true;
					break;
				case Keyboard.Key.F11:
					F11Pressed = true;
					break;
				case Keyboard.Key.F12:
					F12Pressed = true;
					break;
				case Keyboard.Key.F13:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.F14:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.F15:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Pause:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.KeyCount:
					throw new NotImplementedException();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			KeyPressed(sender, keyEventArgs);
			//Console.WriteLine("Input Event: Keyboard Key Pressed: " + keyEventArgs.Code);
		}

		private void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			switch (keyEventArgs.Code)
			{
				case Keyboard.Key.Unknown:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.A:
					APressed = false;
					break;
				case Keyboard.Key.B:
					BPressed = false;
					break;
				case Keyboard.Key.C:
					CPressed = false;
					break;
				case Keyboard.Key.D:
					DPressed = false;
					break;
				case Keyboard.Key.E:
					EPressed = false;
					break;
				case Keyboard.Key.F:
					FPressed = false;
					break;
				case Keyboard.Key.G:
					GPressed = false;
					break;
				case Keyboard.Key.H:
					HPressed = false;
					break;
				case Keyboard.Key.I:
					IPressed = false;
					break;
				case Keyboard.Key.J:
					JPressed = false;
					break;
				case Keyboard.Key.K:
					KPressed = false;
					break;
				case Keyboard.Key.L:
					LPressed = false;
					break;
				case Keyboard.Key.M:
					MPressed = false;
					break;
				case Keyboard.Key.N:
					NPressed = false;
					break;
				case Keyboard.Key.O:
					OPressed = false;
					break;
				case Keyboard.Key.P:
					PPressed = false;
					break;
				case Keyboard.Key.Q:
					QPressed = false;
					break;
				case Keyboard.Key.R:
					RPressed = false;
					break;
				case Keyboard.Key.S:
					SPressed = false;
					break;
				case Keyboard.Key.T:
					TPressed = false;
					break;
				case Keyboard.Key.U:
					UPressed = false;
					break;
				case Keyboard.Key.V:
					VPressed = false;
					break;
				case Keyboard.Key.W:
					WPressed = false;
					break;
				case Keyboard.Key.X:
					XPressed = false;
					break;
				case Keyboard.Key.Y:
					YPressed = false;
					break;
				case Keyboard.Key.Z:
					ZPressed = false;
					break;
				case Keyboard.Key.Num0:
					ZeroPressed = false;
					break;
				case Keyboard.Key.Num1:
					OnePressed = false;
					break;
				case Keyboard.Key.Num2:
					TwoPressed = false;
					break;
				case Keyboard.Key.Num3:
					ThreePressed = false;
					break;
				case Keyboard.Key.Num4:
					FourPressed = false;
					break;
				case Keyboard.Key.Num5:
					FivePressed = false;
					break;
				case Keyboard.Key.Num6:
					SixPressed = false;
					break;
				case Keyboard.Key.Num7:
					SevenPressed = false;
					break;
				case Keyboard.Key.Num8:
					EightPressed = false;
					break;
				case Keyboard.Key.Num9:
					NinePressed = false;
					break;
				case Keyboard.Key.Escape:
					EscPressed = false;
					break;
				case Keyboard.Key.LControl:
					LControlPressed = false;
					break;
				case Keyboard.Key.LShift:
					LShiftPressed = false;
					break;
				case Keyboard.Key.LAlt:
					LAltPressed = false;
					break;
				case Keyboard.Key.LSystem:
					LSystemPressed = false;
					break;
				case Keyboard.Key.RControl:
					RControlPressed = false;
					break;
				case Keyboard.Key.RShift:
					RShiftPressed = false;
					break;
				case Keyboard.Key.RAlt:
					RAltPressed = false;
					break;
				case Keyboard.Key.RSystem:
					RSystemPressed = false;
					break;
				case Keyboard.Key.Menu:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.LBracket:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.RBracket:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.SemiColon:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Comma:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Period:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Quote:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Slash:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.BackSlash:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Tilde:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Equal:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Dash:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Space:
					SpacePressed = false;
					break;
				case Keyboard.Key.Return:
					EnterPressed = false;
					break;
				case Keyboard.Key.BackSpace:
					BackspacePressed = false;
					break;
				case Keyboard.Key.Tab:
					TabPressed = false;
					break;
				case Keyboard.Key.PageUp:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.PageDown:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.End:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Home:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Insert:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Delete:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Add:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Subtract:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Multiply:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Divide:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Left:
					LeftPressed = false;
					break;
				case Keyboard.Key.Right:
					RightPressed = false;
					break;
				case Keyboard.Key.Up:
					UpPressed = false;
					break;
				case Keyboard.Key.Down:
					DownPressed = false;
					break;
				case Keyboard.Key.Numpad0:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad1:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad2:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad3:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad4:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad5:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad6:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad7:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad8:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Numpad9:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.F1:
					F1Pressed = false;
					break;
				case Keyboard.Key.F2:
					F2Pressed = false;
					break;
				case Keyboard.Key.F3:
					F3Pressed = false;
					break;
				case Keyboard.Key.F4:
					F4Pressed = false;
					break;
				case Keyboard.Key.F5:
					F5Pressed = false;
					break;
				case Keyboard.Key.F6:
					F6Pressed = false;
					break;
				case Keyboard.Key.F7:
					F7Pressed = false;
					break;
				case Keyboard.Key.F8:
					F8Pressed = false;
					break;
				case Keyboard.Key.F9:
					F9Pressed = false;
					break;
				case Keyboard.Key.F10:
					F10Pressed = false;
					break;
				case Keyboard.Key.F11:
					F11Pressed = false;
					break;
				case Keyboard.Key.F12:
					F12Pressed = false;
					break;
				case Keyboard.Key.F13:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.F14:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.F15:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.Pause:
					throw new NotImplementedException();
					break;
				case Keyboard.Key.KeyCount:
					throw new NotImplementedException();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
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