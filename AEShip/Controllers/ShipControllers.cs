using System.Collections.Generic;
using AEShip.Service.Exceptions;
using AEShip.Service.Interfaces;
using AEShip.Service.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AEShip.Controllers
{
    [ApiController]
    [Route("api/ship")]
    public class ShipControllers: ControllerBase
    {
        private readonly IShipService _shipService;

        public ShipControllers(IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet("ports")]
        public IStatusCodeActionResult GetPorts()
        {
            var ports = _shipService.GetAllPorts();
            return Ok(ports);
        }

        [HttpPost("add")]
        public IStatusCodeActionResult AddShip(NewShipRequest request)
        {
            _shipService.AddShip(request);
            return Ok();
        }

        [HttpPost("addmultiple")]
        public IStatusCodeActionResult AddShips(IEnumerable<NewShipRequest> requests)
        {
            _shipService.AddShips(requests);
            return Ok();
        }

        [HttpGet("view")]
        public IStatusCodeActionResult GetAllShips()
        {
            var allShips = _shipService.GetAllShips();
            return Ok(allShips);
        }

        [HttpPut("updatevelocity")]
        public IStatusCodeActionResult UpdateShipVelocity(UpdateVelocityRequest request)
        {
            try
            {
                _shipService.UpdateShipVelocity(request.ShipId, request.Velocity);
                return Ok();
            }
            catch (ShipNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    Message = $"No ship found with Id:{ex.Id}"
                });
            }
        }

        [HttpGet("closestport")]
        public IStatusCodeActionResult GetClosestPort([FromQuery] string id)
        {
            try
            {
                var closestPort = _shipService.GetClosestPort(id);
                return Ok(closestPort);
            }
            catch (ShipNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    Message = $"No ship found with Id:{ex.Id}"
                });
            }
        }
    }
}
