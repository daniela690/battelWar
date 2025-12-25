

using battelWar.Models;
using static battelWar.Models.GameModel;

namespace battelWar.ModelsLogic
{
    public class Ship : ShipModel
    {
        private new readonly List<Point> lstHits = new List<Point>();
        private Items[,]? board; // הפניה ללוח כדי לעדכן פגיעות

        public Ship(int size, Items[,]? gameBoard = null) : base(size)
        {
            Size = size;
            board = gameBoard;
        }
        public bool HitPart(Point hitPoint, out bool HitAll)
        {
            if (!lstHits.Contains(hitPoint))
                lstHits.Add(hitPoint);

            // עדכון הלוח אם קיים
            if (board != null)
            {
                board[(int)hitPoint.X, (int)hitPoint.Y] = Items.Ship; // אפשר לשנות ל-Hit אם מוסיפים סוג חדש
            }

            HitAll = lstHits.Count >= Size;
            return true;
        }
        public override bool HitPart(out bool HitAll)
        {
            HitAll = lstHits.Count >= Size;
            return true;
        }
        /// בדיקה אם נקודה ספציפית כבר נפגעה
        public bool IsHit(Point p)
        {
            return lstHits.Contains(p);
        }
    }
}

