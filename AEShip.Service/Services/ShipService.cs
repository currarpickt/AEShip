using System.Collections.Generic;
using System.Linq;
using AEShip.Service.Exceptions;
using AEShip.Service.Interfaces;
using AEShip.Service.Models;
using AEShip.Service.Models.Requests;
using AEShip.Service.Models.Responses;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace AEShip.Service.Services
{
    public class ShipService: IShipService
    {
        private readonly IRepositoryService _repositoryService;

        public ShipService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public void AddShips(IEnumerable<NewShipRequest> ships)
        {
            var newShips = ships.Select(MapNewShipRequestToShip);

            _repositoryService.AddShips(newShips);
        }

        public void AddShip(NewShipRequest ship)
        {
            var newShip = MapNewShipRequestToShip(ship);
            _repositoryService.AddShip(newShip);
        }

        public void UpdateShipVelocity(string id, double velocity)
        {
            _repositoryService.UpdateShipVelocity(id, velocity);
        }

        public IEnumerable<Ship> GetAllShips()
        {
            return _repositoryService.GetAllShips();
        }

        public IEnumerable<Port> GetAllPorts()
        {
            return _repositoryService.GetAllPorts();
        }

        public ClosestPortResponse GetClosestPort(string id)
        {
            var ship = _repositoryService.GetShip(id);

            if(ship == null) throw new ShipNotFoundException(id);

            var closestPort = _repositoryService.GetClosestPort(ship.Location);

            var arrivalTimeInHours = closestPort.Distance / ship.Velocity;
            var port = closestPort.Port;

            return new ClosestPortResponse(port.Id, port.Name, port.Location.Y, port.Location.X, arrivalTimeInHours);
        }

        private Ship MapNewShipRequestToShip(NewShipRequest request)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            return new Ship
            {
                Id = request.Id,
                Name = request.Name,
                Location = geometryFactory.CreatePoint(new Coordinate(request.Latitude, request.Longitude)),
                Velocity = request.Velocity
            };
        }
    }
}
