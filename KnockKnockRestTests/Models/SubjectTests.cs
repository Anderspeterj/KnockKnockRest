using KnockKnockRest.Models;
using KnockKnockRest.Repositories;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnockKnockRestTests.Models
{
    [TestClass()]
    public class SubjectTests
    {

        [TestMethod()]
        public void SubjectProperties()
        {
            string name = "Matematik";
            TimeSpan startTime = new TimeSpan(12, 0, 0);
            TimeSpan endTime = new TimeSpan(12, 45, 0);

            Subject subject = new Subject
            {
                Name = name,
                StartTime = startTime,
                EndTime = endTime
            };

            Assert.AreEqual(name, subject.Name);
            Assert.AreEqual(startTime, subject.StartTime);
            Assert.AreEqual(endTime, subject.EndTime);
        }

        [TestMethod]
        public void SubjectNullName()
        {
            // Arrange
            string invalidName = null;
            Subject subject = new Subject();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => subject.Name = invalidName);

        }

        [TestMethod]
        public void SubjectMaxLength()
        {
            // Arrange
            string invalidName = "Matematematematiktiktiktik";
            Subject subject = new Subject();

            // Act and Assert
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => subject.Name = invalidName);
            Assert.AreEqual("Name", ex.ParamName);
            StringAssert.Contains(ex.Message, "Subject name is too long!");
        }
        [TestMethod]
        public void SubjectMinLength()
        {
            // Arrange
            string invalidName = "T";
            Subject subject = new Subject();

            // Act and Assert
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => subject.Name = invalidName);
            Assert.AreEqual("Name", ex.ParamName);
            StringAssert.Contains(ex.Message, "Subject name has to be longer!");
        }
    }


    
}
