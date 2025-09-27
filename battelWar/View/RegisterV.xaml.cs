namespace battelWar.View;
using battelWar.ViewModel;
public partial class RegisterV : ContentPage
{
	public RegisterV()
	{
        InitializeComponent();
        BindingContext = new RegisterVM();
    }
}