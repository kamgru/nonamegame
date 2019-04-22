using Microsoft.Xna.Framework;

namespace NoNameGame.Core.Services
{

    public class ConfigurationService
    {
        public int GetFps()
        {
            return 24;
        }

        public Point GetTileSizeInPixels()
        {
            return new Point(32, 32);
        }
    }
}