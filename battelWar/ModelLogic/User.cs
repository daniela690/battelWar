using battelWar.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace battelWar.ModelLogic
{
    internal class User : UserModel
    {
        public User()
        {
            Name = Preferences.Get(Keys.NameKey, string.Empty);
        }

        public override void Register()
        {
            fbd.CreateUserWithEmailAndPasswordAsync(Email, Password, Name, OnComplete);
        }

        private void OnComplete(Task task)
        {

            if (task.IsCompletedSuccessfully)
            {
                SaveToPreferences();
                OnAuthComplete?.Invoke(this, EventArgs.Empty);
            }
            else if (task.Exception != null)
            {
                string msg = IdentifyFireBaseError(task);
                ShowAlert(msg);
            }
            else
                ShowAlert(Strings.CreatUserError);
        }
        private static void ShowAlert(string msg)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make(msg, ToastDuration.Long).Show();
            });

        }

        private void SaveToPreferences()
        {
            Preferences.Set(Keys.NameKey, Name);
        }
        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Email);
        }

        
        public override void Login()
        {
            fbd.SignInWithEmailAndPasswordAsync(Email, Password, OnComplete);
        }

        public override string IdentifyFireBaseError(Task task)
        {
            string message = task!.Exception!.InnerException!.Message!;
            int reasonIndex = message.LastIndexOf("Reason:");
            if (reasonIndex >= 0)
            {
                message = message[(reasonIndex + "Reason: ".Length)..];
            }
            return message;
        }
    }
}
