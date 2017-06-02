using SFML.Window;

namespace SFML_Engine.Engine.IO
{
	public interface IControllable
	{
		void RegisterInput();

		void UnregisterInput();

		void OnMouseButtonPressed(object sender, MouseButtonEventArgs mouseButtonEventArgs);

		void OnMouseButtonReleased(object sender, MouseButtonEventArgs mouseButtonEventArgs);

		void OnMouseMoved(object sender, MouseMoveEventArgs mouseMoveEventArgs);

		void OnMouseScrolled(object sender, MouseWheelScrollEventArgs mouseWheelScrollEventArgs);

		void OnKeyPressed(object sender, KeyEventArgs keyEventArgs);

		void OnKeyDown(object sender, KeyEventArgs keyEventArgs);

		void OnKeyReleased(object sender, KeyEventArgs keyEventArgs);

		void OnJoystickConnected(object sender, JoystickConnectEventArgs joystickConnectEventArgs);

		void OnJoystickDisconnected(object sender, JoystickConnectEventArgs joystickConnectEventArgs);

		void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs joystickButtonEventArgs);

		void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs joystickButtonEventArgs);

		void OnJoystickMoved(object sender, JoystickMoveEventArgs joystickMoveEventArgs);

		void OnTouchBegan(object sender, TouchEventArgs touchEventArgs);

		void OnTouchEnded(object sender, TouchEventArgs touchEventArgs);

		void OnTouchMoved(object sender, TouchEventArgs touchEventArgs);
	}
}