using battelWar.ViewModel;

namespace battelWar.View;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
        BindingContext = new LoginVM();
    }
}