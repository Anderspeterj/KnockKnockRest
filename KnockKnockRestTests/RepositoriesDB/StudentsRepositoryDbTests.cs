using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnockKnockRest.Models;
using KnockKnockRest.RepositoriesDB;
using KnockKnockRest.Context;
using System.Diagnostics;

namespace KnockKnockRest.Tests.RepositoriesDB
{
    [TestClass]
    public class StudentsRepositoryDbTests
    {
        private KnockKnockContext _context;
        private StudentsRepositoryDb _repository;

        Student student = new Student { QrCode = 12345678, Name = "John Doe", Address = "123 Main St.", Email = "JohnD@hotmail.com", Password = "lol234"};
        Student student2 = new Student { QrCode = 87654321, Name = "Jane Smith", Address = "456 Elm St.", Email = "JaneSmith@mail.dk", Password = "39506" };
        Student student3 = new Student { QrCode = 12345678, Name = "Bob Johnson", Address = "789 Oak St.", Email = "bobby@bob.com", Password = "9849"};
        Student student4 = new Student { QrCode = 11111111, Name = "Alice Brown", Address = "999 Maple St.", Email ="alice@mail.com", Password = "123"};

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<KnockKnockContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            _context = new KnockKnockContext(options);

            _repository = new StudentsRepositoryDb(_context);
        }

        [TestMethod]
        public void Add_NewStudent_ReturnsSameStudent()
        {
            var result = _repository.Add(student);
            var result2 = _repository.Add(student2);

            Assert.IsNotNull(result);
            Assert.AreEqual(student, result);
            Assert.AreEqual(student, _context.students.Find(1));
            Assert.AreEqual(student2, _context.students.Find(2));

            Debug.WriteLine("Contents of the database:");
            for (int i = 0; i <= _context.students.Count(); i++)
            {
                Debug.WriteLine(_context.students.Find(i));
            }
        }

        [TestMethod]
        public void GetById_GetsTheRightId()
        {
            _context.Database.EnsureDeleted();
            _repository.Add(student);
            _repository.Add(student2);

            Assert.AreEqual(_repository.GetByID(2), student2);

            Debug.WriteLine("Contents of the database:");
            for (int i = 0; i <= _context.students.Count(); i++)
            {
                Debug.WriteLine(_context.students.Find(i));
            }
        }

        [TestMethod]
        public void GetByQr_GetsTheRightQr()
        {
            _context.Database.EnsureDeleted();
            _repository.Add(student);
            _repository.Add(student2);

            Assert.AreEqual(_repository.GetByQr(87654321), student2);

            Debug.WriteLine("Contents of the database:");
            for (int i = 0; i <= _context.students.Count(); i++)
            {
                Debug.WriteLine(_context.students.Find(i));
            }
        }

        [TestMethod]
        public void GetAll_GetsAll()
        {
            _context.Database.EnsureDeleted();
            _repository.Add(student);
            _repository.Add(student2);
            _repository.Add(student3);
            _repository.Add(student4);

            Assert.AreEqual(4, _repository.GetAll().Count);

            Debug.WriteLine("Contents of the database:");
            for (int i = 0; i <= _context.students.Count(); i++)
            {
                Debug.WriteLine(_context.students.Find(i));
            }
        }
    }
}