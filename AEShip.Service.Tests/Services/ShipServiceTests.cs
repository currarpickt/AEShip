using System.Collections.Generic;
using System.Linq;
using AEShip.Service.Exceptions;
using AEShip.Service.Interfaces;
using AEShip.Service.Models;
using AEShip.Service.Models.Requests;
using AEShip.Service.Models.Responses;
using AEShip.Service.Services;
using AutoFixture.Xunit2;
using Moq;
using Xunit;

namespace AEShip.Service.Tests.Services
{
    public class ShipServiceTests
    {
        [Theory, AutoMoqData]
        public void IsAssignableFromIShipService(ShipService sut)
        {
            Assert.IsAssignableFrom<IShipService>(sut);
        }

        [Theory, AutoMoqData]
        public void AddShips_CallsRepositoryServiceAddShips(
            List<NewShipRequest> shipRequest,
            Ship ship,
            [Frozen] Mock<IMapperService> mockMapperService,
            [Frozen] Mock<IRepositoryService> mockRepositoryService,
            ShipService sut)
        {
            mockMapperService
                .Setup(m =>
                    m.MapNewShipRequestToShip(It.IsAny<NewShipRequest>()))
                .Returns(ship);

            mockRepositoryService.Setup(
                m => m.AddShips(It.IsAny<IEnumerable<Ship>>())).Verifiable();

            sut.AddShips(shipRequest);

            mockRepositoryService.Verify(
                m => m.AddShips(It.IsAny<IEnumerable<Ship>>()), Times.Once);
        }

        [Theory, AutoMoqData]
        public void AddShip_CallsRepositoryServiceAddShip(
            NewShipRequest shipRequest,
            Ship ship,
            [Frozen] Mock<IMapperService> mockMapperService,
            [Frozen] Mock<IRepositoryService> mockRepositoryService,
            ShipService sut)
        {
            mockMapperService
                .Setup(m =>
                    m.MapNewShipRequestToShip(It.IsAny<NewShipRequest>()))
                .Returns(ship);

            mockRepositoryService.Setup(
                m => m.AddShip(ship)).Verifiable();

            sut.AddShip(shipRequest);

            mockRepositoryService.Verify(
                m => m.AddShip(ship), Times.Once);
        }

        [Theory, AutoMoqData]
        public void UpdateShipVelocity_ThrowsException_WhenShipIdNotExist(string shipId,
            double velocity,
            [Frozen] Mock<IRepositoryService> mockRepositoryService,
            ShipService sut)
        {
            mockRepositoryService
                .Setup(m => m.UpdateShipVelocity(shipId, velocity))
                .Throws(new ShipNotFoundException(shipId));

            var ex = Record.Exception(() => sut.UpdateShipVelocity(shipId, velocity));

            Assert.NotNull(ex);
            Assert.IsType<ShipNotFoundException>(ex);
        }

        [Theory, AutoMoqData]
        public void UpdateShipVelocity_CallsRepositoryServiceUpdateShipVelocity(string shipId,
            double velocity,
            [Frozen] Mock<IRepositoryService> mockRepositoryService,
            ShipService sut)
        {
            mockRepositoryService
                .Setup(m => m.UpdateShipVelocity(shipId, velocity))
                .Verifiable();

            var ex = Record.Exception(() => sut.UpdateShipVelocity(shipId, velocity));

            mockRepositoryService.Verify(
                m => m.UpdateShipVelocity(shipId, velocity), Times.Once);
        }

        [Theory, AutoMoqData]
        public void GetAllShips_ReturnsExpectedValue(
            List<Ship> ships,
            List<ShipResponse> shipResponses,
            [Frozen] Mock<IMapperService> mockMapperService,
            [Frozen] Mock<IRepositoryService> mockRepositoryService,
            ShipService sut)
        {
            mockRepositoryService.Setup(
                m => m.GetAllShips()).Returns(ships);

            mockMapperService
                .Setup(m =>
                    m.MapShipsToResponse(ships))
                .Returns(shipResponses);

            var result = sut.GetAllShips().ToList();

            Assert.NotNull(result);
            Assert.Equal(shipResponses.Count, result.Count);
            Assert.Equal(shipResponses, result);
        }

        [Theory, AutoMoqData]
        public void GetAllPorts_ReturnsExpectedValue(
            List<Port> ports,
            List<PortResponse> portResponses,
            [Frozen] Mock<IMapperService> mockMapperService,
            [Frozen] Mock<IRepositoryService> mockRepositoryService,
            ShipService sut)
        {
            mockRepositoryService.Setup(
                m => m.GetAllPorts()).Returns(ports);

            mockMapperService
                .Setup(m =>
                    m.MapPortsToResponse(ports))
                .Returns(portResponses);

            var result = sut.GetAllPorts().ToList();

            Assert.NotNull(result);
            Assert.Equal(portResponses.Count, result.Count);
            Assert.Equal(portResponses, result);
        }

        [Theory, AutoMoqData]
        public void GetClosestPort_ThrowsException_WhenShipIdNotFound(
            string shipId,
            [Frozen] Mock<IRepositoryService> mockRepositoryService,
            ShipService sut)
        {
            mockRepositoryService
                .Setup(m => m.GetShip(shipId))
                .Throws(new ShipNotFoundException(shipId));

            var ex = Record.Exception(() => sut.GetClosestPort(shipId));

            Assert.NotNull(ex);
            Assert.IsType<ShipNotFoundException>(ex);
        }

        [Theory, AutoMoqData]
        public void GetClosestPort_ReturnsExpectedValue(
            string shipId,
            double distance,
            Ship ship,
            List<Port> ports,
            [Frozen] Mock<IShipUtilities> mockShipUtilities,
            [Frozen] Mock<IRepositoryService> mockRepositoryService,
            ShipService sut)
        {
            mockRepositoryService
                .Setup(m => m.GetShip(shipId))
                .Returns(ship);

            mockRepositoryService.Setup(m => m.GetAllPorts()).Returns(ports);

            mockShipUtilities.Setup(m =>
                m.GetDistance(
                    It.Is<GeoLocation>(x =>
                        x.Latitude.Equals(ship.Latitude) &&
                        x.Longitude.Equals(ship.Longitude)),
                    It.IsAny<GeoLocation>())).Returns(distance);

            var expectedArrivalTimeInHours = distance / ship.Velocity;

            var result = sut.GetClosestPort(shipId);

            Assert.NotNull(result);
            Assert.Equal(expectedArrivalTimeInHours, result.EstimatedArrivalTimeInHours);
        }
    }
}
