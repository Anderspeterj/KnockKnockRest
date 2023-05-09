using KnockKnockRest.Interfaces;
using KnockKnockRest.Models;
using System.Xml.Linq;

namespace KnockKnockRest.Repositories
{
    public class ArrivalsRepository : IArrivalsRepository
    {
        private int _nextID;
        private List<Arrival> _arrivals;

        public ArrivalsRepository()
        {
            _nextID = 1;
            _arrivals = new List<Arrival>()
            {

            };
        }

        public List<Arrival> GetAll()
        {
            return new List<Arrival>(_arrivals);
        }

        public Arrival? GetByID(int id)
        {
            return _arrivals.Find(x => x.Id == id);
        }

        public Arrival Add(Arrival newArrival)
        {
            newArrival.Id = _nextID++;
            _arrivals.Add(newArrival);
            return newArrival;
        }

        public Arrival? Delete(int id)
        {
            Arrival? foundArrival = GetByID(id);
            if (foundArrival == null)
            {
                return null;
            }
            _arrivals.Remove(foundArrival);
            return foundArrival;
        }

        public Arrival? Update(int id, Arrival updates)
        {
            Arrival? foundArrival = GetByID(id);
            if (foundArrival == null)
            {
                return null;
            }
            foundArrival.ArrivalTime = updates.ArrivalTime;
            return foundArrival;
        }
    }
}
