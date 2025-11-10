using battelWar.ViewModels;

namespace battelWar.View;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new HomePageVM();
    }
}