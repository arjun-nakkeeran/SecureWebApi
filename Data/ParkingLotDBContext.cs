using Microsoft.EntityFrameworkCore;
using MyMicroservice.Models.ParkingLot;

namespace MyMicroservice.Data
{
    public class ParkingLotDBContext : DbContext
    {
        public DbSet<ParkingUnit> ParkingUnits { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Location> Locations { get; set; }

        private string _dbPath;

        public ParkingLotDBContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            _dbPath = Path.Combine(path, "parkinglot.db");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }
}
