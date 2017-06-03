using NoNameGame.ECS.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoNameGame.ECS.Api;
using Microsoft.Xna.Framework.Content;
using NoNameGame.ECS.Core;
using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.ECS;
using Microsoft.Xna.Framework.Graphics;

namespace NoNameGame.Gameplay.Factories
{
    public class PoofFactory : EntityFactory
    {
        public PoofFactory(IEntityManager entityManager, ContentManager contentManager) 
            : base(entityManager, contentManager)
        {
        }

        public Entity CreatePoof()
        {
            var poof = CreateEntity();
            poof.Transform.Position = Vector2.Zero;
            poof.AddComponent(new Animator
            {
                Animations = new[]
                {
                    new Animation(ContentManager.Load<Texture2D>("poof"), new Point(32, 32))
                    {
                        Looped = false,
                        Name = "poof",
                        Speed = 0.25f
                    }
                },
            });
            poof.AddComponent(new Sprite
            {
                Texture2D = ContentManager.Load<Texture2D>("blank"),
                ZIndex = 1000
            });
            return poof;
        }
    }
}
