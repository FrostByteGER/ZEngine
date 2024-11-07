using ZEngine.Engine.Messaging;

namespace ZEngine.Engine.Core.Messages;

internal class EngineFocusChangeMessage : AbstractMessage
{
    public bool NewFocusState { get; private set; }
    public EngineFocusChangeMessage(object sender, bool newState) : base(sender)
    {
        NewFocusState = newState;
    }
}