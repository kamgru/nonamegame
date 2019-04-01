﻿namespace NoNameGame.ECS.StateHandling
{
    public abstract class StateHandlerBase
    {
        public string State { get; }

        protected StateHandlerBase(string state)
        {
            State = state;
        }

        public abstract void Handle(EntityState entityState);
    }
}