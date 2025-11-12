using System.Collections.Generic;
using System.Threading.Tasks;
using MyMicroservice.Models.ParkingLot;

namespace MyMicroservice.Services
{
    public interface IParkingLotService
    {
        Task VehicleEntry(int vehicleId);
        Task VehicleExit(int vehicleId);
        Task<IEnumerable<ParkingUnit>> GetParkingUnits();
    }
}
