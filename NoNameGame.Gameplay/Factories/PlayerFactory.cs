using System.Collections.Generic;
using NoNameGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.ECS.Components;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Entities;
using NoNameGame.ECS.Messaging;

namespace NoNameGame.Gameplay.Factories
{
    public class PlayerFactory
    {
        private readonly ContentManager _contentManager;

        public PlayerFactory(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public Player CreatePlayer()
        {
            var player = new Player();

            var texture = _contentManager.Load<Texture2D>("ball_jump_purple");

            player.AddComponent(new Sprite { Texture2D = texture, ZIndex = 2000, Rectangle = new Rectangle(0, 0, 32, 32) });
            player.AddComponent(new PositionOnBoard());
            player.AddComponent(new CommandQueue());

            player.AddComponent(new State
            {
                CurrentState = PlayerStates.Idle
            });

            player.AddComponent(new Animator
            {
                Animations = new List<Animation>
                {
                    new Animation(_contentManager.Load<Texture2D>("ball_jump_purple"), new Point(32, 32))
                    {
                        Looped = false,
                        Name = AnimationDictionary.PlayerMove,
                        Speed = 0.5f
                    }
                }
            });

            player.Name = "Player";

            SystemMessageBroker.Send(new EntityCreated(player));

            return player;
        }
    }
}
