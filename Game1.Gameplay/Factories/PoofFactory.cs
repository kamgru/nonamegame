using Game1.ECS.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.ECS.Api;
using Microsoft.Xna.Framework.Content;
using Game1.ECS.Core;
using Microsoft.Xna.Framework;
using Game1.ECS.Components;
using Game1.ECS;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Gameplay.Factories
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
            poof.AddComponent(new Sprite());
            return poof;
        }
    }
}
