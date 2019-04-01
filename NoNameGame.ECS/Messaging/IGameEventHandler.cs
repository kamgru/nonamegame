namespace NoNameGame.ECS.Messaging
{
    public interface IGameEventHandler<TEvent> : IMessageListener<TEvent> 
        where TEvent : IGameEvent
    {
    }
}
