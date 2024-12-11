using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMicroservice.Exceptions;
using MyMicroservice.Models.ParkingLot;
using MyMicroservice.Services;

namespace MyMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingLotController : ControllerBase
    {
        private readonly IParkingLotService _parkingLotService;
        private readonly IVehicleRegisterService _vehicleRegisterService;

        public ParkingLotController(IParkingLotService parkingLotService, IVehicleRegisterService vehicleRegisterService)
        {
            _parkingLotService = parkingLotService;
            _vehicleRegisterService = vehicleRegisterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingUnit>>> GetParkingLots()
        {
            var parkingLots = await _parkingLotService.GetParkingUnits();
            return Ok(parkingLots);
        }

        [HttpGet("{vehicleId}")]
        public async Task<Vehicle> GetVehicleAsync(int vehicleId)
        {
            var vehicle = await _vehicleRegisterService.GetVehicle(vehicleId);
            return vehicle;
        }

        [HttpPost("register")]
        public async Task<int> RegisterVehicle([FromBody] Vehicle vehicle)
        {
            var id = await _vehicleRegisterService.RegisterVehicle(vehicle);
            return id;
        }

        [HttpPost("{vehicleId}/park")]
        public async Task<bool> ParkVehicle(int vehicleId)
        {
            try
            {
                await _parkingLotService.VehicleEntry(vehicleId);
                return true;
            }
            catch (NoParkingSlotException)
            {
                return false;
            }
        }

        [HttpPost("{vehicleId}/unpark")]
        public async Task UnparkVehicle(int vehicleId)
        {
            await _parkingLotService.VehicleExit(vehicleId);
        }
    }
}
