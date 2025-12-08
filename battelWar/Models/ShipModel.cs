namespace battelWar.Models
{
    public class ShipModel
    {
        public List<CellModel> Cells { get; set; } = new List<CellModel>();
        public int Length { get; set; }
        public bool IsVertical { get; set; }

        public ShipModel(int length)
        {
            Length = length;
        }
    }
}
