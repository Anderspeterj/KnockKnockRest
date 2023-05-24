using KnockKnockRest.Models;
using KnockKnockRest.Context;
using KnockKnockRest.Interfaces;

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

            newArrival.ArrivalTime = DateTime.Now;
            newArrival.Validate();
            _context.arrivals.Add(newArrival);
            _context.SaveChanges();
            return newArrival;
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