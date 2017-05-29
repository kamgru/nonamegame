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
using System.Diagnostics;

namespace Game1.Factories
{
    public class BoardFactory : EntityFactory
    {
        private readonly TileFactory _tileFactory;

        public BoardFactory(IEntityManager entityManager, ContentManager contentManager, TileFactory tileFactory) 
            : base(entityManager, contentManager)
        {
            _tileFactory = tileFactory;
        }

        public Entity CreateBoard(Board data)
        {
            var board = base.CreateEntity();

            var tiles = data.Tiles
                .Select(tileData => 
                {
                    var tile = _tileFactory.CreateTile(tileData);
                    tile.Transform.SetParent(board.Transform);
                    return tile;
                })
                .ToList();

            board.AddComponent(new BoardInfo
            {
                Size = new Point
                {
                    X = tiles.Max(t => t.GetComponent<TileInfo>().Position.X) + 1,
                    Y = tiles.Max(t => t.GetComponent<TileInfo>().Position.Y) + 1
                }
            });

            board.Name = "Board";

            return board;
        }
    }
}
