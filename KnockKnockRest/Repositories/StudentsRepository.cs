using KnockKnockRest.Models;
using System.Collections.Generic;
using KnockKnockRest.Interfaces;

namespace KnockKnockRest.Repositories

{
    public class StudentsRepository : IStudentsRepository
    {
        private int _nextID;
        private List<Student> _Students;

        public StudentsRepository()
        {
            _nextID = 1;
            _Students = new List<Student>();
            {

            }
        }

        public List<Student> GetAll()
        {
            return new List<Student>(_Students);
        }

        public Student? GetByID(int id)
        {
            return _Students.Find(x => x.Id == id);
        }

        public Student Add(Student newStudent)
        {
            newStudent.Id = _nextID++;
            _Students.Add(newStudent);
            return newStudent;
        }

        public Student? Delete(int id)
        {
            Student? foundStudent = GetByID(id);
            if (foundStudent == null)
            {
                return null;
            }
            _Students.Remove(foundStudent);
            return foundStudent;
        }

        public Student? Update(int id, Student updates)
        {
            Student? foundStudent = GetByID(id);
            if (foundStudent == null)
            {
                return null;
            }
            foundStudent.Name = updates.Name;
            foundStudent.Address = updates.Address;
            foundStudent.QrCode = updates.QrCode;
            return foundStudent;
        }

        public Student? GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Student GetByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}

