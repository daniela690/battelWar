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
        CreateBoard();
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
    private void CreateBoard()
    {
        int size = 12;

        for (int i = 0; i < size; i++)
        {
            BoardGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
            BoardGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
        }

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                var cell = new Frame
                {
                    WidthRequest = 30,
                    HeightRequest = 30,
                    BackgroundColor = Colors.LightBlue,
                    BorderColor = Colors.Black,
                    Padding = 0
                };

                BoardGrid.Add(cell, col, row);
            }
        }
    }
}