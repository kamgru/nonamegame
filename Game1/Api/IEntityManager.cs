using Game1.Entities;
using System.Collections.Generic;
using Game1.Components;

namespace Game1.Api
{
    public interface IEntityManager
    {
        IEnumerable<Entity> GetEntities();
        IEnumerable<Entity> GetEntitiesByComponent<TComponent>() where TComponent : ComponentBase;
        void RegisterEntity(Entity entity);
        void DestroyEntity(Entity entity);
    }
}