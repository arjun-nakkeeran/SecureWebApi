using Microsoft.EntityFrameworkCore;
using MyMicroservice.Data;
using MyMicroservice.Models.ParkingLot;

namespace MyMicroservice.Services
{
    public class ParkingLotInitializer
    {
       
        private readonly ParkingLotDBContext _dbContext;

        public ParkingLotInitializer(ParkingLotDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedData()
        {
            _dbContext.Database.EnsureCreated();

            if (!_dbContext.Locations.Any())
            {
                // Create and add initial parking lot data
                Enumerable.Range(1, 10).ToList().ForEach(i =>
                {
                    var location = new Location { Floor = 0, Row = 'A', SerialNumber = i };
                    _dbContext.Locations.Add(location);
                });
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.ParkingUnits.Any())
            {
                Random random = new Random();
                await _dbContext.Locations.ForEachAsync(location =>
                {
                    VehicleType type = ((VehicleType)random.Next(0, 2));
                    var parkingUnit = new ParkingUnit { Location = location, UnitSize = type };
                    _dbContext.ParkingUnits.Add(parkingUnit);
                });
            }
            var parkingSave = _dbContext.SaveChangesAsync();

            if (!_dbContext.Vehicles.Any())
            {
                // Add dummy vehicle
                var vehicle = new Vehicle { LicensePlate = "ABC123", Type = VehicleType.Car, ContactPhone = "12345678" };
                _dbContext.Vehicles.Add(vehicle);

            }
            var vehicleSave = _dbContext.SaveChangesAsync();


            await Task.WhenAll(parkingSave, vehicleSave);
        }
    }
}
