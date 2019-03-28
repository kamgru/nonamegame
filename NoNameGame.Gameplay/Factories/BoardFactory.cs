using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoNameGame.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using NoNameGame.ECS.Api;
using NoNameGame.ECS.Core;
using NoNameGame.Gameplay.Components;
using NoNameGame.ECS.Messaging;

namespace NoNameGame.Gameplay.Factories
{
    public class BoardFactory
    {
        private readonly TileFactory _tileFactory;

        public BoardFactory(TileFactory tileFactory) 
        {
            _tileFactory = tileFactory;
        }

        public Entity CreateBoard(Board data)
        {
            var board = new Entity();

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
