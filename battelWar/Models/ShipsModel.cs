namespace battelWar.Models
{
    public class ShipsModel
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public bool IsVertical { get; set; }
        public int Length { get; set; }
        public bool[] Hits { get; set; }
        public ShipsModel(int startX, int startY, int length, bool isVertical)
        {
            StartX = startX;
            StartY = startY;
            Length = length;
            IsVertical = isVertical;
            Hits = new bool[length];
        }
    }
}
