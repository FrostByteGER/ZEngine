using ZEngine.Engine.Services;

namespace ZEngine.Engine.IO.UserInput
{
    public interface IInputManager : IEngineService
    {
        bool RegisterForInputDevice<T>(IInputReceiver receiver) where T : IInputDevice;
        bool RegisterForAllInputDevices(IInputReceiver receiver);
        bool UnregisterFromInputDevice<T>(IInputReceiver receiver) where T : IInputDevice;
        bool UnregisterFromAllInputDevices(IInputReceiver receiver);
    }
}