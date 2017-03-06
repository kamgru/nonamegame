using Microsoft.Xna.Framework;

namespace Game1.Api
{
    public interface IConfigurationService
    {
        Point GetTileSizeInPixels();
        int GetFps();
    }
}
