using System.Collections.Generic;
using AEShip.Models;
using AEShip.Service.Models;
using AEShip.Service.Models.Requests;
using AEShip.Service.Models.Responses;

namespace AEShip.Service.Interfaces
{
    public interface IShipService
    {
        void AddShips(IEnumerable<NewShipRequest> ship);

        void AddShip(NewShipRequest ship);
        
        void UpdateShipVelocity(string id, double velocity);

        IEnumerable<Ship> GetAllShips();

        IEnumerable<Port> GetAllPorts();

        ClosestPortResponse GetClosestPort(string id);
    }
}
