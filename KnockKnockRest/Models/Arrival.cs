using System.Globalization;

namespace KnockKnockRest.Models
{
    public class Arrival
    {
        public int Id { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public int QrCode { get; set; }
        public string Name { get; set; }

        public string Test { get; set; }


        public void ValidateArrivalTime()
        {
            
            if (!ArrivalTime.HasValue)
            {
                throw new InvalidOperationException("Arrival time must be provided");
            }

            if (ArrivalTime.Value > DateTime.Now)
            {
                throw new InvalidOperationException("Arrival time cannot be in the future");
            }

            var arrivalTimeThreshold = DateTime.Now.AddHours(-24);
            if (ArrivalTime.Value < arrivalTimeThreshold)
            {
                throw new InvalidOperationException("Arrival time is too far in the past");
            }
        }


        public void ValidateQrCode()
        {
            if (QrCode == 0)
            {
                throw new ArgumentNullException("QrCode cannot be null");
            }
            if (QrCode > 99999999 || QrCode < 10000000)
            {
                throw new ArgumentException("QrCode must have exactly 8 digits");
            }

        }
        public void Validate()
        {
            ValidateArrivalTime();
            ValidateQrCode();
        }
        public override string ToString()
        {
            return $"Id: {Id}, ArrivalTime: {ArrivalTime}, QrCode: {QrCode}, Name: {Name}";
        }

    }


}
