using battelWar.ModelLogic;
using battelWar.Models;
using System.Windows.Input;
namespace battelWar.ViewModel
{
    internal class RegisterVM:ObservableObject
    {
        private readonly User user = new();
        public ICommand RegisterCommand { get; }
        public ICommand ToggleIsPasswordCommand { get; }
        public bool IsBusy { get; set; } = false;

        private bool CanRegister(object arg)
        {
            return user.IsValid();
        }

        private void Register(object obj)
        {
            user.Register();
        }
        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
            OnPropertyChanged(nameof(IsPassword));
        }

        public string Name
        {
            get => user.Name;
            set
            {
                if (user.Name != value)
                {
                    user.Name = value;
                    (RegisterCommand as Command)?.ChangeCanExecute();
                }
            }
        }
        public string Password
        {
            get => user.Password;
            set
            {
                if (user.Password != value)
                {
                    user.Password = value;
                    (RegisterCommand as Command)?.ChangeCanExecute();
                }
            }
        }
        public string Email
        {
            get => user.Email;
            set
            {
                if (user.Email != value)
                {
                    user.Email = value;
                    (RegisterCommand as Command)?.ChangeCanExecute();
                }
            }
        }
        public bool IsPassword { get; set; } = true;

        public RegisterVM()
        {
            RegisterCommand = new Command(Register, CanRegister);
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
            user.OnAuthComplete += OnAuthComplete;
        }

        private void OnAuthComplete(object? sender, EventArgs e)
        {
           if(Application.Current!=null)
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Application.Current.MainPage = new AppShell();
                });
            }
        }
    }
}
