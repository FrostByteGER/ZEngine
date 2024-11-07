namespace ZEngine.Engine.Messaging
{
    public class AbstractMessage : IMessage
    {
        public object Sender { get; private set; }

        protected AbstractMessage(object sender)
        {
            this.Sender = sender;
        }
    }
}