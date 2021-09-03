using AEShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AEShip.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortsController: ControllerBase
    {
        private readonly IShipService _shipService;

        public PortsController(IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet]
        public IStatusCodeActionResult GetPorts()
        {
            var ports = _shipService.GetAllPorts();
            return Ok(ports);
        }
    }
}
