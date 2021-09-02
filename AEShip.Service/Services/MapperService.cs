using System.Collections.Generic;
using System.Linq;
using AEShip.Service.Interfaces;
using AEShip.Service.Models;
using AEShip.Service.Models.Requests;
using AEShip.Service.Models.Responses;

namespace AEShip.Service.Services
{
    public class MapperService: IMapperService
    {
        public IEnumerable<ShipResponse> MapShipsToResponse(IEnumerable<Ship> ships)
        {
            return ships.Select(s => new ShipResponse(s.Id, s.Name, s.Latitude, s.Longitude, s.Velocity));
        }

        public IEnumerable<PortResponse> MapPortsToResponse(IEnumerable<Port> ports)
        {
            return ports.Select(p => new PortResponse(p.Id, p.Name, p.Latitude, p.Longitude));
        }

        public Ship MapNewShipRequestToShip(NewShipRequest request)
        {
            return new Ship
            {
                Id = request.Id,
                Name = request.Name,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Velocity = request.Velocity
            };
        }
    }
}
