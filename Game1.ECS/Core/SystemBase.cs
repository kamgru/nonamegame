using Game1.ECS.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.ECS.Core
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
