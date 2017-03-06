using Game1.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Entities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game1.Components;
using Game1.Data;
using Microsoft.Xna.Framework;

namespace Game1.Services
{
    public class PlayerBuildingService : IPlayerBuildingService
    {
        private readonly ContentManager _contentManager;
        private readonly IEntityFactory _entityFactory;

        public PlayerBuildingService(ContentManager contentManager, IEntityFactory entityFactory)
        {
            _contentManager = contentManager;
            _entityFactory = entityFactory;
        }

        public Player Build()
        {
            var player = _entityFactory.Create<Player>();
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

            return player;
        }
    }
}
