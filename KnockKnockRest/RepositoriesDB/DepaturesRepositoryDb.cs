using KnockKnockRest.Context;
using KnockKnockRest.Interfaces;
using KnockKnockRest.Models;
using KnockKnockRest.Repositories;

namespace KnockKnockRest.RepositoriesDB
{
    public class DepaturesRepositoryDb : IDepaturesRepository
    {
        private KnockKnockContext _context;

        public DepaturesRepositoryDb(KnockKnockContext context)
        {
            _context = context;
        }

        public Departure Add(Departure newDeparture)
        {
            foreach (var item in _context.students)
            {
                if (item.QrCode == newDeparture.QrCode)
                    newDeparture.Name = item.Name;
            }

            newDeparture.Validate();
            _context.departures.Add(newDeparture);
            _context.SaveChanges();
            return newDeparture;
        }

        public Departure? DeleteById(int id)
        {
            Departure? departureToBeDeleted = GetByID(id);
            if (departureToBeDeleted == null)
            {
                return null;
            }
            _context.departures.Remove(departureToBeDeleted);
            _context.SaveChanges();
            return departureToBeDeleted;
        }

        public List<Departure> GetAll()
        {
            return _context.departures.ToList();
        }

        public Departure? GetByID(int id)
        {
            List<Departure> result = _context.departures.ToList();
            return result.Find(departure => departure.Id == id);
        }

        public Departure? GetByQr(int qr)
        {
            List<Departure> result = _context.departures.ToList();
            return result.Find(departure => departure.QrCode == qr);
        }
    }
}
