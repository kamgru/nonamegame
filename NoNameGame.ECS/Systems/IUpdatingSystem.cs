using Microsoft.Xna.Framework;

namespace NoNameGame.ECS.Systems
{
    public interface IUpdatingSystem : ISystem
    {
        void Update(GameTime gameTime);
    }
}
