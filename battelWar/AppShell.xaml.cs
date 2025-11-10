using battelWar.View;

namespace battelWar
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(RegisterV), typeof(RegisterV));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));

        }
    }
}
