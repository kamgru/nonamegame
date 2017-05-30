using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Game1.ECS.Api;
using Game1.ECS.Core;
using Game1.ECS.Components;
using Game1.ECS.Factories;
using Game1.ECS;
using Game1.Gameplay.Components;

namespace Game1.Gameplay.Factories
{
    public class PlayerFactory : EntityFactory
    {
        public PlayerFactory(IEntityManager entityManager, ContentManager contentManager)
            : base(entityManager, contentManager)
        {
        }

        public Entity CreatePlayer()
        {
            var player = base.CreateEntity();

            var texture = ContentManager.Load<Texture2D>("red_dot");

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
                    new Animation(ContentManager.Load<Texture2D>("ball"), new Point(32, 32))
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
