using System.Collections.Generic;
using NoNameGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.ECS.Components;
using NoNameGame.ECS;
using NoNameGame.Gameplay.Components;
using NoNameGame.ECS.Entities;

namespace NoNameGame.Gameplay.Factories
{
    public class PlayerFactory
    {
        private readonly ContentManager _contentManager;

        public PlayerFactory(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public Entity CreatePlayer()
        {
            var player = new Entity();

            var texture = _contentManager.Load<Texture2D>("red_ball");

            player.AddComponent(new Sprite { Texture2D = texture, ZIndex = 2000 });
            player.AddComponent(new MoveSpeed { Speed = 3f });
            player.AddComponent(new PositionOnBoard());
            player.AddComponent(new TargetScreenPosition());
            player.AddComponent(new Player());

            player.AddComponent(new State
            {
                CurrentState = PlayerStates.Idle
            });

            player.AddComponent(new Animator
            {
                Animations = new List<Animation>
                {
                    new Animation(_contentManager.Load<Texture2D>("ballses"), new Point(32, 32))
                    {
                        Looped = false,
                        Name = AnimationDictionary.PlayerMove,
                        Speed = 0.5f
                    }
                }
            });

            player.Name = "Player";

            return player;
        }
    }
}
