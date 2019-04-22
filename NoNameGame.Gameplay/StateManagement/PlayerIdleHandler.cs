using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Core.Services;
using NoNameGame.Data;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Systems.StateHandling;

namespace NoNameGame.Gameplay.StateManagement
{
    public class PlayerIdleHandler : StateHandlerBase
    {
        private readonly InputService _inputService;
        private readonly ContentManager _contentManager;

        public PlayerIdleHandler(InputService inputService, ContentManager contentManager) 
            : base(PlayerStates.Idle)
        {
            _inputService = inputService;
            _contentManager = contentManager;
        }

        public override void Handle(EntityState entity)
        {
            if (entity.State.InTransition)
            {
                var texture = _contentManager.Load<Texture2D>("ball_jump_purple");
                var sprite = entity.Entity.GetComponent<Sprite>();
                sprite.Texture2D = texture;
                sprite.Rectangle = new Rectangle(0, 0, 32, 32);

                _inputService.SetContextActive((int)Context.Gameplay, true);
                entity.State.InTransition = false;
            }
        }
    }
}
