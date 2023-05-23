using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnockKnockRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnockKnockRest.Models.Tests
{
    [TestClass()]
    public class DepartureTests
    {
        Departure departureClean = new Departure()
        {
            DepartureTime = DateTime.Now,
            QrCode = 58472328,
            Name = "Lasse"
            
        };

        Departure departureFail = new Departure()
        {
            QrCode = 123456789,
            Name = null
        };

        [TestMethod()]
        public void ValidateDepartureTimeTest()
        {
            
            
            departureFail.DepartureTime = DateTime.Now.AddHours(-25);
            Assert.ThrowsException<ArgumentException>(() => departureFail.ValidateDepartureTime());
            departureClean.ValidateDepartureTime();
        }

        [TestMethod()]
        public void ValidateNameTest()
        {
            var typeOfName = departureClean.Name.GetType();
            Assert.AreEqual(typeof(string), typeOfName);

            Assert.ThrowsException<ArgumentNullException>(() => departureFail.ValidateName());
        }

        [TestMethod()]
        public void ValidateQrCodeTest()
        {
            departureFail.QrCode = null;
            Assert.ThrowsException<ArgumentNullException>(() => departureFail.ValidateQrCode());
            departureFail.QrCode = 1234;
            Assert.ThrowsException<ArgumentOutOfRangeException>(()=> departureFail.ValidateQrCode());
            departureFail.QrCode = 123456789;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => departureFail.ValidateQrCode());

            departureClean.ValidateQrCode();
        }

        [TestMethod()]
        public void ValidateTest()
        {
            Departure departure = new Departure() { Id = 1, DepartureTime = DateTime.Now, QrCode = 12345678, Name = "Ole Jensen" };
            departure.Validate();
            Departure departureTimeNotOK = new Departure() { Id = 1, DepartureTime = DateTime.Now.AddHours(-25), QrCode = 12345678, Name = "Ole Jensen" };
            Assert.ThrowsException<ArgumentException>(() => departureTimeNotOK.ValidateDepartureTime());
            Departure departureQRCodeNotOK = new Departure() { Id = 1, DepartureTime = DateTime.Now.AddHours(-25), QrCode = 1234567, Name = "Ole Jensen" };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => departureQRCodeNotOK.ValidateQrCode());
            Departure departureNameNull = new Departure() { Id = 1, DepartureTime = DateTime.Now.AddHours(-25), QrCode = 12345678, Name = null };
            Assert.ThrowsException<ArgumentNullException>(() => departureNameNull.ValidateName());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Departure departure = new Departure() { Id = 1, DepartureTime = DateTime.Now, QrCode = 12345678, Name = "Ole Jensen" };
            string tostr =  $"Id: 1, ArrivalTime: {DateTime.Now}, QrCode: {12345678}, Name: Ole Jensen";
            Assert.AreEqual(tostr, departure.ToString());

        }
    }
}