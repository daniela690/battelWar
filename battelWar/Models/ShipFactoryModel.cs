using battelWar.ModelsLogic;
using static battelWar.Models.GameModel;

namespace battelWar.Models
{
    public abstract class ShipFactoryModel(GameModel.Items[,] board)
    {
        protected int _currentShipSize;
        protected Items[,]? board = board;
        public EventHandler<Ship>? OnShipCompleted;
        public abstract int CurrentShipSize { get; set; }
        public abstract bool PlaceShipPart(Point shipPosition, Orientations orientation);
        public Orientations Orientation { get; set; }
        // רשימת משבצות זמניות לספינה הנוכחית
        protected List<Point> lstShipPositions = new List<Point>();
      
        // ספינה נוכחית
        protected Ship? currentShip = null;
    }
}
