using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnockKnockRest.RepositoriesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnockKnockRest.Context;
using KnockKnockRest.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KnockKnockRest.RepositoriesDB.Tests
{
    [TestClass()]
    public class DepaturesRepositoryDbTests
    {
        private KnockKnockContext _context;
        private DepaturesRepositoryDb _repository;
        private StudentsRepositoryDb _studentRepo;

        Student student = new Student { Name = "Jakob Test", Address = "Testvej 9", QrCode = 87878787, Email = "JakobTheMan@gmail.com", Password = "fido12" };
        Student student2 = new Student { Name = "Christian Test", Address = "Testvej 46", QrCode = 23546745, Email = "Chrisser@hotmail.com", Password = "93056mor" };
        Student student3 = new Student { Name = "Birk Test", Address = "Testvej 34", QrCode = 67567900, Email = "birk.mail@mail.com", Password = "okjegsej123" };
        Student student4 = new Student { Name = "Jonathan Test", Address = "Testvej 22", QrCode = 23746532, Email = "JonathanSpang@gmail.com", Password = "grisen123" };
        Departure departure = new Departure { QrCode = 87878787, DepartureTime = DateTime.Now };
        Departure departure2 = new Departure { QrCode = 23546745, DepartureTime = DateTime.Now };
        Departure departure3 = new Departure { QrCode = 67567900, DepartureTime = DateTime.Now };
        Departure departure4 = new Departure { QrCode = 23746532, DepartureTime = DateTime.Now };

        public void ContentOfDB()
        {
            //Debug.WriteLine("Contents of the database:");
            //for (int i = 0; i <= _context.departures.Count(); i++)
            //{
            //    Debug.WriteLine(_context.departures.Find(i));
            //}
            Debug.WriteLine("Contents of the database:");
            foreach (var item in _repository.GetAll())
            {
                Debug.WriteLine(item);
            }   
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<KnockKnockContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            _context = new KnockKnockContext(options);

            _repository = new DepaturesRepositoryDb(_context);
            _studentRepo = new StudentsRepositoryDb(_context);
            _studentRepo.Add(student);
            _studentRepo.Add(student2);
            _studentRepo.Add(student3);
            _studentRepo.Add(student4);
            _repository.Add(departure);
            _repository.Add(departure2);
            _repository.Add(departure3);
            _repository.Add(departure4);
            _context.SaveChanges();
        }

        [TestMethod()]
        public void AddTest()
        {
            Departure newDeparture  = new Departure { QrCode = 23746532, DepartureTime = DateTime.Now };
            int listLength = _repository.GetAll().Count;
            _repository.Add(newDeparture);
            Assert.AreEqual(listLength + 1, _repository.GetAll().Count);
            Assert.IsNotNull(_repository.GetByQr(23746532));
            Assert.AreEqual(23746532, _repository.GetByID(newDeparture.Id)?.QrCode);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.IsNotNull(_repository.GetByID(3));
            _repository.DeleteById(departure3.Id);
            Assert.IsNull(_repository.GetByID(3));
            
        }

        [TestMethod()]
        public void GetAllTest()
        {
            Assert.AreEqual(4, _repository.GetAll().Count);
        }

        [TestMethod()]
        public void GetByIDTest()
        {
            Assert.AreEqual(_repository.GetByID(2), departure2);
        }

        [TestMethod()]
        public void GetByQrTest()
        {
            Assert.AreEqual(_repository.GetByQr(67567900), departure3);
        }

        [TestMethod]
        public void DepartureGetRightName()
        {
            Student studentTest = new Student { Address = "Testvej2", Name = "Mike Test", QrCode = 66746421, Email = "test@gmail.com", Password = "2948" };
            Departure departureTest = new Departure { QrCode = 66746421, DepartureTime = DateTime.Now };
            _studentRepo.Add(studentTest);
            _repository.Add(departureTest);
            Assert.AreEqual(studentTest.Name, departureTest.Name);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ContentOfDB();
            _context.Database.EnsureDeleted();
        }
    }
}