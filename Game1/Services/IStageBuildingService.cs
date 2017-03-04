using Game1.Entities;
using System.Collections.Generic;

namespace Game1.Services
{
    public interface IStageBuildingService
    {
        IEnumerable<Tile> Build(int num);
    }
}