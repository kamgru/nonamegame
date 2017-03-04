using Game1.Entities;
using System.Collections.Generic;

namespace Game1.Managers
{
    public interface IEntityManager
    {
        IEnumerable<Entity> GetEntities();
        void RegisterEntity(Entity entity);
    }
}