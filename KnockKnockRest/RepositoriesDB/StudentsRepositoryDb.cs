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
            newStudent.Validate();
            _context.students.Add(newStudent);
            _context.SaveChanges();
            return newStudent;
        }

        public Student? Delete(int id)
        {
            Student? studentToBeDeleted = GetByID(id);
            if (studentToBeDeleted == null)
            {
                return null;
            }
            _context.students.Remove(studentToBeDeleted);
            _context.SaveChanges();
            return studentToBeDeleted;
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

        public Student GetByEmailAndPassword(string email, string password)
        {
            return _context.students.FirstOrDefault(student => student.Email == email && student.Password == password);
        }

        public Student? DeleteByQr(int qr)
        {
            Student? studentToBeDeleted = GetByQr(qr);
            if (studentToBeDeleted == null)
            {
                return null;
            }
            _context.students.Remove(studentToBeDeleted);
            _context.SaveChanges();
            return studentToBeDeleted;
        }
    }
}