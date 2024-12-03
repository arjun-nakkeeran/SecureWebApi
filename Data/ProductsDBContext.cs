using Microsoft.EntityFrameworkCore;
using MyMicroservice.Models;

namespace MyMicroservice.Data
{
    public class ProductsDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        private string _dbPath;

        public ProductsDBContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            _dbPath = Path.Combine(path, "products.db");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }
}
