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
        protected bool Active;

        protected SystemBase(IEntityManager entityManager)
        {
            EntityManager = entityManager;
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
