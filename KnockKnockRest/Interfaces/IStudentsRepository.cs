using KnockKnockRest.Models;

namespace KnockKnockRest.Interfaces
{
    public interface IStudentsRepository
    {
        Student Add(Student newStudent);
        Student? Delete(int id);
        Student? DeleteByQr(int qr);
        List<Student> GetAll();
        Student? GetByID(int id);
        Student? Update(int id, Student updates);
        public Student GetByEmailAndPassword(string email, string password);
    }
}