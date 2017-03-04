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
        public Board(IEnumerable<Tile> tiles, Player player)
        {
            var transform = new TransformComponent();
            AddComponent(transform);
            
            foreach(var tile in tiles)
            {
                tile.GetComponent<TransformComponent>().SetParent(transform);
            }

            player.GetComponent<TransformComponent>().SetParent(transform);
            
        }
                
    }
}
