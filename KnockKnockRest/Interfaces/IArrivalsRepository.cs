using KnockKnockRest.Models;

namespace KnockKnockRest.Interfaces
{
    public interface IArrivalsRepository
    {
        Arrival Add(Arrival newArrival);
        Arrival? Delete(int id);
        List<Arrival> GetAll();
        Arrival? GetByID(int id);
        Arrival? Update(int id, Arrival updates);
    }
}