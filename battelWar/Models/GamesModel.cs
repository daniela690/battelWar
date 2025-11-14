
using battelWar.ModelLogic;
using battelWar.ModelsLogic;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using System.Collections.ObjectModel;


namespace battelWar.Models
{
    public abstract class GamesModel
    {
        protected FBData fbd = new();
        protected IListenerRegistration? ilr;
        protected Game? currentGame;
        public bool IsBusy { get; set; }
        public ObservableCollection<Game>? GamesList { get; set; } = [];
        public Game? CurrentGame { get => currentGame; set => currentGame = value; }
        public ObservableCollection<string> GameTypes => [Strings.Computer, Strings.Player];
        public string SelectedGameType { get; set; } = Strings.Computer;
        [Ignored]
       
        public Game SelectedGame {  get; set; } = new Game();
        public EventHandler<Game>? OnGameAdded;
        public EventHandler? OnGamesChanged;
        public abstract void RemoveSnapshotListener();
        public abstract void AddSnapshotListener();
    }
}
