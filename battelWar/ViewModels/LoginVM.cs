using battelWar.ModelLogic;
using battelWar.Models;
using System.Windows.Input;

namespace battelWar.ViewModels
{
    internal partial class LoginVM : ObservableObject
    {
        private readonly User user = new();
        public ICommand LoginCommand { get; }
        public ICommand ToggleIsPasswordCommand { get; }
        public bool IsBusy { get; set; } = true;
        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
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
            LoginCommand = new Command( Login, CanLogin);
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
            user.OnAuthComplete += OnAuthComplete;
        }

        private void OnAuthComplete(object? sender, EventArgs e)
        {
            if (Application.Current != null)
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Application.Current.MainPage = new AppShell();
                });
            }
        }

        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
            OnPropertyChanged(nameof(IsPassword));
        }

        private void Login()
        {
            IsBusy = true;
            OnPropertyChanged(nameof(IsBusy));
            user.Login();
        }

        private bool CanLogin()
        {
            return (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password));
        }


    }
}
