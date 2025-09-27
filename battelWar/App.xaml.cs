
using battelWar.ModelLogic;

namespace battelWar
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            User user = new();
            Page page = user.IsRegistered ? new View.LoginV() : new View.RegisterV();
            MainPage = page;
        }
    }
}
