using System.Numerics;

namespace ZEngine.Engine.IO.UserInput
{
    public interface IBaseControllable
    {

    }

    public interface IKeyboardControllable : IBaseControllable
    {
        //TODO: wait for Silk.NET implementation
        //void OnKeyPressed(object sender, Key key);

        void OnKeyDown(object sender, Key key, int code);

        void OnKeyReleased(object sender, Key key, int code);

        void OnKeyboardConnected();

        void OnKeyboardDisconnected();
    }

    public interface IMouseControllable : IBaseControllable
    {
        void OnMouseButtonPressed(object sender, MouseButton button);

        void OnMouseButtonReleased(object sender, MouseButton button);

        void OnMouseButtonClicked(object sender, MouseButton button, Vector2 position);

        void OnMouseButtonDoubleClicked(object sender, MouseButton button, Vector2 position);

        void OnMouseMoved(object sender, Vector2 coords);

        void OnMouseScrolled(object sender, ScrollWheel scrollPosition);

        void OnMouseConnected();

        void OnMouseDisconnected();
    }

    public interface IJoystickControllable : IBaseControllable
    {
        void OnJoystickButtonPressed(object sender, Button button);

        void OnJoystickButtonReleased(object sender, Button button);

        void OnJoystickMoved(object sender, Axis axisArgs);

        void OnJoystickHatMoved(object sender, Hat hatArgs);

        void OnJoystickConnected();

        void OnJoystickDisconnected();
    }

    public interface IGamepadControllable : IBaseControllable
    {
        void OnGamepadButtonPressed(object sender, Button button);

        void OnGamepadButtonReleased(object sender, Button button);

        void OnGamepadThumbstickMoved(object sender, Thumbstick stick);

        void OnGamepadTriggerMoved(object sender, Trigger trigger);

        void OnGamepadConnected();

        void OnGamepadDisconnected();
    }
}