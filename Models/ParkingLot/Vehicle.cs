namespace MyMicroservice.Models.ParkingLot
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string ContactPhone { get; set; } = string.Empty;
        public string? LicensePlate { get; set; }
        public VehicleType Type { get; set; }
    }
}
