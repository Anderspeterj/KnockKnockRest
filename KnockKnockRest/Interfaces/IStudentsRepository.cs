using KnockKnockRest.Models;

namespace KnockKnockRest.Interfaces
{
    public interface IStudentsRepository
    {
        Student Add(Student newStudent);
        Student? Delete(int id);
        List<Student> GetAll();
        Student? GetByID(int id);
        Student? Update(int id, Student updates);
    }
}