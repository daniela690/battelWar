using battelWar.Models;
using battelWar.ModelsLogic;
using battelWar.View;
using Microsoft.Extensions.Logging;
using System.Windows.Input;
namespace battelWar.ViewModels
{
    internal partial class HomePageVM : ObservableObject
    {
        public ICommand NavToLoginCommand => new Command(NavToLogin);

        public ICommand NavToRegisterCommand => new Command(NavToRegister);

        private void NavToRegister()
        {
            if (Application.Current != null)
                Application.Current.MainPage = new RegisterV();
        }

        private void NavToLogin()
        {
            if (Application.Current != null)
                Application.Current.MainPage = new Login();
        }
    }
}
