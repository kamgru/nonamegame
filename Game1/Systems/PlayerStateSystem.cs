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

namespace Game1.Systems
{
    public class PlayerStateSystem : SystemBase, IUpdatingSystem
    {
        private readonly InputService _inputService;

        public PlayerStateSystem(IEntityManager entityManager, SystemsManager systemsManager, InputService inputService) 
            : base(entityManager, systemsManager)
        {
            _inputService = inputService;
        }

        public void ChangeState(string state, EntityState component)
        {
            if (state == PlayerStates.Idle)
            {
                _inputService.SetContextActive((int)Context.Gameplay, true);
                component.CurrentState = state;
            }

            if (state == PlayerStates.Moving)
            {
                _inputService.SetContextActive((int)Context.Gameplay, false);
                var animator = component.Entity.GetComponent<Animator>();
                animator.Play("walk");

                animator.OnAnimationEnded = () => {
                    ChangeState(PlayerStates.Idle, component);
                };

                component.CurrentState = state;
            }
        }

        public void Update(GameTime gameTime)
        {
            var entity = EntityManager.GetEntitiesByComponent<EntityState>().FirstOrDefault();

            if (entity != null)
            {

            }
        }
    }
}
