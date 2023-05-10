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
    public class ArrivalsRepositoryDbTests
    {
        private KnockKnockContext _context;
        private ArrivalsRepositoryDb _repository;
        private StudentsRepositoryDb _studentRepo;

        Student student = new Student { Name = "Jakob Test", Address = "Testvej 9", QrCode = 87878787 };
        Student student2 = new Student { Name = "Christian Test", Address = "Testvej 46", QrCode = 23546745 };
        Student student3 = new Student { Name = "Birk Test", Address = "Testvej 34", QrCode = 67567900 };
        Student student4 = new Student { Name = "Jonathan Test", Address = "Testvej 22", QrCode = 23746532 };
        Arrival arrival = new Arrival { QrCode = 87878787, ArrivalTime = DateTime.Now };
        Arrival arrival2 = new Arrival { QrCode = 23546745, ArrivalTime = DateTime.Now };
        Arrival arrival3 = new Arrival { QrCode = 67567900, ArrivalTime = DateTime.Now };
        Arrival arrival4 = new Arrival { QrCode = 23746532, ArrivalTime = DateTime.Now };

        public void ContentOfDB()
        {
            Debug.WriteLine("Contents of the database:");
            for (int i = 0; i <= _context.arrivals.Count(); i++)
            {
                Debug.WriteLine(_context.arrivals.Find(i));
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {

            // Create an instance of DbContextOptions<KnockKnockContext> using the UseInMemoryDatabase method
            var options = new DbContextOptionsBuilder<KnockKnockContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            // Create an instance of KnockKnockContext using the DbContextOptions<KnockKnockContext> instance
            _context = new KnockKnockContext(options);

            // Create an instance of ArrivalsRepositoryDb using the KnockKnockContext instance
            _repository = new ArrivalsRepositoryDb(_context);
            _studentRepo = new StudentsRepositoryDb(_context);
            _studentRepo.Add(student);
            _studentRepo.Add(student2);
            _studentRepo.Add(student3);
            _studentRepo.Add(student4);
            _repository.Add(arrival);
            _repository.Add(arrival2);
            _repository.Add(arrival3);
            _repository.Add(arrival4);
            _context.SaveChanges();
        }


        [TestMethod]
        public void Add_NewArrival_ReturnsSameArrival()
        {
            Arrival arrival5 = new Arrival { QrCode = 78976778, ArrivalTime = DateTime.Now ,Name = "TestNavn"};

            var result = _repository.Add(arrival5);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(arrival5, result);
            Assert.AreEqual(arrival5, _context.arrivals.Find(5));
            
        }
        [TestMethod]
        public void GetById_GetsTheRightId()
        {
            Assert.AreEqual(_repository.GetByID(2), arrival2);
        }
        [TestMethod]
        public void GetByQr_GetsTheRightQr()
        {
            Assert.AreEqual(_repository.GetByQr(67567900), arrival3);

        }
        [TestMethod]
        public void GetAll_GetsAll()
        {       
            Assert.AreEqual(4, _repository.GetAll().Count);
        }
        [TestMethod]
        public void ArrivalGetRightName()
        {
            Student studentTest = new Student { Address = "Testvej2", Name = "Mike Test", QrCode = 66746421 };
            Arrival arrivalTest = new Arrival { QrCode = 66746421, ArrivalTime = DateTime.Now };
            _studentRepo.Add(studentTest);
            _repository.Add(arrivalTest);
            Assert.AreEqual(studentTest.Name, arrivalTest.Name);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ContentOfDB();
            _context.Database.EnsureDeleted();
        }


    }
}