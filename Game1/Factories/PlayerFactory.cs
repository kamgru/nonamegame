﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Api;
using Game1.Components;
using Game1.Data;
using Game1.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Factories
{
    public class PlayerFactory : EntityFactory
    {
        public PlayerFactory(IEntityManager entityManager, ContentManager contentManager)
            : base(entityManager, contentManager)
        {
        }

        public override Entity CreateEntity()
        {
            var player = base.CreateEntity();

            var texture = _contentManager.Load<Texture2D>("red_dot");

            player.AddComponent(new Sprite { Texture2D = texture });
            player.AddComponent(new IntentMap { Intent = Intent.MoveDown | Intent.MoveLeft | Intent.MoveRight | Intent.MoveUp });
            player.AddComponent(new MoveSpeed { Speed = 3f });
            player.AddComponent(new BoardPosition());

            player.AddComponent(new Animator
            {
                Animations = new List<Animation>
                {
                    new Animation(_contentManager.Load<Texture2D>("ball"), new Point(32, 32))
                    {
                        Looped = false,
                        Name = "walk",
                        Speed = 0.5f
                    }
                }
            });

            player.Name = "Player";

            return player;
        }
    }
}