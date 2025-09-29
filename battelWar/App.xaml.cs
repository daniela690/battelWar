
using battelWar.ModelLogic;
using battelWar.View;
namespace battelWar
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            User user = new();
            Page page = user.IsRegistered ? new LoginV() : new RegisterV();
            MainPage = page;
        }
    }
}
