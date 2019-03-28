using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoNameGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Core;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Factories;
using NoNameGame.ECS;
using NoNameGame.Gameplay.Components;

namespace NoNameGame.Gameplay.Factories
{
    public class PlayerFactory : EntityFactory
    {
        public PlayerFactory(ContentManager contentManager)
            : base(contentManager)
        {
        }

        public Entity CreatePlayer()
        {
            var player = base.CreateEntity();

            var texture = ContentManager.Load<Texture2D>("red_ball");

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
                    new Animation(ContentManager.Load<Texture2D>("ballses"), new Point(32, 32))
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
