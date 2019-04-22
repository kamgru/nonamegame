﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Core.Input;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Systems.StateHandling;
using NoNameGame.Gameplay.Data;

namespace NoNameGame.Gameplay.StateManagement
{
    public class PlayerIdleHandler : StateHandlerBase
    {
        private readonly ContentManager _contentManager;
        private readonly IInputMapProvider _inputMapProvider;

        public PlayerIdleHandler( ContentManager contentManager, IInputMapProvider inputMapProvider) 
            : base(PlayerStates.Idle)
        {
            _contentManager = contentManager;
            _inputMapProvider = inputMapProvider;
        }

        public override void Handle(EntityState entity)
        {
            if (entity.State.InTransition)
            {
                var texture = _contentManager.Load<Texture2D>("ball_jump_purple");
                var sprite = entity.Entity.GetComponent<Sprite>();
                sprite.Texture2D = texture;
                sprite.Rectangle = new Rectangle(0, 0, 32, 32);

                _inputMapProvider.GetContextById(Contexts.Gameplay)?.Activate();
                entity.State.InTransition = false;
            }
        }
    }
}
