namespace battelWar.Models
{
    public class BoardModel
    {
        public int Size { get; }
        public CellModel[,] Cells { get; }
        public BoardModel Board { get; internal set; }

        public BoardModel(int size = 12)
        {
            Size = size;
            Cells = new CellModel[size, size];

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    Cells[r, c] = new CellModel
                    {
                        Row = r,
                        Col = c
                    };
                }
            }
        }
        public CellModel? GetCell(int row, int col)
        {
            if (row >= 0 && row < Size && col >= 0 && col < Size)
            {
                return Cells[row, col];
            }
            return null;
        }
    }
}
