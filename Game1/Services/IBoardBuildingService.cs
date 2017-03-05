using Game1.Entities;
using System.Collections.Generic;

namespace Game1.Services
{
    public interface IBoardBuildingService
    {
        //IEnumerable<Tile> Build(int num);
        Board Build(int num);
    }
}