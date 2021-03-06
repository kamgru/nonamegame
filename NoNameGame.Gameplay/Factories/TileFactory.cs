﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NoNameGame.Core.Services;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;

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

        public Tile CreateTile(TileData data)
        {
            var tileSpriteSheet = _contentManager.Load<Texture2D>(SpriteSheetNames.TilesSheet);

            Tile tile;
            switch (data.TileType)
            {
                case TileType.Single:
                    tile = CreateNormalTile(tileSpriteSheet);
                    break;
                case TileType.Double:
                    tile = CreateDoubleTile(tileSpriteSheet);
                    break;
                case TileType.Triple:
                    tile = CreateTripleTile(tileSpriteSheet);
                    break;
                case TileType.Unknown:
                    tile = CreateUnknownTile(tileSpriteSheet);
                    break;
                default:
                    tile = CreateIndestructibleTile(tileSpriteSheet);
                    break;
            }

            var position = new Point(data.X, data.Y);
            tile.Transform.Position = (position * _size).ToVector2();

            tile.TileInfo = tile.AddComponent(new TileInfo
            {
                Value = data.Value,
                Position = position,
                TileType = data.TileType
            });

            tile.State = tile.AddComponent(new State
            {
                CurrentState = TileStates.Pristine
            });

            tile.Name = $"tile {position.X} : {position.Y}";

            SystemMessageBroker.Send(new EntityCreated(tile));

            return tile;
        }

        private Tile CreateIndestructibleTile(Texture2D sheet)
        {
            var tile = new Tile();
            var sprite = new Sprite { Texture2D = sheet, Rectangle = new Rectangle(0, 96, 32, 32) };
            tile.Sprite = tile.AddComponent(sprite);

            return tile;
        }

        private Tile CreateNormalTile(Texture2D sheet)
        {
            var tile = new Tile();
            var sprite = new Sprite { Texture2D = sheet, Rectangle = new Rectangle(0, 0, 32, 32) };
            tile.Sprite = tile.AddComponent(sprite);

            var animation = new Animation(sheet, new Rectangle[]
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
            };

            tile.Animator = tile.AddComponent(new Animator { Animations = new[] { animation } });

            return tile;
        }

        private Tile CreateDoubleTile(Texture2D sheet)
        {
            var tile = new Tile();
            var sprite = new Sprite { Texture2D = sheet, Rectangle = new Rectangle(0, 32, 32, 32) };
            tile.Sprite = tile.AddComponent(sprite);

            var animation = new Animation(sheet, new Rectangle[]
                    {
                        new Rectangle(64, 32, 32, 32),
                        new Rectangle(96, 32, 32, 32),
                        new Rectangle(128, 32, 32, 32),
                        new Rectangle(160, 32, 32, 32),
                        new Rectangle(192, 32, 32, 32),
                        new Rectangle(224, 32, 32, 32),
                    })
            {
                Name = AnimationDictionary.TileDestroy,
                Speed = 0.5f
            };

            tile.Animator = tile.AddComponent(new Animator { Animations = new[] { animation } });

            return tile;
        }

        private Tile CreateTripleTile(Texture2D sheet)
        {
            var tile = new Tile();
            var sprite = new Sprite { Texture2D = sheet, Rectangle = new Rectangle(0, 64, 32, 32) };
            tile.Sprite = tile.AddComponent(sprite);

            var animation = new Animation(sheet, new Rectangle[]
                {
                    new Rectangle(96, 64, 32, 32),
                    new Rectangle(128, 64, 32, 32),
                    new Rectangle(160, 64, 32, 32),
                    new Rectangle(192, 64, 32, 32),
                    new Rectangle(224, 64, 32, 32),
                    new Rectangle(256, 64, 32, 32),
                })
            {
                Name = AnimationDictionary.TileDestroy,
                Speed = 0.5f
            };

            tile.Animator = tile.AddComponent(new Animator { Animations = new[] { animation } });

            return tile;
        }

        private Tile CreateUnknownTile(Texture2D sheet)
        {
            var tile = new Tile();
            var sprite = new Sprite { Texture2D = sheet, Rectangle = new Rectangle(0, 128, 32, 32) };
            tile.Sprite = tile.AddComponent(sprite);

            var destroyAnimation = new Animation(sheet, new[]
                {
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),
                    new Rectangle(96, 128, 32, 32),
                    new Rectangle(128, 128, 32, 32),
                    new Rectangle(160, 128, 32, 32),
                    new Rectangle(192, 128, 32, 32),
                    new Rectangle(224, 128, 32, 32),
                })
            {
                Name = AnimationDictionary.TileDestroy,
                Speed = 0.5f
            };

            var touchAnimation = new Animation(sheet, new[]
                {
                   new Rectangle(0, 160, 32, 32),
                   new Rectangle(0, 160, 32, 32),
                   new Rectangle(0, 160, 32, 32),
                   new Rectangle(0, 160, 32, 32),
                   new Rectangle(32, 160, 32, 32),
                })
            {
                Name = AnimationDictionary.TileTouch,
                Speed = 0.3f
            };

            tile.Animator = tile.AddComponent(new Animator { Animations = new[] { destroyAnimation, touchAnimation } });

            return tile;
        }
    }
}
