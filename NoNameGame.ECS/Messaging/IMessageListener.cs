namespace NoNameGame.ECS.Messaging
{
    public interface IMessageListener<TMessage> where TMessage : IMessage
    {
        void Handle(TMessage message);
    }
}
