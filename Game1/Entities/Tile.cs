using Game1.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Entities
{
    public class Tile : Entity
    {
        public TileInfo TileInfo { get; private set; }

        public Tile(Point position, Point size, Texture2D texture, int tileValue)
        {
            Transform.Position = (position * size).ToVector2();
            AddComponent(new Sprite { Texture2D = texture });
            TileInfo = AddComponent(new TileInfo { Value = tileValue, Position = position }) as TileInfo;
        }
    }
}
