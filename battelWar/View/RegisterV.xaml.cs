namespace battelWar.View;
using battelWar.ViewModels;
public partial class RegisterV : ContentPage
{
	public RegisterV()
	{
        InitializeComponent();
        BindingContext = new RegisterVM();
    }
}