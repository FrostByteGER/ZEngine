namespace ZEngine.Engine.Messaging
{
    public class AbstractMessage : IMessage
    {
        public object Sender { get; private set; }

        public AbstractMessage(object sender)
        {
            this.Sender = sender;
        }
    }
}