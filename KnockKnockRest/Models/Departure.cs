namespace KnockKnockRest.Models
{
    public class Departure
    {
        public int Id { get; set; }
        public DateTime? DepartureTime { get; set; }
        public int? QrCode { get; set; }
        public string? Name { get; set; }

        public void ValidateDepartureTime()
        {
            throw new NotImplementedException();
        }

        public void ValidateQrCode()
        {
            throw new NotImplementedException();
        }

        public void ValidateName()
        {
            throw new NotImplementedException();
        }

        public void Validate()
        {
            ValidateDepartureTime();
            ValidateQrCode();
            ValidateName();
        }

        public override string ToString()
        {
            return $"Id: {Id}, ArrivalTime: {DepartureTime}, QrCode: {QrCode}, Name: {Name}";
        }
    }
}