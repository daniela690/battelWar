using battelWar.ModelLogic;
using battelWar.Models;

namespace battelWar.ModelsLogic
{
    public class Navy : NavyModel
    {

        public override void AddShip(Ship ship)
        {
            lstShips.Add(ship);
        }

        public override bool HitPart(out bool HitAll, out Ship hitShip, out bool HitNavy)
        {
            HitAll = false;
            HitNavy = false;
            hitShip = null!; // מאותחל במקרה שלא נמצא ספינה שנפגעה

            for (int i = 0; i < lstShips.Count; i++)
            {
                Ship ship = lstShips[i];

                // בדיקה אם הספינה הושמדה
                bool shipDestroyed;
                ship.HitPart(out shipDestroyed);

                if (shipDestroyed)
                {
                    hitShip = ship;
                    HitAll = true;
                    HitNavy = true;
                    return true;
                }
            }

            // אם לא פגעה באף ספינה
            HitAll = false;
            HitNavy = false;
            return false;
        }
    }
}


