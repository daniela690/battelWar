using battelWar.Models;
using battelWar.ModelsLogic;
namespace battelWar.ModelLogic
{
    public class Board
    {
        public BoardModel BoardModel { get; private set; }
        public Navy Navy { get; private set; } = new Navy();
        private Ship? currentShip = null;
        public Board(int size = 12) // ברירת מחדל 12
        {
            BoardModel = new BoardModel(size);
        }
        //public bool PlaceCell(int row, int col, int shipLength)
        //{
        //    if (!IsInside(row, col)) return false;

        //    CellModel clickedCell = BoardModel.Cells[row, col];

        //    if (!clickedCell.IsClickable) return false;

        //    if (currentShip == null)
        //    {
        //        currentShip = new Ship(shipLength);
        //        currentShip.Cells = new List<CellModel> { clickedCell };
        //        return true;
        //    }

        //    int currentIndex = currentShip.Cells.Count;
        //    CellModel previousCell = currentShip.Cells[currentIndex - 1];

        //    if (!currentShip.CanAddCell(previousCell, clickedCell, currentIndex))
        //        return false;

        //    currentShip.Cells.Add(clickedCell);

        //    if (currentShip.Cells.Count == 2)
        //        currentShip.SetDirection();

        //    if (currentShip.Cells.Count == currentShip.Length)
        //    {
        //        Navy.AddShip(currentShip);

        //        foreach (var cell in currentShip.Cells)
        //        {
        //            cell.IsOccupied = true;
        //            cell.IsClickable = false;
        //        }

        //        BlockAround(currentShip.Cells);

        //        currentShip = null;
        //    }

        //    return true;
        //}

        public bool PlaceShip(int startRow, int startCol, int length, bool vertical)
        {
            List<CellModel> tempCells = new List<CellModel>();

            for (int i = 0; i < length; i++)
            {
                int r = startRow + (vertical ? i : 0);
                int c = startCol + (vertical ? 0 : i);

                if (!IsInside(r, c)) return false;

                CellModel cell = BoardModel.Cells[r, c];
                if (cell.IsOccupied || cell.IsBlocked) return false;

                tempCells.Add(cell);
            }

            foreach (var cell in tempCells)
            {
                cell.IsOccupied = true;
                cell.IsClickable = false;
            }

            BlockAround(tempCells);

            Ship ship = new Ship(length)
            {
                Cells = tempCells,
                IsVertical = vertical
            };
            Navy.AddShip(ship);

            return true;
        }

        private void BlockAround(List<CellModel> cells)
        {
            foreach (var cell in cells)
            {
                for (int r = cell.Row - 1; r <= cell.Row + 1; r++)
                {
                    for (int c = cell.Col - 1; c <= cell.Col + 1; c++)
                    {
                        if (!IsInside(r, c)) continue;

                        CellModel blockCell = BoardModel.Cells[r, c];

                        if (cells.Contains(blockCell) || blockCell.IsOccupied) continue;

                        blockCell.IsBlocked = true;
                        blockCell.IsClickable = false;
                    }
                }
            }
        }

        private bool IsInside(int row, int col)
        {
            return row >= 0 && row < BoardModel.Size && col >= 0 && col < BoardModel.Size;
        }
        // חסימת תאים מסביב לספינה
        private void BlockAround(BoardModel board, List<CellModel> cells)
        {
            foreach (CellModel cell in cells)
            {
                for (int r = cell.Row - 1; r <= cell.Row + 1; r++)
                {
                    for (int c = cell.Col - 1; c <= cell.Col + 1; c++)
                    {
                        if (!IsInside(board, r, c)) continue;

                        CellModel blockCell = board.Cells[r, c];

                        if (cells.Contains(blockCell) || blockCell.IsOccupied) continue;

                        blockCell.IsBlocked = true;
                        blockCell.IsClickable = false;
                    }
                }
            }
        }

        private bool IsInside(BoardModel board, int row, int col)
        {
            return row >= 0 && row < board.Size && col >= 0 && col < board.Size;
        }
    }
}
