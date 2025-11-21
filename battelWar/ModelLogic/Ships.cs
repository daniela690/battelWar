using battelWar.Models;
namespace battelWar.ModelLogic
{
    public class Ships
    {
        private readonly ShipsModel ship;

        public Ships(ShipsModel ship)
        {
            this.ship = ship;
        }
        public void Hit(int position)
        {
            if (position >= 0 && position < ship.Length)
            {
                ship.Hits[position] = true;
            }
        }
        public bool IsSunk()
        {
            foreach (bool hit in ship.Hits)
            {
                if (!hit) return false;
            }
            return true;
        }
        public bool IsHit()
        {
            foreach (bool hit in ship.Hits)
            {
                if (hit) return true;
            }
            return false;
        }
        public (int x, int y)[] GetCoordinates()
        {
            var coords = new (int x, int y)[ship.Length];
            for (int i = 0; i < ship.Length; i++)
            {
                if (ship.IsVertical)
                    coords[i] = (ship.StartX, ship.StartY + i);
                else
                    coords[i] = (ship.StartX + i, ship.StartY);
            }
            return coords;
        }

    }
}
