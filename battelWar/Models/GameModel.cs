using battelWar.ModelLogic;
using battelWar.ModelsLogic;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;


namespace battelWar.Models
{
    public abstract class GameModel
    {
        protected IListenerRegistration? ilr;
        [Ignored]
        public EventHandler? OnGameChanged;
        [Ignored]
        public EventHandler? OnGameDeleted;
        protected FBData fbd = new();
        [Ignored]
        public string Id { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
        public string GuestName { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public int RowSize { get; set; }
        [Ignored]
        public string RowSizeName => $"{RowSize} X {RowSize}";
        public bool IsFull { get; set; }
        [Ignored]
        public abstract string OpponentName { get; }
        [Ignored]
        public string MyName { get; set; } = new User().Name;
        public bool IsHostUser { get; set; }
        public abstract void SetDocument(Action<Task> OnComplete);
        public abstract void RemoveSnapshotListener();
        public abstract void AddSnapshotListener();
        public abstract void DeleteDocument(Action<System.Threading.Tasks.Task> OnComplete);
    }
}
