
using battelWar.ModelLogic;
using battelWar.Models;
using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;



namespace battelWar.ViewModels
{
    public partial class GamePageVM : ObservableObject
    {
        private readonly Game game=new();
        public string MyName => game.MyName;
        public string StatusMessage => game.StatusMessage;
        public string OpponentName => game.OpponentName;
        public BoardModel Board { get; }
        public List<int> ShipSizes { get; } = new List<int> { 2, 3, 4, 5 };

        private readonly Board logic;
        private List<CellModel> currentShipCells = new List<CellModel>();
        public ICommand PlaceShipCommand => new Command(PlaceShip);
        public ICommand AddShipCommand => new Command(AddShip);
        public int ShipSize { get; set; }
        public int ShipXPosition { get; set; }
        public int ShipYPosition { get; set; }

        public void PlaceShip()
        {
            Point shipPosition = new(ShipXPosition, ShipYPosition);
            game.PlaceShipPart(shipPosition , SelectedOrientation);
        }


        private void AddShip()
        {
            game.AddShip(ShipSize);
        }
    

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

        public CellModel[,] Cell { get; set; }

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
        public Orientations SelectedOrientation { get; set; } = Orientations.Horizontal;

        public bool PlaceShip(int row, int col)
        {
            Point pos = new(row, col);
            return game.PlaceShipPart(pos, SelectedOrientation);
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
