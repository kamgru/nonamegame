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
            Transform.Position = position;
            AddComponent(new Sprite { Texture2D = texture });
            AddComponent(new IntentMap { Intent = Intent.MoveUp | Intent.MoveRight | Intent.MoveLeft | Intent.MoveDown});
            AddComponent(new MoveSpeed { Speed = 5f });
        }
    }
}
