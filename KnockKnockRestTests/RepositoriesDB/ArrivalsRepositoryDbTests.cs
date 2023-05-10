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
        

        Arrival arrival = new Arrival { QrCode = 44523245, ArrivalTime = DateTime.Now };
        Arrival arrival2 = new Arrival { QrCode = 25755633, ArrivalTime = DateTime.Now };
        Arrival arrival3 = new Arrival { QrCode = 44523245, ArrivalTime = DateTime.Now };
        Arrival arrival4 = new Arrival { QrCode = 24658724, ArrivalTime = DateTime.Now };

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


            
        }
        [TestMethod]
        public void Add_NewArrival_ReturnsSameArrival()
        {
            var result = _repository.Add(arrival);
            var result2 = _repository.Add(arrival2);
     
            Assert.IsNotNull(result);
            Assert.AreEqual(arrival, result);
            Assert.AreEqual(arrival, _context.arrivals.Find(1));
            Assert.AreEqual(arrival2, _context.arrivals.Find(2));
            Debug.WriteLine("Contents of the database:");
            for (int i = 0; i <= _context.arrivals.Count(); i++)
            {
                Debug.WriteLine(_context.arrivals.Find(i));
            }          
        }
        [TestMethod]
        public void GetById_GetsTheRightId()
        {
            _context.Database.EnsureDeleted();
            _repository.Add(arrival);
            _repository.Add(arrival2);
            Assert.AreEqual(_repository.GetByID(2), arrival2);
            Debug.WriteLine("Contents of the database:");
            for (int i = 0; i <= _context.arrivals.Count(); i++)
            {
                Debug.WriteLine(_context.arrivals.Find(i));
            }
        }
        [TestMethod]
        public void GetByQr_GetsTheRightQr()
        {
            _context.Database.EnsureDeleted();
            _repository.Add(arrival);
            _repository.Add(arrival2);
            Assert.AreEqual(_repository.GetByQr(25755633), arrival2);
            Debug.WriteLine("Contents of the database:");
            for (int i = 0; i <= _context.arrivals.Count(); i++)
            {
                Debug.WriteLine(_context.arrivals.Find(i));
            }
        }
        [TestMethod]
        public void GetAll_GetsAll()
        {
            _context.Database.EnsureDeleted();
            _repository.Add(arrival);
            _repository.Add(arrival2);
            _repository.Add(arrival3);
            _repository.Add(arrival4);
            Assert.AreEqual(4, _repository.GetAll().Count);
            Debug.WriteLine("Contents of the database:");
            for (int i = 0; i <= _context.arrivals.Count(); i++)
            {
                Debug.WriteLine(_context.arrivals.Find(i));
            }
        }

    }
}