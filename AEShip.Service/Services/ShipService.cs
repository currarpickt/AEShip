using System.Collections.Generic;
using System.Linq;
using AEShip.Service.Exceptions;
using AEShip.Service.Interfaces;
using AEShip.Service.Models;
using AEShip.Service.Models.Requests;
using AEShip.Service.Models.Responses;

namespace AEShip.Service.Services
{
    public class ShipService: IShipService
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IMapperService _mapperService;
        private readonly IShipUtilities _shipUtilities;

        public ShipService(IRepositoryService repositoryService,
            IMapperService mapperService,
            IShipUtilities shipUtilities)
        {
            _repositoryService = repositoryService;
            _mapperService = mapperService;
            _shipUtilities = shipUtilities;
        }

        public void AddShips(IEnumerable<NewShipRequest> ships)
        {
            var newShips = ships.Select(_mapperService.MapNewShipRequestToShip);

            _repositoryService.AddShips(newShips);
        }

        public void AddShip(NewShipRequest ship)
        {
            var newShip = _mapperService.MapNewShipRequestToShip(ship);
            _repositoryService.AddShip(newShip);
        }

        public void UpdateShipVelocity(string id, double velocity)
        {
            _repositoryService.UpdateShipVelocity(id, velocity);
        }

        public IEnumerable<ShipResponse> GetAllShips()
        {
            var ships = _repositoryService.GetAllShips();
            return _mapperService.MapShipsToResponse(ships);
        }

        public IEnumerable<PortResponse> GetAllPorts()
        {
            var ports = _repositoryService.GetAllPorts();
            return _mapperService.MapPortsToResponse(ports);
        }

        public ClosestPortResponse GetClosestPort(string id)
        {
            var ship = _repositoryService.GetShip(id);

            if(ship == null) throw new ShipNotFoundException(id);

            var ports = _repositoryService.GetAllPorts();

            var shipLocation = new GeoLocation(ship.Latitude, ship.Longitude);

            var nearestPort = (from p in ports
                let distance = _shipUtilities.GetDistance(shipLocation, new GeoLocation(p.Latitude, p.Longitude))
                orderby distance
                select new ClosestPort(p, distance)).First();
            
            var arrivalTimeInHours = nearestPort.Distance / ship.Velocity;
            var port = nearestPort.Port;

            return new ClosestPortResponse(port.Id, port.Name, port.Latitude, port.Longitude, arrivalTimeInHours);
        }
    }
}
