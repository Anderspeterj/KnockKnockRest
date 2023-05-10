using KnockKnockRest.Interfaces;
using KnockKnockRest.Models;
using KnockKnockRest.Context;

namespace KnockKnockRest.RepositoriesDB
    
{

    public class StudentsRepositoryDb : IStudentsRepository
    {
        private KnockKnockContext _context;

        public StudentsRepositoryDb(KnockKnockContext context)
        {
            _context = context;
        }

        public Student Add(Student newStudent)
        {
            _context.students.Add(newStudent);
            _context.SaveChanges();
            return newStudent;
        }

        public Student? Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            return _context.students.ToList();
        }

        public Student? GetByID(int id)
        {
            List<Student> result = _context.students.ToList();
            return result.Find(student => student.Id == id);
        }
        public Student? GetByQr(int qr)
        {
            List<Student> result = _context.students.ToList();
            return result.Find(student => student.QrCode == qr);
        }

        public Student? Update(int id, Student updates)
        {
            throw new NotImplementedException();
        }
    }
}
