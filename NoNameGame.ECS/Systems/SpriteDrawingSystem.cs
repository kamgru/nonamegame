using NoNameGame.ECS.Api;
using NoNameGame.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Entities;

namespace NoNameGame.ECS.Systems
{
    public class SpriteDrawingSystem 
        : SystemBase, 
        IDrawingSystem, 
        IMessageListener<ComponentAdded<Sprite>>
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly SpriteFont _debugFont;

        public SpriteDrawingSystem(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _debugFont = contentManager.Load<SpriteFont>("default");
            SystemMessageBroker.AddListener<ComponentAdded<Sprite>>(this);
        }

        public void Draw()
        {
            foreach (var entity in Entities)
            {
                var sprite = entity.GetComponent<Sprite>();
                _spriteBatch.Draw(
                    sprite.Texture2D, 
                    entity.GetComponent<ScreenPosition>().Position, 
                    sprite.Rectangle, 
                    Color.White);
            }
        }

        public void Handle(ComponentAdded<Sprite> message)
        {
            AddEntityInOrder(message.Entity);
        }

        public override void Handle(EntityCreated message)
        {
            if (message.Entity.HasComponent<Sprite>())
            {
                AddEntityInOrder(message.Entity);
            }
        }

        private void AddEntityInOrder(Entity entity)
        {
            Entities.Add(entity);
            Entities = Entities.OrderBy(item => item.GetComponent<Sprite>().ZIndex)
                .ToList();
        }
    }
}
