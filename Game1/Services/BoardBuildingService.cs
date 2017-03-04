using Game1.Components;
using Game1.Entities;
using Game1.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Game1.Services
{
    public class BoardBuildingService : IBoardBuildingService
    {
        private IConfigurationService _configurationService;
        private ContentManager _contentManager;
        private IEntityFactory _entityFactory;
        
        public BoardBuildingService(IConfigurationService configurationService, ContentManager contentManager, IEntityFactory entityFactory)
        {
            _contentManager = contentManager;
            _entityFactory = entityFactory;
            _configurationService = configurationService;
        }

        public IEnumerable<Tile> Build(int num)
        {
            var tex = _contentManager.Load<Texture2D>("grey_tile");
            var size = _configurationService.GetTileSizeInPixels();

            var tiles = new List<Tile>();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    tiles.Add(_entityFactory.Create<Tile>(new Point(x * size.X, y * size.Y), tex, 1));
                }
            }

            return tiles;
            
        }
    }
}
