using System.Collections.Generic;
using System.Linq;
using AEShip.Service.Exceptions;
using AEShip.Service.Interfaces;
using AEShip.Service.Models;
using NetTopologySuite.Geometries;

namespace AEShip.Service.Services
{
    public class RepositoryService: IRepositoryService
    {
        private readonly RepositoryContext _repositoryContext;

        public RepositoryService(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void AddPort(Port port)
        {
            if (_repositoryContext.Ports.Any(p => p.Id == port.Id)) return;

            _repositoryContext.Ports.Add(port);
            _repositoryContext.SaveChanges();
        }

        public void AddPorts(IEnumerable<Port> ports)
        {
            foreach (var port in ports)
            {
                if (!_repositoryContext.Ports.Any(p => p.Id == port.Id))
                {
                    _repositoryContext.Ports.Add(port);
                }
            }
            _repositoryContext.SaveChanges();
        }

        public IEnumerable<Port> GetAllPorts()
        {
            return _repositoryContext.Ports;
        }

        public Port GetPort(string id)
        {
            return _repositoryContext.Ports.Find(id);
        }

        public ClosestPort GetClosestPort(Point location)
        {
            var nearestPort = _repositoryContext.Ports
                .OrderBy(c => c.Location.Distance(location))
                .First();

            var distance = nearestPort.Location.Distance(location);

            return new ClosestPort(nearestPort, distance);
        }

        public void AddShip(Ship ship)
        {
            if (_repositoryContext.Ships.Any(s => s.Id == ship.Id)) return;

            _repositoryContext.Ships.Add(ship);
            _repositoryContext.SaveChanges();
        }

        public void AddShips(IEnumerable<Ship> ships)
        {
            foreach (var ship in ships)
            {
                if (!_repositoryContext.Ships.Any(s => s.Id == ship.Id))
                {
                    _repositoryContext.Ships.Add(ship);
                }
            }
            _repositoryContext.SaveChanges();
        }

        public void UpdateShipVelocity(string id, double velocity)
        {
            var ship = _repositoryContext.Ships.FirstOrDefault(s => s.Id == id);

            if (ship == null) throw new ShipNotFoundException(id);

            ship.Velocity = velocity;
            _repositoryContext.SaveChanges();
        }

        public IEnumerable<Ship> GetAllShips()
        {
            return _repositoryContext.Ships;
        }

        public Ship GetShip(string id)
        {
            return _repositoryContext.Ships.Find(id);
        }
    }
}
