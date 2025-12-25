using battelWar.Models;
using static battelWar.Models.GameModel;
using static battelWar.Models.ShipModel;

namespace battelWar.ModelsLogic
{
    public class ShipFactory : ShipFactoryModel
    {
      

        public  ShipFactory(Items[,] board) : base(board) // ← קריאה ל־constructor של בסיס
        {
            this.board = board;
            lstShipPositions = new List<Point>();
        }
       

        public override int CurrentShipSize
        {
            get => _currentShipSize;
            set
            {
                _currentShipSize = value;
                currentShip = new Ship(_currentShipSize)
                {
                    Size = _currentShipSize
                };
                lstShipPositions.Clear();
            }
        }

        // הצבת חלק ספינה
        public override bool PlaceShipPart(Point shipPosition, Orientations orientation)
        {
            if (currentShip == null)
                return false;

            int x = (int)shipPosition.X;
            int y = (int)shipPosition.Y;

            // בדיקה אם בתוך הלוח
            if (x < 0 || x >= board.GetLength(0) ||
                y < 0 || y >= board.GetLength(1))
                return false;

            // בדיקה אם התא פנוי
            if (board[x, y] != Items.Empty)
                return false;

            // אכיפת כיוון עבור המשבצות הנוספות
            if (lstShipPositions.Count > 0)
            {
                Point first = lstShipPositions[0];

                if (orientation == Orientations.Vertical && x != (int)first.X)
                    return false;

                if (orientation == Orientations.Horizontal && y != (int)first.Y)
                    return false;
            }

            // הוספת המשבצת לספינה
            lstShipPositions.Add(shipPosition);
            board[x, y] = Items.Ship;

            // אם הספינה הושלמה
            if (lstShipPositions.Count == _currentShipSize)
            {
                currentShip.Position = lstShipPositions[0];
                currentShip.Orientation = Orientations.Vertical;

                OnShipCompleted?.Invoke(this, currentShip);

                BlockAround(lstShipPositions);

                currentShip = null!;
                lstShipPositions.Clear();
            }

            return true;
        }


        // בדיקה אם תא סמוך תפוס (חסימה סביב ספינות קיימות)
        private bool IsBlocked(int x, int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < board.GetLength(0) && j >= 0 && j < board.GetLength(1))
                    {
                        if (board[i, j] == Items.Ship) return true;
                    }
                }
            }
            return false;
        }

        // חסימת תאים מסביב לספינה לאחר הצבה
        private void BlockAround(List<Point> shipPoints)
        {
            foreach (var point in shipPoints)
            {
                for (int i = (int)(point.X - 1); i <= point.X + 1; i++)
                {
                    for (int j = (int)(point.Y - 1); j <= point.Y + 1; j++)
                    {
                        if (i >= 0 && i < board.GetLength(0) && j >= 0 && j < board.GetLength(1))
                        {
                            if (board[i, j] == Items.Empty)
                            {
                                board[i, j] = Items.Border; // סימון גבול סביב ספינה
                            }
                        }
                    }
                }
            }
        }
    }
}
