using Microsoft.EntityFrameworkCore;
using MyMicroservice.Data;
using MyMicroservice.Exceptions;
using MyMicroservice.Models.ParkingLot;

namespace MyMicroservice.Services
{
    public class ParkingLotService : IParkingLotService
    {
        private readonly ParkingLotDBContext _parkingLotDBContext;

        public ParkingLotService(ParkingLotDBContext parkingLotDBContext)
        {
            _parkingLotDBContext = parkingLotDBContext;
        }

        public async Task VehicleEntry(int vehicleId)
        {
            var vehicle = await _parkingLotDBContext.Vehicles.FindAsync(vehicleId);
            if (vehicle == null)
            {
                throw new InvalidOperationException("Vehicle not found");
            }
            var parkingUnit = await GetFirstAvailableParking(vehicle.Type);

            if (parkingUnit == null)
            {
                throw new NoParkingSlotException();
            }

            parkingUnit.Vehicle = vehicle;
            await _parkingLotDBContext.SaveChangesAsync();
        }

        public async Task VehicleExit(int vehicleId)
        {
            var parkingUnit = await _parkingLotDBContext.ParkingUnits
                .Include(p => p.Location)
                .FirstOrDefaultAsync(p => p.Vehicle.Id == vehicleId);

            if (parkingUnit == null)
            {
                throw new InvalidOperationException("Vehicle not found");
            }

            parkingUnit.Vehicle = null;
            await _parkingLotDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ParkingUnit>> GetParkingUnits()
        {
            return await _parkingLotDBContext.ParkingUnits
                .Include(p => p.Location)
                .Include(p => p.Vehicle)
                .ToListAsync();
        }

        private async Task<ParkingUnit?> GetFirstAvailableParking(VehicleType vehicleType)
        {
            return await _parkingLotDBContext.ParkingUnits
                .Include(p => p.Location)
                .FirstOrDefaultAsync(p => p.Vehicle == null && p.UnitSize == vehicleType);
        }
    }
}
