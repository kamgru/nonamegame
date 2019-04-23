using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Core.Services;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;
using System.Collections.Generic;

namespace NoNameGame.Gameplay.Factories
{
    public class TileFactory
    {
        private readonly ContentManager _contentManager;
        private readonly Point _size;

        public TileFactory(ContentManager contentManager, ConfigurationService configurationService)
        {
            _contentManager = contentManager;
            _size = configurationService.GetTileSizeInPixels();
        }

        private Sprite CreateSpriteForTileType(TileType tileType)
        {
            var texture = _contentManager.Load<Texture2D>(SpriteSheetNames.TilesSheet);
            Rectangle rectangle = default;

            switch (tileType)
            {
                case TileType.Normal:
                    rectangle = new Rectangle(0, 0, 32, 32);
                    break;
                default:
                    rectangle = new Rectangle(0, 96, 32, 32);
                    break;
            }

            return new Sprite
            {
                Texture2D = texture,
                Rectangle = rectangle
            };
        }

        public Tile CreateTile(TileData data)
        {
            var tile = new Tile();
            var position = new Point(data.X, data.Y);

            tile.Transform.Position = (position * _size).ToVector2();

            tile.AddComponent(CreateSpriteForTileType(data.TileType));

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
                    new Animation(_contentManager.Load<Texture2D>(SpriteSheetNames.TilesSheet), new Rectangle[] 
                    {
                        new Rectangle(0, 0, 32, 32),
                        new Rectangle(32, 0, 32, 32),
                        new Rectangle(64, 0, 32, 32),
                        new Rectangle(96, 0, 32, 32),
                        new Rectangle(128, 0, 32, 32),
                        new Rectangle(160, 0, 32, 32),
                    })
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

            SystemMessageBroker.Send(new EntityCreated(tile));

            return tile;
        }
    }
}
