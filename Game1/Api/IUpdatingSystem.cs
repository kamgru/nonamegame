using Microsoft.Xna.Framework;

namespace Game1.Api
{
    public interface IUpdatingSystem : ISystem
    {
        void Update(GameTime gameTime);
    }
}
