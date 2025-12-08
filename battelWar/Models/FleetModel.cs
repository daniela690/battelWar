
namespace battelWar.Models
{
    public class Fleet
    {
        public List<ShipModel> Ships { get; set; } = new List<ShipModel>();

        public void AddShip(ShipModel ship)
        {
            Ships.Add(ship);
        }
    }
}
