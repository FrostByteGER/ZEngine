using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Silk.NET.Input;
using Silk.NET.Input.Common;
using ZEngine.Engine.Game;
using ZEngine.Engine.Messaging;
using ZEngine.Engine.Services;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.IO
{
    public interface IInputManager : IEngineService
    {
        void RegisterInputReceiver(IInputReceiver receiver);
        void UnregisterInputReceiver(IInputReceiver receiver);
    }

    public class InputManager : ITickable, IInputManager
    {
        private IInputContext InputContext { get; set; }

        private readonly HashSet<IInputReceiver> _receivers = new HashSet<IInputReceiver>();

        private readonly OrderedDictionary<IInputDevice, IInputReceiver> _devices = new OrderedDictionary<IInputDevice, IInputReceiver>();
        private readonly IMessageBus _bus;

        public bool CanTick { get; set; } = true;

        public InputManager(IMessageBus bus)
        {
            _bus = bus;
            _bus.Subscribe<EngineWindowLoadedMessage>(OnWindowLoaded);
        }

        private void OnWindowLoaded(IMessage msg)
        {
            InputContext = ((Core.Engine) msg.Sender).Window.CreateInput();
            InputContext.ConnectionChanged += OnInputDeviceConnectionChanged;

            foreach (var mouse in InputContext.Mice)
                _devices.Add(mouse, null);

            foreach (var keyboard in InputContext.Keyboards)
                _devices.Add(keyboard, null);

            foreach (var gamepad in InputContext.Gamepads)
                _devices.Add(gamepad, null);

            foreach (var joystick in InputContext.Joysticks)
                _devices.Add(joystick, null);

            //TODO: Implement other device support when Silk.NET adds support for them(Currently the list is always 0)
            //InputContext.OtherDevices
        }

        public void Initialize()
        {

        }

        public void Deinitialize()
        {
            InputContext.ConnectionChanged -= OnInputDeviceConnectionChanged;

            foreach (var mouse in InputContext.Mice)
                _devices.Remove(mouse);

            foreach (var keyboard in InputContext.Keyboards)
                _devices.Remove(keyboard);

            foreach (var gamepad in InputContext.Gamepads)
                _devices.Remove(gamepad);

            foreach (var joystick in InputContext.Joysticks)
                _devices.Remove(joystick);

            //TODO: Implement other device support when Silk.NET adds support for them(Currently the list is always 0)
            //InputContext.OtherDevices
        }

        private void OnInputDeviceConnectionChanged(IInputDevice device, bool status)
        {
            if (!_devices.TryGetValue(device, out var receiver))
            {
                //TODO: Log, this should not happen because all devices get added at the start!
                return;
            }

            // Simply do nothing
            if (receiver == null)
                return;

            switch (device)
            {
                case IMouse _:
                    if(status)
                        receiver.OnMouseConnected();
                    else
                        receiver.OnMouseDisconnected();
                    break;
                case IKeyboard _:
                    if (status)
                        receiver.OnKeyboardConnected();
                    else
                        receiver.OnKeyboardDisconnected();
                    break;
                case IGamepad _:
                    if (status)
                        receiver.OnGamepadConnected();
                    else
                        receiver.OnGamepadDisconnected();
                    break;
                case IJoystick _:
                    if (status)
                        receiver.OnJoystickConnected();
                    else
                        receiver.OnJoystickDisconnected();
                    break;
                default:
                    //TODO: LOG
                    break;
            }
        }

        public void RegisterInputReceiver(IInputReceiver receiver)
        {
            if (!_receivers.Add(receiver))
                throw new Exception("InputReceiver already registered for input!");

        }

        public void UnregisterInputReceiver(IInputReceiver receiver)
        {
            if(!_receivers.Remove(receiver))
                throw new Exception("InputReceiver to remove is not registered for input!");
        }

        public bool RegisterForInputDevice<T>(IInputReceiver receiver) where T : IInputDevice
        {
            var device = GetAvailableDevice<T>();
            if (device == null)
                return false;

            _devices[device] = receiver;
            switch (device)
            {
                case IMouse mouse:
                    mouse.MouseDown += receiver.OnMouseButtonPressed;
                    mouse.MouseUp += receiver.OnMouseButtonReleased;
                    mouse.Click += receiver.OnMouseButtonClicked;
                    mouse.DoubleClick += receiver.OnMouseButtonDoubleClicked;
                    mouse.MouseMove += receiver.OnMouseMoved;
                    break;
                case IKeyboard keyboard:
                    // Wrap the delegates as we dont want the scancode to be passed down
                    keyboard.KeyDown += receiver.OnKeyDown;
                    keyboard.KeyUp += receiver.OnKeyReleased;
                    break;
                case IGamepad gamepad:
                    gamepad.ButtonDown += receiver.OnGamepadButtonPressed;
                    gamepad.ButtonUp += receiver.OnGamepadButtonReleased;
                    gamepad.ThumbstickMoved += receiver.OnGamepadThumbstickMoved;
                    gamepad.TriggerMoved += receiver.OnGamepadTriggerMoved;
                    break;
                case IJoystick joystick:
                    joystick.ButtonDown += receiver.OnJoystickButtonPressed;
                    joystick.ButtonUp += receiver.OnJoystickButtonReleased;
                    joystick.AxisMoved += receiver.OnJoystickMoved;
                    joystick.HatMoved += receiver.OnJoystickHatMoved;
                    break;
                default:
                    //TODO: LOG
                    return false;
            }

            return true;
        }

        public bool UnregisterFromInputDevice<T>(IInputReceiver receiver) where T : IInputDevice
        {
            var device = GetDeviceForInputReceiver<T>(receiver);
            if (device == null)
                return false;

            try
            {
                UnregisterInput(receiver, device);
            }
            catch (ArgumentException ae)
            {
                //TODO: LOG!
                return false;
            }

            return true;
        }

        private void UnregisterInput(IControllable receiver, IInputDevice device)
        {
            switch (device)
            {
                case IMouse mouse:
                    mouse.MouseDown -= receiver.OnMouseButtonPressed;
                    mouse.MouseUp -= receiver.OnMouseButtonReleased;
                    mouse.Click -= receiver.OnMouseButtonClicked;
                    mouse.DoubleClick -= receiver.OnMouseButtonDoubleClicked;
                    mouse.MouseMove -= receiver.OnMouseMoved;
                    break;
                case IKeyboard keyboard:
                    // Wrap the delegates as we dont want the scancode to be passed down
                    keyboard.KeyDown -= receiver.OnKeyDown;
                    keyboard.KeyUp -= receiver.OnKeyReleased;
                    break;
                case IGamepad gamepad:
                    gamepad.ButtonDown -= receiver.OnGamepadButtonPressed;
                    gamepad.ButtonUp -= receiver.OnGamepadButtonReleased;
                    gamepad.ThumbstickMoved -= receiver.OnGamepadThumbstickMoved;
                    gamepad.TriggerMoved -= receiver.OnGamepadTriggerMoved;
                    break;
                case IJoystick joystick:
                    joystick.ButtonDown -= receiver.OnJoystickButtonPressed;
                    joystick.ButtonUp -= receiver.OnJoystickButtonReleased;
                    joystick.AxisMoved -= receiver.OnJoystickMoved;
                    joystick.HatMoved -= receiver.OnJoystickHatMoved;
                    break;
                default:
                    throw new ArgumentException($"Removing input from InputDevice {device.GetType()} is not supported");
            }

            _devices[device] = null;

            UnregisterFromAllInputDevices(null);
        }

        public bool UnregisterFromAllInputDevices([NotNull]IInputReceiver receiver)
        {
            var devices = GetAllDevicesForInputReceiver(receiver);
            var inputDevices = devices as IInputDevice[] ?? devices.ToArray();

            if (!inputDevices.Any())
            {
                //TODO: Log Warning
                return false;
            }

            // ArgumentException can't happen here as we remove only known devices
            foreach (var device in inputDevices)
                UnregisterInput(receiver, device);

            return true;
        }

        /// <summary>
        /// Returns the first available, connected device.
        /// </summary>
        /// <typeparam name="T">Type of the InputDevice, e.g. Mouse</typeparam>
        /// <returns>The InputDevice or null if none available</returns>
        private T GetAvailableDevice<T>() where T : IInputDevice
        {
            return (T)_devices.FirstOrDefault(e => e.Key is T && e.Key.IsConnected && e.Value == null).Key;
        }

        /// <summary>
        /// Returns the first available, connected device.
        /// </summary>
        /// <typeparam name="T">Type of the InputDevice, e.g. Mouse</typeparam>
        /// <returns>The InputDevice or null if none available</returns>
        private T GetDeviceForInputReceiver<T>(IBaseControllable receiver) where T : IInputDevice
        {
            return (T)_devices.FirstOrDefault(e => e.Key is T && e.Value == receiver).Key;
        }

        private IEnumerable<IInputDevice> GetAllDevicesForInputReceiver(IBaseControllable receiver)
        {
            return _devices.Where(e => e.Value == receiver).Select(f => f.Key);
        }



    }
}