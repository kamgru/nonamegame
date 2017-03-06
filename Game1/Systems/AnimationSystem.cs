using Game1.Api;
using Game1.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Systems
{
    public class AnimationSystem : ISystem
    {
        private readonly IEntityManager _entityManager;

        public AnimationSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }
            

        public void Update(GameTime gameTime)
        {
            var anims = _entityManager.GetEntitiesByComponent<Animation>().Select(x => x.GetComponent<Animation>());

            foreach (var anim in anims)
            {
                var spr = anim.Entity.GetComponent<Sprite>();

                anim.Elapsed += gameTime.ElapsedGameTime.Milliseconds;

                if (anim.Elapsed > 100)
                {
                    anim.CurrentFrame++;
                    if (anim.CurrentFrame >= anim.FrameCount)
                    {                        
                        if (!anim.Looped)
                        {
                            continue;
                        }
                        anim.CurrentFrame = 0;
                    }
                    anim.Elapsed = gameTime.ElapsedGameTime.Milliseconds;

                    var frame = anim.GetFrame(anim.CurrentFrame);

                    spr.Texture2D.SetData(frame);
                }
                
            }
        }
    }
}
