using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using NoNameGame.Data;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Core;
using NoNameGame.ECS.Components;
using NoNameGame.ECS.Factories;
using NoNameGame.ECS;
using NoNameGame.Core.Services;
using NoNameGame.Gameplay.Components;

namespace NoNameGame.Gameplay.Factories
{
    public class TileFactory : EntityFactory
    {
        private readonly Point _size;
        public TileFactory(ContentManager contentManager, ConfigurationService configurationService) 
            : base(contentManager)
        {
            _size = configurationService.GetTileSizeInPixels();
        }

        public Entity CreateTile(Tile data)
        {
            var tile = base.CreateEntity();
            var position = new Point(data.X, data.Y);

            tile.Transform.Position = (position * _size).ToVector2();

            tile.AddComponent(new Sprite
            {
                Texture2D = ContentManager.Load<Texture2D>(data.TextureName)
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
                    new Animation(ContentManager.Load<Texture2D>(data.SheetName), new Point(32, 32))
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
