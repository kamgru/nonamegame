using Microsoft.Xna.Framework;

namespace Game1.ECS.Api
{
    public interface IUpdatingSystem : ISystem
    {
        void Update(GameTime gameTime);
    }
}
