using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Api;
using Game1.Managers;

namespace Game1.Systems
{
    public abstract class SystemBase : ISystem
    {
        protected readonly IEntityManager EntityManager;
        protected readonly SystemsManager SystemsManager;
        protected bool Active;

        protected SystemBase(IEntityManager entityManager, SystemsManager systemsManager)
        {
            EntityManager = entityManager;
            SystemsManager = systemsManager;
        }

        public virtual void SetActive(bool value)
        {
            Active = value;
        }

        public virtual bool IsActive()
        {
            return Active;
        }
    }
}
