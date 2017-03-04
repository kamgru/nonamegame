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
        public Tile(Point position, Texture2D texture, int tileValue)
        {
            AddComponent(new TransformComponent { Position = position.ToVector2() });
            AddComponent(new SpriteComponent { Texture2D = texture });
            AddComponent(new TileComponent { Value = tileValue });
        }
    }
}
