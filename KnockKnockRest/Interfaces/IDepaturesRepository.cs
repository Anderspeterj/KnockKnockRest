using KnockKnockRest.Models;

namespace KnockKnockRest.Interfaces
{
    public interface IDepaturesRepository
    {
        Departure Add(Departure newDeparture);
        Departure? DeleteById(int id);
        List<Departure> GetAll();
        Departure? GetByID(int id);
        Departure? GetByQr(int qr);
    }
}