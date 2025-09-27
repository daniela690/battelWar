using battelWar.ViewModel;
namespace battelWar.View;

public partial class LoginV : ContentPage
{
	public LoginV()
	{
		InitializeComponent();
        BindingContext = new LoginVM();
    }
}