using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Api;
using Game1.Components;
using Game1.Data;
using Game1.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Factories
{
    public class BoardFactory : EntityFactory
    {
        private readonly IConfigurationService _configurationService;

        public BoardFactory(IEntityManager entityManager, ContentManager contentManager, IConfigurationService configurationService) 
            : base(entityManager, contentManager)
        {
            _configurationService = configurationService;
        }

        public override Entity Create()
        {
            var board = base.Create();
            var size = _configurationService.GetTileSizeInPixels();
            var texture = _contentManager.Load<Texture2D>("grey_tile");

            var tiles = new List<Entity>();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    tiles.Add(CreateTile(new Point(x, y), size, texture, board.Transform));
                }
            }

            board.AddComponent(new BoardInfo
            {
                Size = new Point
                {
                    X = tiles.Max(t => t.GetComponent<TileInfo>().Position.X) + 1,
                    Y = tiles.Max(t => t.GetComponent<TileInfo>().Position.Y) + 1
                }
            });

            return board;
        }

        private Entity CreateTile(Point position, Point size, Texture2D texture, Transform parent)
        {
            var tile = base.Create();

            tile.Transform.Position = (position * size).ToVector2();
            tile.Transform.SetParent(parent);

            tile.AddComponent(new Sprite
            {
                Texture2D = texture
            });

            tile.AddComponent(new TileInfo
            {
                Value = 1,
                Position = position
            });

            tile.AddComponent(new Animator
            {
                Animations = new List<Animation>()
                {
                    new Animation(_contentManager.Load<Texture2D>("tile_break"), new Point(32, 32))
                    {
                        Name = "break",
                        Speed = 0.5f
                    }
                }
            });

            return tile;
        }
    }
}
