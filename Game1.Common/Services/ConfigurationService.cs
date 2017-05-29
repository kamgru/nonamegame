using System;
using Microsoft.Xna.Framework;

namespace Game1.Services
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