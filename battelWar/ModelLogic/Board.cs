using battelWar.Models;

namespace battelWar.ModelLogic
{
    public class Board
    {
        public bool TryPlaceShip(BoardModel board, int startRow, int startCol, int length, bool vertical)
        {
            // בדיקות חוקיות
            for (int i = 0; i < length; i++)
            {
                int r = startRow + (vertical ? i : 0);
                int c = startCol + (vertical ? 0 : i);

                if (!IsInside(board, r, c)) return false;

                CellModel cell = board.Board[r, c];
                if (cell.IsOccupied || cell.IsBlocked) return false;
            }

            // סימון תאי הספינה
            for (int i = 0; i < length; i++)
            {
                int r = startRow + (vertical ? i : 0);
                int c = startCol + (vertical ? 0 : i);

                CellModel cell = board.Board[r, c];
                cell.IsOccupied = true;
                cell.IsClickable = false;
            }

            // חסימת הסביבה
            BlockAround(board, startRow, startCol, length, vertical);

            return true;
        }

        private void BlockAround(BoardModel board, int row, int col, int length, bool vertical)
        {
            int minR = row - 1;
            int maxR = row + (vertical ? length : 1);
            int minC = col - 1;
            int maxC = col + (vertical ? 1 : length);

            for (int r = minR; r <= maxR; r++)
            {
                for (int c = minC; c <= maxC; c++)
                {
                    if (!IsInside(board, r, c))
                        continue;

                    CellModel cell = board.Board[r, c];

                    if (cell.IsOccupied)
                        continue;

                    cell.IsBlocked = true;
                    cell.IsClickable = false;
                }
            }
        }

        private bool IsInside(BoardModel board, int row, int col)
        {
            return row >= 0 && col >= 0 && row < board.Size && col < board.Size;
        }
    }
} 
    

  
