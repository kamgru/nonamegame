using Game1.Entities;
using System.Collections.Generic;

namespace Game1.Api
{
    public interface IBoardBuildingService
    {
        Board Build(int num);
    }
}