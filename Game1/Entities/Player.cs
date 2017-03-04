using Game1.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Common;

namespace Game1.Entities
{
    public class Player : Entity
    {
        public Player(Vector2 position, Texture2D texture)
        {
            var transform = AddComponent(new TransformComponent { Position = position }) as TransformComponent;
            AddComponent(new SpriteComponent { Texture2D = texture });
            var moveTo = AddComponent(new MoveToComponent { Speed = 5f }) as MoveToComponent;
            AddComponent(new IntentMapComponent { Intent = Intent.MoveUp | Intent.MoveRight | Intent.MoveLeft | Intent.MoveDown});

            moveTo.Active = false;
        }
    }
}
