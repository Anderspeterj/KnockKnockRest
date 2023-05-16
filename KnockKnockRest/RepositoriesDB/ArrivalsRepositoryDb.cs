using KnockKnockRest.Interfaces;
using KnockKnockRest.Models;
using KnockKnockRest.Context;

namespace KnockKnockRest.RepositoriesDB
{
    public class ArrivalsRepositoryDb : IArrivalsRepository
    {

        private KnockKnockContext _context;
      
        public ArrivalsRepositoryDb(KnockKnockContext context)
        {
            _context = context;
        }
        
        public Arrival Add(Arrival newArrival)
        {
            foreach (var item in _context.students)
            {
                if (item.QrCode == newArrival.QrCode)
                    newArrival.Name = item.Name;
            }
            
            newArrival.Validate();
            _context.arrivals.Add(newArrival);
            _context.SaveChanges();
            return newArrival;

            // Calculate absence percentage based on arrival time and subject
            //switch (newArrival.Subject)
            //{
            //    case "danish":
            //        newArrival.AbsencePercentage = CalculateAbsencePercentage(newArrival.ArrivalTime, TimeSpan.FromHours(8));
            //        break;
            //    case "math":
            //        newArrival.AbsencePercentage = CalculateAbsencePercentage(newArrival.ArrivalTime, TimeSpan.FromHours(9));
            //        break;
            //    case "science":
            //        newArrival.AbsencePercentage = CalculateAbsencePercentage(newArrival.ArrivalTime, TimeSpan.FromHours(10));
            //        break;
            //    case "spanish":
            //        newArrival.AbsencePercentage = CalculateAbsencePercentage(newArrival.ArrivalTime, TimeSpan.FromHours(12));
            //        break;
            //    default:
            //        newArrival.AbsencePercentage = 0;
            //        break;
            //}

            return newArrival;
        }

        private double CalculateAbsencePercentage(DateTime arrivalTime, TimeSpan classStartTime)
        {
            if (arrivalTime.TimeOfDay > classStartTime)
                return 1.0; // 100% absence if arrived after class start time
            else if (arrivalTime.TimeOfDay == classStartTime)
                return 0.0; // 0% absence if arrived exactly at class start time
            else
                return 0.25; // 25% absence for other cases (arrived before class start time)
        }

    

        public Arrival? Delete(int id)
        {
            Arrival? arrivalToBeDeleted = GetByID(id);
            if (arrivalToBeDeleted == null)
            {
                return null;
            }
            _context.arrivals.Remove(arrivalToBeDeleted);
            _context.SaveChanges();
            return arrivalToBeDeleted;
        }

        public List<Arrival> GetAll()
        {
            return _context.arrivals.ToList();
        }

        public Arrival? GetByID(int id)
        {
            List<Arrival> result = _context.arrivals.ToList();
            return result.Find(arrival => arrival.Id == id);
        }
        public Arrival? GetByQr(int qr)
        {
            List<Arrival> result = _context.arrivals.ToList();
            return result.Find(arrival => arrival.QrCode == qr);
        }

        public Arrival? Update(int id, Arrival updates)
        {
            throw new NotImplementedException();
        }
    }
}
