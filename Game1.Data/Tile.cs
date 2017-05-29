namespace Game1.Data
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public TileType TileType { get; set; }
        public string TextureName { get; set; }
        public string SheetName { get; set; }
    }
}
