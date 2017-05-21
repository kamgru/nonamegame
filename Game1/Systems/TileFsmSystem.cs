using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Api;
using Game1.Components;
using Microsoft.Xna.Framework;
using Game1.Managers;
using Game1.Data;

namespace Game1.Systems
{
    public class TileFsmSystem : SystemBase, IUpdatingSystem
    {
        private readonly IEntityManager _entityManager;

        public TileFsmSystem(IEntityManager entityManager)
            :base(entityManager)
        {
            _entityManager = entityManager;
        }

        public void Update(GameTime gameTime)
        {
            var tiles = _entityManager.GetEntitiesByComponent<TileInfo>()
                .Select(x => new
                {
                    TileInfo = x.GetComponent<TileInfo>(),
                    Animator = x.GetComponent<Animator>(),
                    State = x.GetComponent<State>(),
                    Entity = x
                })
                .ToList();

            foreach (var tile in tiles)
            {
                if (tile.State.CurrentState == TileStates.Abandoned)
                {
                    if (tile.State.InTransition)
                    {
                        tile.State.InTransition = false;
                        tile.TileInfo.Value--;

                        if (tile.TileInfo.Value <= 0)
                        {
                            tile.State.CurrentState = TileStates.Destroyed;
                        }
                    }
                }
                else if (tile.State.CurrentState == TileStates.Destroyed)
                {
                    if (tile.State.InTransition)
                    {
                        tile.Animator.Play("break");
                        tile.TileInfo.Destroyed = true;
                        tile.State.InTransition = false;
                    }
                    else if (!tile.Animator.IsPlaying)
                    {
                        EntityManager.DestroyEntity(tile.Entity);
                    }
                }
            }
        }
    }
}
