using battelWar.Models;
using System.Collections.Generic;

namespace battelWar.ModelLogic
{
    public class Board
    {
        public Fleet Fleet { get; private set; } = new Fleet();
        private Ship? currentShip = null;

        // קליק על תא בלוח
        public bool TryPlaceCell(BoardModel board, int row, int col, int shipLength)
        {
            if (!IsInside(board, row, col)) return false;

            CellModel clickedCell = board.Board[row, col];

            if (!clickedCell.IsClickable) return false;

            if (currentShip == null)
            {
                // קליק ראשון – יצירת ספינה זמנית
                ShipModel model = new ShipModel(shipLength);
                currentShip = new Ship(model);
                currentShip.Cells.Add(clickedCell);
                return true;
            }
            else
            {
                int currentIndex = currentShip.Cells.Count;
                CellModel previousCell = currentShip.Cells[currentIndex - 1];

                if (!currentShip.CanAddCell(previousCell, clickedCell, currentIndex))
                    return false;

                currentShip.Cells.Add(clickedCell);

                if (currentShip.Cells.Count == 2)
                    currentShip.SetDirection();

                if (currentShip.Cells.Count == currentShip.Length)
                {
                    // ספינה מלאה – מוסיפים ל-Fleet
                    ShipModel shipModel = new ShipModel(currentShip.Length)
                    {
                        Cells = new List<CellModel>(currentShip.Cells),
                        IsVertical = currentShip.IsVertical
                    };

                    Fleet.AddShip(shipModel);

                    // נעילת התאים
                    foreach (CellModel cell in currentShip.Cells)
                    {
                        cell.IsOccupied = true;
                        cell.IsClickable = false;
                    }

                    // חסימת תאים מסביב
                    BlockAround(board, currentShip.Cells);

                    // אפס ספינה זמנית
                    currentShip = null;
                }

                return true;
            }
        }

        // הצבת ספינה לפי התחלה וכיוון (ללא קליקים)
        public bool TryPlaceShip(BoardModel board, int startRow, int startCol, int length, bool vertical)
        {
            List<CellModel> tempCells = new List<CellModel>();

            for (int i = 0; i < length; i++)
            {
                int r = startRow + (vertical ? i : 0);
                int c = startCol + (vertical ? 0 : i);

                if (!IsInside(board, r, c)) return false;

                CellModel cell = board.Board[r, c];
                if (cell.IsOccupied || cell.IsBlocked) return false;

                tempCells.Add(cell);
            }

            foreach (CellModel cell in tempCells)
            {
                cell.IsOccupied = true;
                cell.IsClickable = false;
            }

            BlockAround(board, tempCells);

            // יצירת ShipModel והוספה ל-Fleet
            ShipModel shipModel = new ShipModel(length)
            {
                Cells = tempCells,
                IsVertical = vertical
            };
            Fleet.AddShip(shipModel);

            return true;
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

                        CellModel blockCell = board.Board[r, c];

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
