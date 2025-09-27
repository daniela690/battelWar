using battelWar.ModelLogic;
using battelWar.Models;

using System.Windows.Input;

namespace battelWar.ViewModel
{
    internal partial class LoginVM : ObservableObject
    {
        private readonly UserLogin user = new();
        public ICommand LoginCommand { get; }
        public ICommand ToggleIsPasswordCommand { get; }
        public bool IsBusy { get; set; } = false;
        public string UserName
        {
            get => user.UserName;
            set
            {
                user.UserName = value;
                (LoginCommand as Command)?.ChangeCanExecute();
            }
        }
        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                (LoginCommand as Command)?.ChangeCanExecute();
            }
        }
        public bool IsPassword { get; set; } = true;

        public LoginVM()
        {
            LoginCommand = new Command(async () => await Login(), CanLogin);
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
        }

        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
            OnPropertyChanged(nameof(IsPassword));
        }

        private async Task Login()
        {
            IsBusy = true;
            OnPropertyChanged(nameof(IsBusy));
            await Task.Delay(5000);
            IsBusy = false;
            OnPropertyChanged(nameof(IsBusy));
        }

        private bool CanLogin()
        {
            return (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password));
        }


    }
}
