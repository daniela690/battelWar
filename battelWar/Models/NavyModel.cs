

using battelWar.ModelsLogic;

namespace battelWar.Models
{
    public abstract class NavyModel
    {
        protected List<Ship> lstShips = [];
        public abstract void AddShip(Ship ship);
        public abstract bool HitPart(out bool HitAll, out Ship ship, out bool HitNavy);
    }
}
