using Game1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Services
{
    public class BoardService
    {
        public Board GetBoard(int id)
        {
            return new Board
            {
                Tiles = new[]
                {
                    new Tile { Value = 2, X = 0, Y = 0 },
                    new Tile { Value = 2, X = 1, Y = 0 },
                    new Tile { Value = 2, X = 2, Y = 0 },
                    new Tile { Value = 2, X = 3, Y = 0 },
                    new Tile { Value = 2, X = 0, Y = 1 },
                    new Tile { Value = 2, X = 1, Y = 1 },
                    new Tile { Value = 2, X = 2, Y = 1 },
                    new Tile { Value = 2, X = 3, Y = 1 },
                    new Tile { Value = 2, X = 4, Y = 1 },
                }
            };
        }
    }
}
