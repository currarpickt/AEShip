using System.Collections.Generic;
using System.Linq;
using AEShip.Service.Services;
using AEShip.Service.Interfaces;
using AEShip.Service.Models;
using AEShip.Service.Models.Requests;
using Xunit;

namespace AEShip.Service.Tests.Services
{
    public class MapperServiceTests
    {
        [Theory, AutoMoqData]
        public void IsAssignableFromIMapperService(MapperService sut)
        {
            Assert.IsAssignableFrom<IMapperService>(sut);
        }

        [Theory, AutoMoqData]
        public void MapShipsToResponse_ReturnsExpectedValue(
            List<Ship> ships,
            MapperService sut)
        {
            var result = sut.MapShipsToResponse(ships);

            Assert.Equal(ships.Count, result.Count());
        }

        [Theory, AutoMoqData]
        public void MapPortsToResponse_ReturnsExpectedValue(
            List<Port> ports,
            MapperService sut)
        {
            var result = sut.MapPortsToResponse(ports);

            Assert.Equal(ports.Count, result.Count());
        }

        [Theory, AutoMoqData]
        public void MapNewShipRequestToShip_ReturnsExpectedValue(
            NewShipRequest request,
            MapperService sut)
        {
            var result = sut.MapNewShipRequestToShip(request);

            Assert.Equal(request.Id, result.Id);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Latitude, result.Latitude);
            Assert.Equal(request.Longitude, result.Longitude);
            Assert.Equal(request.Velocity, result.Velocity);
        }
    }
}
