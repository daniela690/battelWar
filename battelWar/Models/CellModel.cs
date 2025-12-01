namespace battelWar.Models
{
    public class CellModel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsOccupied { get; set; }      // האם משבצת שייכת לספינה
        public bool IsBlocked { get; set; }       // סביב ספינה (אסור ללחוץ)
        public bool IsClickable { get; set; } = true; // האם המשתמש יכול ללחוץ
        public bool IsHit { get; set; }
    }
}
