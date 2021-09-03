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
    [Route("api/[controller]")]
    public class ShipsController: ControllerBase
    {
        private readonly IShipService _shipService;

        public ShipsController(IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpPost]
        public IStatusCodeActionResult AddShips(IEnumerable<NewShipRequest> requests)
        {
            _shipService.AddShips(requests);
            return Ok();
        }

        [HttpGet]
        public IStatusCodeActionResult GetAllShips()
        {
            var allShips = _shipService.GetAllShips();
            return Ok(allShips);
        }

        [HttpPut("{id}")]
        public IStatusCodeActionResult UpdateShipVelocity(string id, [FromBody] UpdateVelocityRequest request)
        {
            try
            {
                _shipService.UpdateShipVelocity(id, request.Velocity);
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

        [HttpGet("nearestPort/{id}")]
        public IStatusCodeActionResult GetClosestPort(string id)
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
