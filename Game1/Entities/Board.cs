using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Entities
{
    public class Board : Entity
    {
        private IEnumerable<Tile> _tiles;

        public Point Size { get; private set; }
        public Point Position { get; set; }

        public Board(IEnumerable<Tile> tiles)
        {

        }

        
    }
}
