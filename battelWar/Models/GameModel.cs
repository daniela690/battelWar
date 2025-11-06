using battelWar.ModelLogic;
using battelWar.ModelsLogic;
using Plugin.CloudFirestore.Attributes;


namespace battelWar.Models
{
    internal abstract class GameModel
    {
        protected FBData fbd = new();
        [Ignored]
        public string Id { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public int RowSize { get; set; }
        [Ignored]
        public string RowSizeName => $"{RowSize} X {RowSize}";
        public bool IsFull { get; set; }
        public abstract void SetDocument(Action<Task> OnComplete);
    }
}
