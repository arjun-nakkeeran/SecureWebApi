namespace MyMicroservice.Models.ParkingLot
{
    public class ParkingUnit
    {
        public int Id { get; set; }
        public Location? Location { get; set; }
        public Vehicle? Vehicle { get; set; }
        public VehicleType UnitSize { get; set; }

        public bool IsEmpty => Vehicle == null;
    }
}
