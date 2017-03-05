using Game1.Entities;
using System.Collections.Generic;

namespace Game1.Api
{
    public interface IEntityManager
    {
        IEnumerable<Entity> GetEntities();
        void RegisterEntity(Entity entity);
    }
}