using Microsoft.Xna.Framework;
using NoNameGame.Core.Services;
using NoNameGame.ECS.Messaging;
using NoNameGame.Gameplay.Components;
using NoNameGame.Gameplay.Data;
using NoNameGame.Gameplay.Entities;
using System.Linq;

namespace NoNameGame.Gameplay.Factories
{
    public class BoardFactory
    {
        private readonly TileFactory _tileFactory;
        private readonly EndFactory _endFactory;
        private readonly ConfigurationService _configurationService;

        public BoardFactory(TileFactory tileFactory, EndFactory endFactory, ConfigurationService configurationService)
        {
            _tileFactory = tileFactory;
            _endFactory = endFactory;
            _configurationService = configurationService;
        }

        public Board CreateBoard(BoardData data)
        {
            var board = new Board();

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

            var end = _endFactory.CreateEnd();
            end.PositionOnBoard.Current = data.End;

            end.Transform.Position = (data.End * _configurationService.GetTileSizeInPixels()).ToVector2();
            end.Transform.SetParent(board.Transform);

            SystemMessageBroker.Send(new EntityCreated(board));

            return board;
        }
    }
}
