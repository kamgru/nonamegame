using NoNameGame.ECS.Core;
using System.Collections.Generic;

namespace NoNameGame.ECS.Api
{
    public interface IEntityManager
    {
        void DestroyEntity(Entity entity);
    }
}