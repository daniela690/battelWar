using battelWar.ModelLogic;
using battelWar.Models;
using battelWar.ModelsLogic;
using battelWar.View;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;



namespace battelWar.ViewModels
{
    internal partial class MainPageVM : ObservableObject
    {
        private readonly Games games = new();
        public ICommand AddGameCommand => new Command(AddGame);
        public bool IsBusy => games.IsBusy;
        public ObservableCollection<string> GameTypes => games.GameTypes!;
        public string SelectedGameType { get => games.SelectedGameType; set => games.SelectedGameType = value; }
        public ObservableCollection<Game>? GamesList => games.GamesList;
        public Game? SelectGameDetails
        {
            get => games.SelectedGame;

            set
            {
                if (value != null)
                {
                    games.SelectedGame = value;

                    Toast.Make(games.SelectedGame.HostName, ToastDuration.Long).Show();
                    MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        Shell.Current.Navigation.PushAsync(new GamePage(value), true);
                    });
                }
            }
        }

        private void AddGame()
        {
            games.AddGame();
            OnPropertyChanged(nameof(IsBusy));
        }

        public MainPageVM()
        {
            games.OnGameAdded += OnGameAdded;
            games.OnGamesChanged += OnGamesChanged;
            
        }

        private void OnGamesChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(GamesList));
        }

        private void OnGameAdded(object? sender, bool e)
        {
            OnPropertyChanged(nameof(IsBusy));
        }
        internal void AddSnapshotListener()
        {
            games.AddSnapshotListener();
        }

        internal void RemoveSnapshotListener()
        {
            games.RemoveSnapshotListener();
        }

    }
}
