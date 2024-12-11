namespace MyMicroservice.Exceptions
{
    public class NoParkingSlotException : Exception
    {
        public NoParkingSlotException(): base("No free parking slot available.")
        {
            
        }
    }
}
