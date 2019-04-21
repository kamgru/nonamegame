using NoNameGame.ECS.Api;
using NoNameGame.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using NoNameGame.ECS.Messaging;
using NoNameGame.ECS.Entities;

namespace NoNameGame.ECS.Systems
{
    public class SpriteDrawingSystem 
        : SystemBase, 
        IDrawingSystem, 
        IMessageListener<ComponentAdded<Sprite>>,
        IMessageListener<ComponentRemoved<Sprite>>
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly SpriteFont _debugFont;
        private List<Entity> _entities = new List<Entity>();

        public SpriteDrawingSystem(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _debugFont = contentManager.Load<SpriteFont>("default");
            SystemMessageBroker.AddListener<ComponentAdded<Sprite>>(this);
            SystemMessageBroker.AddListener<ComponentRemoved<Sprite>>(this);
        }

        public void Draw()
        {
            foreach (var entity in _entities)
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

        public void Handle(ComponentRemoved<Sprite> message)
        {
            _entities.Remove(message.Entity);
        }

        public override void Handle(EntityDestroyed message)
        {
            if (message.Entity.HasComponent<Sprite>())
            {
                _entities.Remove(message.Entity);
            }
        }

        private void AddEntityInOrder(Entity entity)
        {
            _entities.Add(entity);
            _entities = _entities.OrderBy(item => item.GetComponent<Sprite>().ZIndex)
                .ToList();
        }
    }
}
