﻿using NoNameGame.Data;
using NoNameGame.ECS;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Core;
using NoNameGame.ECS.Components;
using NoNameGame.Gameplay.Components;

namespace NoNameGame.Gameplay.StateManagement
{
    public class TileDestroyedHandler : StateHandlerBase
    {
        private readonly IEntityManager _entityManager;

        public TileDestroyedHandler(IEntityManager entityManager) 
            : base(TileStates.Destroyed)
        {
            _entityManager = entityManager;
        }

        public override void Handle(EntityState entityState)
        {
            if (entityState.State.InTransition)
            {
                entityState.Entity.GetComponent<Animator>().Play(AnimationDictionary.TileDestroy);
                entityState.Entity.GetComponent<TileInfo>().Destroyed = true;
                entityState.State.InTransition = false;
            }
            else if (!entityState.Entity.GetComponent<Animator>().IsPlaying)
            {
                _entityManager.DestroyEntity(entityState.Entity);
            }
        }
    }
}