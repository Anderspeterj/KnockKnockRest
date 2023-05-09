using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnockKnockRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnockKnockRest.Repositories;

namespace KnockKnockRest.Models.Tests
{
    [TestClass()]
    public class StudentTests
    {

        StudentsRepository repo;

        Student studentClean = new Student()
        {
            Name = "John",
            QrCode = 58472328,
            Address = "1234 Main St"
        };
        Student studentFail = new Student() { Id = 0, Name = "Anders", Address = "ringsted", QrCode = 32302657 };



        [TestMethod()]
        public void ValidateNameTest()
        {
            studentClean.ValidateName();
            studentFail.Name = null;
            Assert.ThrowsException<ArgumentNullException>(() => studentFail.ValidateName());
            studentFail.Name = "asjdjadkwawmdwadddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddadsadwadwadwadwadwadmwamdwamdmwamdwamdmamdwa";
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => studentFail.ValidateName());
            studentFail.Name = "4";
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => studentFail.ValidateName());

        }

        [TestMethod()]
        public void ValidateQrCodeTest()
        {
            studentClean.ValidateQrCode();
            
        }

        [TestMethod()]
        public void ValidateAddressTest()
        {
            Assert.Fail();
        }
    }
}