using Game1.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Components
{
    public class Animator : ComponentBase
    {
        public bool IsPlaying { get; set; }
        public IEnumerable<Animation> Animations { get; set; }
        public Animation CurrentAnimation { get; set; }

        public void Play(string name)
        {
            var animation = Animations.FirstOrDefault(x => x.Name == name);
            if (animation != null)
            {
                CurrentAnimation = animation;
                IsPlaying = true;
            }
        }
    }
}
