using AEShip.Service.Models;

namespace AEShip.Service.Interfaces
{
    public interface IShipUtilities
    {
        double GetDistance(GeoLocation shipLocation,
            GeoLocation portLocation);
    }
}
