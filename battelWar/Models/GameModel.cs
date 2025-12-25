using battelWar.ModelLogic;
using battelWar.ModelsLogic;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;



namespace battelWar.Models
{
    public abstract class GameModel
    {
        public string GameType { get; set; } = string.Empty;
        protected IListenerRegistration? ilr;
        [Ignored]
        public EventHandler? OnGameChanged;
        [Ignored]
        public EventHandler? OnGameDeleted;
        protected FBData fbd = new();
        protected abstract GameStatus Status { get; }
        [Ignored]
        public string StatusMessage => Status.StatusMessage;
        public string Id { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
        public string GuestName { get; set; } = string.Empty;
        public DateTime Created { get; set; }
      
       
        public bool IsFull { get; set; }
        public bool IsHostTurn { get; set; } = false;
        [Ignored]
        public abstract string OpponentName { get; }
        [Ignored]
        public string MyName { get; set; } = new User().Name;
        [Ignored]
        public bool IsHostUser { get; set; }
        public abstract void SetDocument(Action<Task> OnComplete);
        public abstract void RemoveSnapshotListener();
        public abstract void AddSnapshotListener();
        public abstract void DeleteDocument(Action<System.Threading.Tasks.Task> OnComplete);
        protected const int BoardSize = 12;
        protected Navy navy = new();
        public enum Items { Empty, Border, Ship }
        protected Items[,] board = new Items[BoardSize, BoardSize];
        protected ShipFactory? shipFactory;
        public  bool PlaceShipPart(Point shipPosition, Orientations orientation) => shipFactory!.PlaceShipPart(shipPosition, orientation);
        public abstract void AddShip(int size);
       
    }
}
