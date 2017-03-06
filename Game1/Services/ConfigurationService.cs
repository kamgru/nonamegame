using System;
using Game1.Api;
using Microsoft.Xna.Framework;

namespace Game1.Services
{

    public class ConfigurationService : IConfigurationService
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