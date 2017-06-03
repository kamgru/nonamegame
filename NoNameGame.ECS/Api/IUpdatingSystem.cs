using Microsoft.Xna.Framework;

namespace NoNameGame.ECS.Api
{
    public interface IUpdatingSystem : ISystem
    {
        void Update(GameTime gameTime);
    }
}
