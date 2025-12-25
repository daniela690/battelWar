namespace battelWar.Models
{
    public abstract class ShipModel
    {
        public List<CellModel> Cells { get; set; } = new List<CellModel>();
        public int Length { get; set; }
        public bool IsVertical { get; set; }

        public ShipModel(int length)
        {
            Length = length;
        }
        protected List<Point> lstHits = [];
        private static int size;
        public Point Position { get; set; }
        public int Size { get; set; } = size;
        public abstract bool HitPart(out bool HitAll);
        public Orientations Orientation{ get; set; }
      
    }
}
