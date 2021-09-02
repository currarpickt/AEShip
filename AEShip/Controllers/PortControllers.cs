using AEShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AEShip.Controllers
{
    [ApiController]
    [Route("api/port")]
    public class PortControllers: ControllerBase
    {
        private readonly IShipService _shipService;

        public PortControllers(IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet("view")]
        public IStatusCodeActionResult GetPorts()
        {
            var ports = _shipService.GetAllPorts();
            return Ok(ports);
        }
    }
}
