
using battelWar.ModelLogic;
using battelWar.ModelsLogic;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;


namespace battelWar.Models
{
    internal class GamesModel
    {
        protected FBData fbd = new();
        protected IListenerRegistration? ilr;

        public bool IsBusy { get; set; }
        public ObservableCollection<Game>? GamesList { get; set; } = [];
        public ObservableCollection<string> GameTypes => [Strings.Computer, Strings.Player];
        public string SelectedGameType { get; set; } = Strings.Computer;

        public Game SelectedGame {  get; set; } = new Game();
        public EventHandler<bool>? OnGameAdded;
        public EventHandler? OnGamesChanged;
    }
}
