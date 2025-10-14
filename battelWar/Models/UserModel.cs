
using battelWar.ModelsLogic;
namespace battelWar.Models
{
    internal abstract class UserModel
    {
        protected FBData fbd = new();
        public bool IsRegistered => !string.IsNullOrWhiteSpace(Name);
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; }=string.Empty;
        public abstract void Register();
        public string UserName { get; set; } = string.Empty;
        public abstract bool Login();
        public abstract bool IsValid();
    }
}
