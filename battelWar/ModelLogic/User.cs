using battelWar.Models;


namespace battelWar.ModelLogic
{
    internal  class User : UserModel
    {
        public override void Register()
        {
            Preferences.Set(Keys.NameKey, Name);
            Preferences.Set(Keys.EmailKey, Email);
            Preferences.Set(Keys.PasswordKey, Password);
        }
        public User()
        {
            Name = Preferences.Get(Keys.NameKey, string.Empty);
            Password= Preferences.Get(Keys.PasswordKey, string.Empty);
            Email= Preferences.Get(Keys.EmailKey, string.Empty);
        }
    }
}
