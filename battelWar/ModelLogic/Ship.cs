using battelWar.Models;

namespace battelWar.ModelLogic
{
    public class Ship
    {
        private ShipModel model;
        public Ship(ShipModel shipModel)
        {
            model = shipModel;
        }
        public List<CellModel> Cells => model.Cells;
        public int Length => model.Length;
        public bool IsVertical
        {
            get => model.IsVertical;
            set => model.IsVertical = value;
        }
        public bool IsSunk
        {
            get
            {
                foreach (CellModel cell in Cells)
                {
                    if (!cell.IsOccupied)
                        return false;
                }
                return true;
            }
        }
        public bool CanAddCell(CellModel previousCell, CellModel newCell, int currentIndex)
        {
            if (currentIndex == 0) return true;

            if (currentIndex == 1)
            {
                bool isAdjacent = (newCell.Row == previousCell.Row && (newCell.Col == previousCell.Col + 1 || newCell.Col == previousCell.Col - 1))
                    || (newCell.Col == previousCell.Col && (newCell.Row == previousCell.Row + 1 || newCell.Row == previousCell.Row - 1));
                return isAdjacent;
            }
            else
            {
                if (IsVertical)
                {
                    return newCell.Col == previousCell.Col && (newCell.Row == previousCell.Row + 1 || newCell.Row == previousCell.Row - 1);
                }
                else
                {
                    return newCell.Row == previousCell.Row && (newCell.Col == previousCell.Col + 1 || newCell.Col == previousCell.Col - 1);
                }
            }
        }
        public void SetDirection()
        {
            if (Cells.Count >= 2)
            {
                IsVertical = Cells[0].Col == Cells[1].Col;
            }
        }
    }
}
