using Microsoft.Xna.Framework.Content;
using NoNameGame.ECS.Core;
using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.ECS;
using Microsoft.Xna.Framework.Graphics;

namespace NoNameGame.Gameplay.Factories
{
    public class PoofFactory
    {
        private readonly ContentManager _contentManager;

        public PoofFactory(ContentManager contentManager) 
        {
            _contentManager = contentManager;
        }

        public Entity CreatePoof()
        {
            var poof = new Entity();
            poof.Transform.Position = Vector2.Zero;
            poof.AddComponent(new Animator
            {
                Animations = new[]
                {
                    new Animation(_contentManager.Load<Texture2D>("poof"), new Point(32, 32))
                    {
                        Looped = false,
                        Name = "poof",
                        Speed = 0.25f
                    }
                },
            });
            poof.AddComponent(new Sprite
            {
                Texture2D = _contentManager.Load<Texture2D>("blank"),
                ZIndex = 1000
            });

            return poof;
        }
    }
}
