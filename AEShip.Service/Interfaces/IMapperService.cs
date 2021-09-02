using System.Collections.Generic;
using AEShip.Service.Models;
using AEShip.Service.Models.Requests;
using AEShip.Service.Models.Responses;

namespace AEShip.Service.Interfaces
{
    public interface IMapperService
    {
        IEnumerable<ShipResponse> MapShipsToResponse(IEnumerable<Ship> ships);

        IEnumerable<PortResponse> MapPortsToResponse(IEnumerable<Port> ports);

        Ship MapNewShipRequestToShip(NewShipRequest request);
    }
}
