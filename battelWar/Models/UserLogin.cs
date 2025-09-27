namespace battelWar.Models
{
    abstract class UserLogin
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public abstract bool Login();
    }
}
