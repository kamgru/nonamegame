using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Api;
using Microsoft.Xna.Framework.Content;
using Game1.Entities;
using Game1.Data;
using Microsoft.Xna.Framework.Graphics;
using Game1.Components;
using Microsoft.Xna.Framework;

namespace Game1.Factories
{
    public class TileFactory : EntityFactory
    {
        private readonly Texture2D _texture;
        private readonly Point _size;

        public TileFactory(IEntityManager entityManager, ContentManager contentManager, IConfigurationService configurationService) 
            : base(entityManager, contentManager)
        {
            _size = configurationService.GetTileSizeInPixels();
            _texture = contentManager.Load<Texture2D>("grey_tile");
        }

        public Entity CreateTile(Tile data)
        {
            var tile = base.CreateEntity();
            var position = new Point(data.X, data.Y);

            tile.Transform.Position = (position * _size).ToVector2();

            tile.AddComponent(new Sprite
            {
                Texture2D = _texture
            });

            tile.AddComponent(new TileInfo
            {
                Value = data.Value,
                Position = position,
                TileType = data.TileType
            });

            tile.AddComponent(new Animator
            {
                Animations = new List<Animation>()
                {
                    new Animation(_contentManager.Load<Texture2D>("tile_break"), new Point(32, 32))
                    {
                        Name = AnimationDictionary.TileDestroy,
                        Speed = 0.5f
                    }
                }
            });

            tile.AddComponent(new State
            {
                CurrentState = TileStates.Idle
            });

            tile.Name = $"tile {position.X} : {position.Y}";

            return tile;
        }
    }
}
