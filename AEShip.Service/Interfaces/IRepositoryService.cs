using System.Collections.Generic;
using AEShip.Service.Models;

namespace AEShip.Service.Interfaces
{
    public interface IRepositoryService
    {
        void AddPort(Port port);

        void AddPorts(IEnumerable<Port> ports);
        
        IEnumerable<Port> GetAllPorts();

        Port GetPort(string id);

        void AddShip(Ship ship);

        void AddShips(IEnumerable<Ship> ships);

        void UpdateShipVelocity(string id, double velocity);

        IEnumerable<Ship> GetAllShips();

        Ship GetShip(string id);
    }
}
