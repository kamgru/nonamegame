﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.ECS.Entities;
using NoNameGame.ECS.Input;
using NoNameGame.ECS.Systems.StateHandling;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;

namespace NoNameGame.Gameplay.StateManagement
{
    public class PlayerIdleHandler : StateHandlerBase
    {
        private readonly ContentManager _contentManager;
        private readonly IInputMapProvider _inputMapProvider;

        public PlayerIdleHandler(ContentManager contentManager, IInputMapProvider inputMapProvider)
            : base(PlayerStates.Idle)
        {
            _contentManager = contentManager;
            _inputMapProvider = inputMapProvider;
        }

        public override void UpdateState(Entity entity, GameTime gameTime)
        {
            var player = entity as Player;
            if (player.State.InTransition)
            {
                var texture = _contentManager.Load<Texture2D>(SpriteSheetNames.PlayerSheet);
                player.Sprite.Texture2D = texture;
                player.Sprite.Rectangle = new Rectangle(0, 0, 32, 32);

                _inputMapProvider.GetContextById(Contexts.Gameplay)?.Activate();
                player.State.InTransition = false;
            }
        }
    }
}
