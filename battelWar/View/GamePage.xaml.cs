using battelWar.ModelLogic;
using battelWar.ViewModels;
namespace battelWar.View;



public partial class GamePage : ContentPage
{
    private readonly GamePageVM gpVM;
    internal GamePage(Game game)
	{
		InitializeComponent();
        gpVM = new GamePageVM(game);
        BindingContext = gpVM;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        gpVM.AddSnapshotListener();
    }

    protected override void OnDisappearing()
    {
        gpVM.RemoveSnapshotListener();
        base.OnDisappearing();
    }
}