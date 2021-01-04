using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Silk.NET.Input;
using ZEngine.Engine.Core.Messages;
using ZEngine.Engine.Messaging;
using ZEngine.Engine.Rendering.Window;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.IO.UserInput.Silk
{
    public class SilkInputManager : IInputManager
    {
        private IInputContext InputContext { get; set; }

        private static readonly Dictionary<Type, Type> DeviceMappings = new()
        {
            {typeof(IMouse), typeof(global::Silk.NET.Input.IMouse)},
            {typeof(IKeyboard), typeof(global::Silk.NET.Input.IKeyboard)},
            {typeof(IGamepad), typeof(global::Silk.NET.Input.IGamepad)},
            {typeof(IJoystick), typeof(global::Silk.NET.Input.IJoystick)}
        };
        private OrderedDictionary<global::Silk.NET.Input.IInputDevice, SilkInputDelegateWrapper> Devices { get; } = new();
        private HashSet<IInputReceiver> Receivers { get; } = new();
        private IMessageBus Bus { get; }
        private IWindowManager WindowManager { get; } 

        public SilkInputManager([NotNull]IMessageBus bus, [NotNull] IWindowManager windowManager)
        {
            WindowManager = windowManager;
            Bus = bus;
            Bus.Subscribe<EngineWindowLoadedMessage>(OnWindowLoaded);
        }

        private void OnWindowLoaded(EngineWindowLoadedMessage msg)
        {
            InputContext = WindowManager.Window.CreateInput();
            InputContext.ConnectionChanged += OnInputDeviceConnectionChanged;

            foreach (var mouse in InputContext.Mice)
                Devices.Add(mouse, null);

            foreach (var keyboard in InputContext.Keyboards)
                Devices.Add(keyboard, null);

            foreach (var gamepad in InputContext.Gamepads)
                Devices.Add(gamepad, null);

            foreach (var joystick in InputContext.Joysticks)
                Devices.Add(joystick, null);

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
                Devices.Remove(mouse);

            foreach (var keyboard in InputContext.Keyboards)
                Devices.Remove(keyboard);

            foreach (var gamepad in InputContext.Gamepads)
                Devices.Remove(gamepad);

            foreach (var joystick in InputContext.Joysticks)
                Devices.Remove(joystick);

            //TODO: Implement other device support when Silk.NET adds support for them(Currently the list is always 0)
            //InputContext.OtherDevices
        }

        private void OnInputDeviceConnectionChanged(global::Silk.NET.Input.IInputDevice device, bool status)
        {
            if (!Devices.TryGetValue(device, out var receiver))
            {
                // this should not happen because all devices get added at the start!
                Debug.LogError("Failed to get receiver for device " + device.Name, DebugLogCategories.Engine);
                return;
            }

            // Simply do nothing
            if (receiver == null)
                return;

            switch (device)
            {
                case global::Silk.NET.Input.IMouse _:
                    if(status)
                        receiver.OnMouseConnected();
                    else
                        receiver.OnMouseDisconnected();
                    break;
                case global::Silk.NET.Input.IKeyboard _:
                    if (status)
                        receiver.OnKeyboardConnected();
                    else
                        receiver.OnKeyboardDisconnected();
                    break;
                case global::Silk.NET.Input.IGamepad _:
                    if (status)
                        receiver.OnGamepadConnected();
                    else
                        receiver.OnGamepadDisconnected();
                    break;
                case global::Silk.NET.Input.IJoystick _:
                    if (status)
                        receiver.OnJoystickConnected();
                    else
                        receiver.OnJoystickDisconnected();
                    break;
                default:
                    Debug.LogWarning("Device " + device.Name + " is not supported!", DebugLogCategories.Engine);
                    break;
            }
        }

        public bool RegisterForAllInputDevices([NotNull]IInputReceiver receiver)
        {
            var devices = GetAvailableDevices().ToArray();
            if (!devices.Any())
                return false;

            foreach (var device in devices)
            {
                try
                {
                    RegisterInput(receiver, device);
                }
                catch (ArgumentException ae)
                {
                    Debug.LogWarning(ae.Message, DebugLogCategories.Engine);
                }
            }

            return true;
        }

        public bool RegisterForInputDevice<T>([NotNull]IInputReceiver receiver) where T : IInputDevice
        {
            var mapping = MapInputDeviceToSilkInputDevice(typeof(T));
            var device = GetAvailableDevice(mapping);
            if (device == null)
                return false;
            RegisterInput(receiver, device);
            return true;
        }

        private void RegisterInput(IInputReceiver receiver, global::Silk.NET.Input.IInputDevice device)
        {
            var wrapper = new SilkInputDelegateWrapper(receiver);
            Devices[device] = wrapper;
            switch (device)
            {
                case global::Silk.NET.Input.IMouse mouse:
                    mouse.MouseDown += wrapper.OnMouseButtonPressed;
                    mouse.MouseUp += wrapper.OnMouseButtonReleased;
                    mouse.Click += wrapper.OnMouseButtonClicked;
                    mouse.DoubleClick += wrapper.OnMouseButtonDoubleClicked;
                    mouse.MouseMove += wrapper.OnMouseMoved;
                    break;
                case global::Silk.NET.Input.IKeyboard keyboard:
                    keyboard.KeyDown += wrapper.OnKeyDown;
                    keyboard.KeyUp += wrapper.OnKeyReleased;
                    break;
                case global::Silk.NET.Input.IGamepad gamepad:
                    gamepad.ButtonDown += wrapper.OnGamepadButtonPressed;
                    gamepad.ButtonUp += wrapper.OnGamepadButtonReleased;
                    gamepad.ThumbstickMoved += wrapper.OnGamepadThumbstickMoved;
                    gamepad.TriggerMoved += wrapper.OnGamepadTriggerMoved;
                    break;
                case global::Silk.NET.Input.IJoystick joystick:
                    joystick.ButtonDown += wrapper.OnJoystickButtonPressed;
                    joystick.ButtonUp += wrapper.OnJoystickButtonReleased;
                    joystick.AxisMoved += wrapper.OnJoystickMoved;
                    joystick.HatMoved += wrapper.OnJoystickHatMoved;
                    break;
                default:
                    throw new ArgumentException($"Removing input from InputDevice {device.GetType()} is not supported");
            }
        }

        public bool UnregisterFromInputDevice<T>([NotNull]IInputReceiver receiver) where T : IInputDevice
        {
            var mapping = MapInputDeviceToSilkInputDevice(typeof(T));
            var device = GetDeviceForInputReceiver(mapping, receiver);
            if (device == null)
                return false;

            if (!Devices.TryGetValue(device, out var wrapper))
            {
                // this should not happen because all devices get added at the start!
                Debug.LogError("Failed to get receiver for device " + device.Name, DebugLogCategories.Engine);
                return false;
            }

            try
            {
                UnregisterInput(wrapper, device);
            }
            catch (ArgumentException ae)
            {
                Debug.LogWarning(ae.Message, DebugLogCategories.Engine);
                return false;
            }

            return true;
        }

        private void UnregisterInput(SilkInputDelegateWrapper receiverWrapper, global::Silk.NET.Input.IInputDevice device)
        {
            switch (device)
            {
                case global::Silk.NET.Input.IMouse mouse:
                    mouse.MouseDown -= receiverWrapper.OnMouseButtonPressed;
                    mouse.MouseUp -= receiverWrapper.OnMouseButtonReleased;
                    mouse.Click -= receiverWrapper.OnMouseButtonClicked;
                    mouse.DoubleClick -= receiverWrapper.OnMouseButtonDoubleClicked;
                    mouse.MouseMove -= receiverWrapper.OnMouseMoved;
                    break;
                case global::Silk.NET.Input.IKeyboard keyboard:
                    // Wrap the delegates as we dont want the scancode to be passed down
                    keyboard.KeyDown -= receiverWrapper.OnKeyDown;
                    keyboard.KeyUp -= receiverWrapper.OnKeyReleased;
                    break;
                case global::Silk.NET.Input.IGamepad gamepad:
                    gamepad.ButtonDown -= receiverWrapper.OnGamepadButtonPressed;
                    gamepad.ButtonUp -= receiverWrapper.OnGamepadButtonReleased;
                    gamepad.ThumbstickMoved -= receiverWrapper.OnGamepadThumbstickMoved;
                    gamepad.TriggerMoved -= receiverWrapper.OnGamepadTriggerMoved;
                    break;
                case global::Silk.NET.Input.IJoystick joystick:
                    joystick.ButtonDown -= receiverWrapper.OnJoystickButtonPressed;
                    joystick.ButtonUp -= receiverWrapper.OnJoystickButtonReleased;
                    joystick.AxisMoved -= receiverWrapper.OnJoystickMoved;
                    joystick.HatMoved -= receiverWrapper.OnJoystickHatMoved;
                    break;
                default:
                    throw new ArgumentException($"Removing input from InputDevice {device.GetType()} is not supported");
            }

            Devices[device] = null;
        }

        public bool UnregisterFromAllInputDevices([NotNull]IInputReceiver receiver)
        {
            var devices = GetAllDevicesForInputReceiver(receiver);
            var inputDevices = devices as global::Silk.NET.Input.IInputDevice[] ?? devices.ToArray();

            if (!inputDevices.Any())
            {
                Debug.LogWarning("No devices to unregister!", DebugLogCategories.Engine);
                return false;
            }

            // ArgumentException can'type happen here as we remove only known devices
            foreach (var device in inputDevices)
            {
                if (!Devices.TryGetValue(device, out var wrapper))
                {
                    // this should not happen because all devices get added at the start!
                    Debug.LogError("Failed to get receiver for device " + device.Name, DebugLogCategories.Engine);
                    continue;
                }

                UnregisterInput(wrapper, device);
            }

            return true;
        }

        /// <summary>
        /// Returns the first available, connected device.
        /// </summary>
        /// <param name="deviceType">Type of the InputDevice, e.g. Mouse</param>
        /// <returns>The InputDevice or null if none available</returns>
        private global::Silk.NET.Input.IInputDevice GetAvailableDevice(Type deviceType)
        {
            return Devices.FirstOrDefault(e => e.Key.GetType() == deviceType && e.Key.IsConnected && e.Value == null).Key;
        }

        /// <summary>
        /// Returns the first available, connected device.
        /// </summary>
        /// <typeparam name="T">Type of the InputDevice, e.g. Mouse</typeparam>
        /// <returns>The InputDevice or null if none available</returns>
        private IEnumerable<global::Silk.NET.Input.IInputDevice> GetAvailableDevices()
        {
            return Devices.Where(e => e.Key.IsConnected && e.Value == null).Select(f => f.Key);
        }

        /// <summary>
        /// Returns the first available, connected device.
        /// </summary>
        /// <param name="deviceType">Type of the InputDevice, e.g. IMouse</param>
        /// <returns>The InputDevice or null if none available</returns>
        private global::Silk.NET.Input.IInputDevice GetDeviceForInputReceiver(Type deviceType, IBaseControllable receiver)
        {
            return Devices.FirstOrDefault(e => e.Key.GetType() == deviceType && e.Value.Receiver == receiver).Key;
        }

        private IEnumerable<global::Silk.NET.Input.IInputDevice> GetAllDevicesForInputReceiver(IBaseControllable receiver)
        {
            return Devices.Where(e => e.Value.Receiver == receiver).Select(f => f.Key);
        }

        private static Type MapInputDeviceToSilkInputDevice(Type type)
        {
            return DeviceMappings.TryGetValue(type, out var value) ? value : null;
        }

    }
}