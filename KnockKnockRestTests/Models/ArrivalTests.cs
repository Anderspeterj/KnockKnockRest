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
    public class ArrivalTests
    {
        Arrival arrivalClean = new Arrival()
        {
            ArrivalTime = DateTime.Now,
            QrCode = 58472328
        };

        Arrival arrivalFail = new Arrival()
        {
            
            QrCode = 123456789
        };

        [TestMethod()]
        public void ValidateArrivalTimeTest()
        {
            arrivalFail.ArrivalTime = DateTime.Now.AddHours(-25);
            Assert.ThrowsException<InvalidOperationException>(() => arrivalFail.ValidateArrivalTime());
            arrivalClean.ValidateArrivalTime();

        }

        [TestMethod()]
        public void ValidateArrivalQrCodeTest()
        {
            arrivalClean.ValidateQrCode();
            arrivalFail.QrCode = 1234;
            Assert.ThrowsException<ArgumentException>(() => arrivalFail.ValidateQrCode());
            arrivalFail.QrCode = 1234567890;
            Assert.ThrowsException<ArgumentException>(() => arrivalFail.ValidateQrCode());
            arrivalFail.QrCode = null;
            Assert.ThrowsException<ArgumentException>(() => arrivalFail.ValidateQrCode());
        }
    }
}