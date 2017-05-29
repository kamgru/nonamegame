using Game1.ECS.Core;
using System.Collections.Generic;

namespace Game1.ECS.Api
{
    public interface IEntityManager
    {
        IEnumerable<Entity> GetEntities();
        IEnumerable<Entity> GetEntitiesByComponent<TComponent>() where TComponent : ComponentBase;
        void RegisterEntity(Entity entity);
        void DestroyEntity(Entity entity);
    }
}