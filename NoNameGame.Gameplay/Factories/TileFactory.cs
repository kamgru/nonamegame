using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NoNameGame.ECS.Components;
using NoNameGame.Core.Services;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Entities;
using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Data;

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
            var tile = new Tile();
            var position = new Point(data.X, data.Y);

            tile.Transform.Position = (position * _size).ToVector2();

            tile.AddComponent(new Sprite
            {
                Texture2D = _contentManager.Load<Texture2D>(data.TextureName)
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
                    new Animation(_contentManager.Load<Texture2D>(data.SheetName), new Point(32, 32))
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
