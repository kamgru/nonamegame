using Game1.Components;
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
        private BoardInfo _boardInfo;
        public BoardInfo BoardInfo
        {
            get
            {
                if (_boardInfo == null)
                {
                    _boardInfo = GetComponent<BoardInfo>();
                }
                return _boardInfo;
            }
        }
    }
}
