using Microsoft.EntityFrameworkCore;
using MyMicroservice.Data;
using MyMicroservice.Models.ParkingLot;

namespace MyMicroservice.Services
{
    public class VehicleRegisterService : IVehicleRegisterService
    {
        private readonly ParkingLotDBContext _dbContext;

        public VehicleRegisterService(ParkingLotDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> RegisterVehicle(Vehicle vehicle)
        {
            var existingVehicle = await _dbContext.Vehicles.FindAsync(vehicle.Id);
            if (existingVehicle != null)
            {
                throw new InvalidOperationException("Vehicle already exists");
            }
            _dbContext.Vehicles.Add(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle.Id;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task<Vehicle?> GetVehicle(int id)
        {
            return await _dbContext.Vehicles.FindAsync(id);
        }

        public async Task UpdateVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                throw new ArgumentException("ID mismatch");
            }

            _dbContext.Entry(vehicle).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteVehicle(int id)
        {
            var vehicle = await _dbContext.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                throw new ArgumentException("Vehicle not found");
            }

            _dbContext.Vehicles.Remove(vehicle);
            await _dbContext.SaveChangesAsync();
        }
    }
}
