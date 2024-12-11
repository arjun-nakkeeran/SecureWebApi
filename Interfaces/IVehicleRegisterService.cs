using System.Collections.Generic;
using System.Threading.Tasks;
using MyMicroservice.Models.ParkingLot;

namespace MyMicroservice.Services
{
    public interface IVehicleRegisterService
    {
        Task<int> RegisterVehicle(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> GetVehicles();
        Task<Vehicle?> GetVehicle(int id);
        Task UpdateVehicle(int id, Vehicle vehicle);
        Task DeleteVehicle(int id);
    }
}
