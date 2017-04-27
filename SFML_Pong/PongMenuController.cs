using SFML.Window;
using SFML_Engine.Engine;

namespace SFML_Pong
{
	public class PongMenuController : PlayerController
	{
		public override void RegisterInput(Engine engine)
		{
			Input = engine.InputManager;

			Input.RegisterKeyInput(OnKeyPressed, OnKeyReleased);

			Input.RegisterJoystickInput(null, null, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved);
		}

		public override void UnregisterInput(Engine engine)
		{
			Input = engine.InputManager;

			Input.UnregisterKeyInput(OnKeyPressed, OnKeyReleased);

			Input.UnregisterJoystickInput(null, null, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved);
		}

		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyPressed(sender, keyEventArgs);
		}

		protected override void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyReleased(sender, keyEventArgs);
		}

		protected override void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			base.OnJoystickButtonPressed(sender, joystickButtonEventArgs);
		}

		protected override void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			base.OnJoystickButtonReleased(sender, joystickButtonEventArgs);
		}

		protected override void OnJoystickMoved(object sender, JoystickMoveEventArgs joystickMoveEventArgs)
		{
			base.OnJoystickMoved(sender, joystickMoveEventArgs);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}