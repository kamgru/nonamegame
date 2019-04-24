using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;
using System.Collections.Generic;

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

            var texture = _contentManager.Load<Texture2D>(SpriteSheetNames.PlayerSheet);

            player.Sprite = player.AddComponent(new Sprite { Texture2D = texture, ZIndex = 2000, Rectangle = new Rectangle(0, 0, 32, 32) });
            player.PositionOnBoard = player.AddComponent(new PositionOnBoard());
            player.CommandQueue = player.AddComponent(new CommandQueue());

            player.State = player.AddComponent(new State
            {
                CurrentState = PlayerStates.Idle
            });

            player.Animator = player.AddComponent(new Animator
            {
                Animations = new List<Animation>
                {
                    new Animation(_contentManager.Load<Texture2D>(SpriteSheetNames.PlayerSheet), new []
                    {
                        new Rectangle(0, 0, 32, 32),
                        new Rectangle(32, 0, 32, 32),
                        new Rectangle(64, 0, 32, 32),
                        new Rectangle(96, 0, 32, 32),
                        new Rectangle(128, 0, 32, 32),
                        new Rectangle(160, 0, 32, 32),
                    })
                    {
                        Looped = false,
                        Name = AnimationDictionary.PlayerMove,
                        Speed = 0.5f
                    },
                    new Animation(texture, new[]
                    {
                        new Rectangle(0, 32, 32, 32),
                        new Rectangle(32, 32, 32, 32),
                        new Rectangle(64, 32, 32, 32),
                        new Rectangle(96, 32, 32, 32),
                        new Rectangle(128, 32, 32, 32),
                    })
                    {
                        Name = AnimationDictionary.PlayerFall,
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
