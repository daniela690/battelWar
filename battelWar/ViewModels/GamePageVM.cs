
using battelWar.Models;
using battelWar.ModelLogic;
using CommunityToolkit.Maui.Alerts;




namespace battelWar.ViewModels
{
    public partial class GamePageVM : ObservableObject
    {
        private readonly Game game;
        public string MyName => game.MyName;
        public string StatusMessage => game.StatusMessage;
        public string OpponentName => game.OpponentName;
        public BoardModel Board { get; }
        public List<int> ShipSizes { get; } = new List<int> { 2, 3, 4, 5 };

        private readonly Board logic;
        private List<CellModel> currentShipCells = new List<CellModel>();


        private int selectedShipSize = 3;
        public int SelectedShipSize
        {
            get => selectedShipSize;
            set
            {
                if (selectedShipSize != value)
                {
                    selectedShipSize = value;
                    OnPropertyChanged(nameof(SelectedShipSize));
                }
            }
        }
        private Dictionary<int, int> maxShipsPerSize = new Dictionary<int, int>
        {
         { 2, 4 },
         { 3, 3 },
         { 4, 2 },
         { 5, 1 }
        };
        private Dictionary<int, int> shipsPlaced = new Dictionary<int, int>
        {
         { 2, 0 },
         { 3, 0 },
         { 4, 0 },
         { 5, 0 }
        };
        public GamePageVM(Game game)
        {
            Board = new BoardModel(12);
            logic = new Board();
            game.OnGameChanged += OnGameChanged;
            this.game = game;
            Console.WriteLine("########### - IsHostUser =" + game.IsHostUser);
            if (!game.IsHostUser)
                game.UpdateGuestUser(OnComplete);
        }
        public bool PlaceShip(int row, int col, bool vertical)
        {
            CellModel cell = Board.Board[row, col];
            if (shipsPlaced[SelectedShipSize] >= maxShipsPerSize[SelectedShipSize])              
                return false;
            if (!cell.IsClickable || currentShipCells.Count >= SelectedShipSize) return false;

            // מוסיפים את התא הנבחר לספינה הנוכחית
            currentShipCells.Add(cell);
            cell.IsOccupied = true;
            cell.IsClickable = false;

            // אם הגענו לגודל הספינה
            if (currentShipCells.Count == SelectedShipSize)
            {
                // חוסמים את התאים מסביב
                BlockAroundShip(currentShipCells);
                currentShipCells.Clear(); // מאפסים לבחירת ספינה הבאה
                shipsPlaced[SelectedShipSize]++;
            }
            if (currentShipCells.Count >= SelectedShipSize)
            {
                // הודעה למשתמש
                 Toast.Make("בחרי ספינה חדשה ב-Picker", CommunityToolkit.Maui.Core.ToastDuration.Short);
                return false;
            }

            OnPropertyChanged(nameof(Board));
            return true;
        }
        private void BlockAroundShip(List<CellModel> shipCells)
        {
            Board board = new Board(); // או להשתמש בלוגיקה קיימת
            int startRow = shipCells.Min(c => c.Row);
            int endRow = shipCells.Max(c => c.Row);
            int startCol = shipCells.Min(c => c.Col);
            int endCol = shipCells.Max(c => c.Col);

            for (int r = startRow - 1; r <= endRow + 1; r++)
            {
                for (int c = startCol - 1; c <= endCol + 1; c++)
                {
                    if (r >= 0 && c >= 0 && r < Board.Size && c < Board.Size)
                    {
                        CellModel cell = Board.Board[r, c];
                        if (!cell.IsOccupied)
                        {
                            cell.IsBlocked = true;
                            cell.IsClickable = false;
                        }
                    }
                }
            }
        }

        private void OnGameChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(OpponentName));
        }

        private void OnComplete(Task task)
        {
            if (!task.IsCompletedSuccessfully)
                Toast.Make(Strings.JoinGameErr, CommunityToolkit.Maui.Core.ToastDuration.Long, 14);

        }

        public void AddSnapshotListener()
        {
            game.AddSnapshotListener();
        }

        public void RemoveSnapshotListener()
        {
            game.RemoveSnapshotListener();
        }
    }
}
