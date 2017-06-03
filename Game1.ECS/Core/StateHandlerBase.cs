using System;
using System.Text;
using System.Threading.Tasks;

namespace NoNameGame.ECS.Core
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
