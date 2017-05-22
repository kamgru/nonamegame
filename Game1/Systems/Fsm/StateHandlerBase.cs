using System;
using System.Text;
using System.Threading.Tasks;
using Game1.Managers;

namespace Game1.Systems
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
