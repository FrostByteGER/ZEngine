using System.Drawing;

namespace ZEngine.Engine.IO.UserInput.Silk
{
    public class SilkInputDelegateWrapper
    {
        public IInputReceiver Receiver { get; }

        public SilkInputDelegateWrapper(IInputReceiver receiver)
        {
            Receiver = receiver;
        }


        //TODO: wait for Silk.NET implementation
        //void OnKeyPressed(object sender, Key key);

        internal void OnKeyDown(object sender, global::Silk.NET.Input.Common.Key key, int code)
        {
            Receiver.OnKeyDown(sender, (Key)(int)key, code);
        }

        internal void OnKeyReleased(object sender, global::Silk.NET.Input.Common.Key key, int code)
        {
            Receiver.OnKeyReleased(sender, (Key)(int)key, code);
        }

        internal void OnKeyboardConnected()
        {
            Receiver.OnKeyboardConnected();
        }

        internal void OnKeyboardDisconnected()
        {
            Receiver.OnKeyboardDisconnected();
        }

        internal void OnMouseButtonPressed(object sender, global::Silk.NET.Input.Common.MouseButton button)
        {
            Receiver.OnMouseButtonPressed(sender, (MouseButton)(int)button);
        }

        internal void OnMouseButtonReleased(object sender, global::Silk.NET.Input.Common.MouseButton button)
        {
            Receiver.OnMouseButtonReleased(sender, (MouseButton)(int)button);
        }

        internal void OnMouseButtonClicked(object sender, global::Silk.NET.Input.Common.MouseButton button)
        {
            Receiver.OnMouseButtonClicked(sender, (MouseButton)(int)button);
        }

        internal void OnMouseButtonDoubleClicked(object sender, global::Silk.NET.Input.Common.MouseButton button)
        {
            Receiver.OnMouseButtonDoubleClicked(sender, (MouseButton)(int)button);
        }

        internal void OnMouseMoved(object sender, PointF coords)
        {
            Receiver.OnMouseMoved(sender, coords);
        }

        internal void OnMouseScrolled(object sender, global::Silk.NET.Input.Common.ScrollWheel scrollArgs)
        {
            Receiver.OnMouseScrolled(sender, new ScrollWheel(scrollArgs.X, scrollArgs.Y));
        }

        internal void OnMouseConnected()
        {
            Receiver.OnMouseConnected();
        }

        internal void OnMouseDisconnected()
        {
            Receiver.OnMouseDisconnected();
        }

        internal void OnJoystickButtonPressed(object sender, global::Silk.NET.Input.Common.Button button)
        {
            Receiver.OnJoystickButtonPressed(sender, new Button((ButtonType)(int)button.Name, button.Index, button.Pressed));
        }

        internal void OnJoystickButtonReleased(object sender, global::Silk.NET.Input.Common.Button button)
        {
            Receiver.OnJoystickButtonReleased(sender, new Button((ButtonType)(int)button.Name, button.Index, button.Pressed));
        }

        internal void OnJoystickMoved(object sender, global::Silk.NET.Input.Common.Axis axisArgs)
        {
            Receiver.OnJoystickMoved(sender, new Axis(axisArgs.Index, axisArgs.Position));
        }

        internal void OnJoystickHatMoved(object sender, global::Silk.NET.Input.Common.Hat hatArgs)
        {
            Receiver.OnJoystickHatMoved(sender, new Hat(hatArgs.Index, (Position2D)(int)hatArgs.Position));
        }

        internal void OnJoystickConnected()
        {
            Receiver.OnJoystickConnected();
        }

        internal void OnJoystickDisconnected()
        {
            Receiver.OnJoystickDisconnected();
        }

        internal void OnGamepadButtonPressed(object sender, global::Silk.NET.Input.Common.Button button)
        {
            Receiver.OnGamepadButtonPressed(sender, new Button((ButtonType)(int)button.Name, button.Index, button.Pressed));
        }

        internal void OnGamepadButtonReleased(object sender, global::Silk.NET.Input.Common.Button button)
        {
            Receiver.OnGamepadButtonReleased(sender, new Button((ButtonType)(int)button.Name, button.Index, button.Pressed));
        }

        internal void OnGamepadThumbstickMoved(object sender, global::Silk.NET.Input.Common.Thumbstick stick)
        {
            Receiver.OnGamepadThumbstickMoved(sender, new Thumbstick(stick.Index, stick.X, stick.Y));
        }

        internal void OnGamepadTriggerMoved(object sender, global::Silk.NET.Input.Common.Trigger trigger)
        {
            Receiver.OnGamepadTriggerMoved(sender, new Trigger(trigger.Index, trigger.Position));
        }

        internal void OnGamepadConnected()
        {
            Receiver.OnGamepadConnected();
        }

        internal void OnGamepadDisconnected()
        {
            Receiver.OnGamepadDisconnected();
        }
    }
}