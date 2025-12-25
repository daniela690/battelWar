using battelWar.ModelLogic;
using battelWar.ViewModels;
using battelWar.Models;
namespace battelWar.View;



public partial class GamePage : ContentPage
{
    private readonly GamePageVM gpVM;
    public GamePage(Game game)
	{
		InitializeComponent();
        gpVM = new GamePageVM(game);
        BindingContext = gpVM;
        ShipSizePicker.ItemsSource = new int[] { 2, 3, 4, 5 };
        ShipSizePicker.SelectedIndex = 0;     
        {
            if (ShipSizePicker.SelectedItem != null)
                gpVM.SelectedShipSize = (int)ShipSizePicker.SelectedItem;
        };
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
                CellModel cell = gpVM.Cell[row, col];
                Button btn = new ()
                {
                    WidthRequest = 30,
                    HeightRequest = 30,
                    BackgroundColor = Colors.LightBlue,
                    BindingContext = cell
                };

                btn.Clicked += CellClicked;
                BoardGrid.Add(btn, col, row);
            }
        }
    }
    private void CellClicked(object? sender, EventArgs e)
    {
        Button btn = (Button)sender!;
        CellModel cell = (CellModel)btn.BindingContext;

       

        // מניחים ספינה אנכית
        bool placed = gpVM.PlaceShip(cell.Row, cell.Col);

        if (placed)
            btn.BackgroundColor = Colors.DarkBlue;
    }
}