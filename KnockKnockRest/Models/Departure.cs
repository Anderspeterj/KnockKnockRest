namespace KnockKnockRest.Models
{
    public class Departure
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public int? QrCode { get; set; }
        public string? Name { get; set; }

        public void ValidateDepartureTime()
        {
            var departureTimeThreshold = DateTime.Now.AddHours(-25);
            if (DepartureTime < departureTimeThreshold)
            {
                throw new ArgumentException("Departure time is too far in the past");
            }
        }

        public void ValidateQrCode()
        {
           if(QrCode == null)
            {
                throw new ArgumentNullException("QR Code can't be null!");
            }

            if (QrCode > 99999999 || QrCode < 10000000)
            {
                throw new ArgumentOutOfRangeException("QrCode must have exactly 8 digits");
            }

        }

        public void ValidateName()
        {
            if (Name == null)
            {
                throw new ArgumentNullException("Name can't be null");
            }
        }

        public void Validate()
        {
            //ValidateDepartureTime();
            ValidateQrCode();
            ValidateName();
        }

        public override string ToString()
        {
            return $"Id: {Id}, ArrivalTime: {DepartureTime}, QrCode: {QrCode}, Name: {Name}";
        }
    }
}