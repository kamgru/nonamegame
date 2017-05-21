using Game1.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Game1.Components;
using Game1.Data;
using Game1.Services;
using Game1.Managers;
using Game1.Entities;

namespace Game1.Systems
{
    public class PlayerFsmSystem : SystemBase, IUpdatingSystem
    {
        private readonly InputService _inputService;

        private Dictionary<string, Action<FsmPlayer>> _handlers;

        public PlayerFsmSystem(IEntityManager entityManager, InputService inputService) 
            : base(entityManager)
        {
            _inputService = inputService;

            _handlers = new Dictionary<string, Action<FsmPlayer>>();
            _handlers.Add(PlayerStates.Idle, HandleIdle);
            _handlers.Add(PlayerStates.Moving, HandleMoving);
            _handlers.Add(PlayerStates.Dead, HandleDead);
        }

        public void Update(GameTime gameTime)
        {
            var player = EntityManager.GetEntitiesByComponent<Player>()
                .Select(x => new FsmPlayer
                {
                    Entity = x,
                    TargetScreenPosition = x.GetComponent<TargetScreenPosition>(),
                    State = x.GetComponent<State>(),
                    Animator = x.GetComponent<Animator>()
                })
                .SingleOrDefault();

            if (player?.State?.CurrentState != null && _handlers.ContainsKey(player.State.CurrentState))
            {
                _handlers[player.State.CurrentState](player);
            }
        }

        private void HandleIdle(FsmPlayer player)
        {
            if (player.State.InTransition)
            {
                _inputService.SetContextActive((int)Context.Gameplay, true);
                player.State.InTransition = false;
            }
        }

        private void HandleMoving(FsmPlayer player)
        {
            if (player.State.InTransition)
            {
                _inputService.SetContextActive((int)Context.Gameplay, false);
                player.Entity.GetComponent<Animator>().Play("walk");
                player.State.InTransition = false;
            }
            else
            {
                if (player.Entity.Transform.Position == player.TargetScreenPosition.Position)
                {
                    var currentPosition = player.Entity.GetComponent<PositionOnBoard>().Current;
                    var currentTile = EntityManager.GetEntities()
                        .Where(x => x.HasComponent<TileInfo>())
                        .Select(x => new { TileInfo = x.GetComponent<TileInfo>() })
                        .FirstOrDefault(x => x.TileInfo.Position == currentPosition);

                    if (currentTile == null || currentTile.TileInfo.Destroyed)
                    {
                        player.State.CurrentState = PlayerStates.Dead;
                    }
                    else
                    {
                        player.State.CurrentState = PlayerStates.Idle;
                    }
                }
            }
        }

        private void HandleDead(FsmPlayer player)
        {
            if (player.State.InTransition)
            {
                EntityManager.DestroyEntity(player.Entity);
            }
        }

        private class FsmPlayer
        {
            public Entity Entity { get; set; }
            public State State { get; set; }
            public TargetScreenPosition TargetScreenPosition { get; set; }
            public Animator Animator { get; set; }
        }
    }
}
