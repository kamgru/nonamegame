namespace NoNameGame.ECS.Messaging
{
    public static class GameEventManager
    {
        public static void Raise<TEvent>(TEvent @event) where TEvent : IGameEvent
        {
            SystemMessageBroker.Send(@event);
        }

        public static void RegisterHandler<TEvent>(IGameEventHandler<TEvent> handler) where TEvent : IGameEvent
        {
            SystemMessageBroker.AddListener(handler);
        }
    }
}
