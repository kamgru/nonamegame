using Game1.Components;
using Game1.Entities;
using Game1.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
using Game1.Api;
using Game1.Data;

namespace Game1.Services
{
    public class BoardBuildingService : IBoardBuildingService
    {
        private IConfigurationService _configurationService;
        private ContentManager _contentManager;
        private IEntityFactory _entityFactory;
        private GraphicsDevice _graphicsDevice;
        
        public BoardBuildingService(IConfigurationService configurationService,
            ContentManager contentManager, 
            IEntityFactory entityFactory,
            GraphicsDevice graphicsDevice)
        {
            _contentManager = contentManager;
            _entityFactory = entityFactory;
            _configurationService = configurationService;
            _graphicsDevice = graphicsDevice;
        }

        public Board Build(int num)
        {
            var texture = _contentManager.Load<Texture2D>("grey_tile");
            var size = _configurationService.GetTileSizeInPixels();
            //var tileSpriteSheet = _contentManager.Load<Texture2D>("tile_break");
            var tiles = new List<Tile>();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    tiles.Add(_entityFactory.Create<Tile>(new Point(x, y), size, new Texture2D(_graphicsDevice, size.X, size.Y), 1));
                }
            }

            var board = _entityFactory.Create<Board>();
            tiles.ForEach(x => 
            {
                x.Transform.SetParent(board.Transform);
                x.AddComponent(new Animator
                {
                    Animations = new List<Animation>()
                    {
                        new Animation(_contentManager.Load<Texture2D>("tile_break"), new Point(32, 32))
                        {
                            Name = "break", Speed = 0.5f
                        }
                    }
                });
            });

            board.AddComponent(new BoardInfo
            {
                Size = new Point(tiles.Max(t => t.TileInfo.Position.X) + 1, tiles.Max(t => t.TileInfo.Position.Y) + 1),
                TileInfos = tiles.Select(x => x.TileInfo).ToList()
            });

            return board;
        }
    }
}
