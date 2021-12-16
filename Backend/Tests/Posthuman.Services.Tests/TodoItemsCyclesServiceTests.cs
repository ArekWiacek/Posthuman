using NUnit.Framework;
using Posthuman.Core.Models.Enums;
using System;
using System.Collections;

namespace Posthuman.Services.Tests
{
    [TestFixture]
    public class TodoItemsCyclesServiceTests
    {
        private TodoItemsCyclesService sut;
        private DateTime startDate;
        private DateTime endDate;

        [SetUp]
        public void Setup()
        {
            sut = new TodoItemsCyclesService();

            startDate = new DateTime(2021, 5, 10);
            endDate = new DateTime(2021, 7, 2);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        
        public class TestData
        {
            public TestData(
                RepetitionPeriod repetitionPeriod, 
                DateTime startDate, 
                DateTime? endDate, 
                bool expectedIsInfinite, 
                bool shouldThrowException = false)
            {
                this.RepetitionPeriod = repetitionPeriod;
                this.StartDate = startDate;
                this.EndDate = endDate;
                this.ExpectedIsInfinite = expectedIsInfinite;
                this.ShouldThrowException = shouldThrowException;
            }
            public RepetitionPeriod RepetitionPeriod { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public bool ExpectedIsInfinite { get; set; }
            public bool ShouldThrowException { get; set; }
        }

        private static TestData[] testData = new[]
        {
            new TestData(RepetitionPeriod.Daily, new DateTime(2021, 5, 15), null, true),
            new TestData(RepetitionPeriod.Daily, new DateTime(2021, 5, 15), new DateTime(2021, 5, 16), false),
            new TestData(RepetitionPeriod.Daily, new DateTime(2021, 5, 15), new DateTime(2021, 5, 20), false),
            new TestData(RepetitionPeriod.Daily, new DateTime(2021, 5, 15), new DateTime(2021, 6, 2), false, true),
            new TestData(RepetitionPeriod.Daily, new DateTime(2021, 5, 15), new DateTime(2021, 5, 10), false)
        };

        [Test]
        public void CreateCycleInfo_WhenEndDateInvalid_ThrowsException([ValueSource("testData")] TestData testData)
        { 
            if (testData.ShouldThrowException)
                Assert.Throws<ArgumentException>(() => { sut.CreateCycleInfo(testData.RepetitionPeriod, testData.StartDate, testData.EndDate); });

            var createdCycle = sut.CreateCycleInfo(testData.RepetitionPeriod, testData.StartDate, testData.EndDate);

            Assert.AreEqual(testData.RepetitionPeriod, createdCycle.RepetitionPeriod);
            Assert.AreEqual(testData.StartDate, createdCycle.StartDate);
            Assert.AreEqual(testData.EndDate, createdCycle.EndDate);
            Assert.AreEqual(testData.ExpectedIsInfinite, createdCycle.IsInfinite);
        }

        [Test]
        public void CreateTodoItemCycleTest([ValueSource("testData")]TestData testData)
        {
            if (testData.ShouldThrowException)
                Assert.Throws<ArgumentException>(() => { sut.CreateCycleInfo(testData.RepetitionPeriod, testData.StartDate, testData.EndDate);});
            
            var createdCycle = sut.CreateCycleInfo(testData.RepetitionPeriod, testData.StartDate, testData.EndDate);

            Assert.AreEqual(testData.RepetitionPeriod, createdCycle.RepetitionPeriod);
            Assert.AreEqual(testData.StartDate, createdCycle.StartDate);
            Assert.AreEqual(testData.EndDate, createdCycle.EndDate);
            Assert.AreEqual(testData.ExpectedIsInfinite, createdCycle.IsInfinite);
        }


        [Test]
        [TestCase(RepetitionPeriod.Daily, "", "")]
        [TestCase(RepetitionPeriod.Daily, "5-11-2021", "")]
        [TestCase(RepetitionPeriod.Daily, "", "5-11-2021")]
        [TestCase(RepetitionPeriod.Daily, "5-11-2021", "5-11-2021")]
        [TestCase(RepetitionPeriod.Daily, "5-11-2021", "6-11-2021")]
        [TestCase(RepetitionPeriod.Daily, "5-11-2021", "18-11-2021")]
        [TestCase(RepetitionPeriod.Daily, "5-11-2021", "3-12-2021")]
        public void CreateTodoItemCycle(RepetitionPeriod repetitionPeriod, DateTime startDate, DateTime? endDate)
        {
            var createdCycle = sut.CreateCycleInfo(repetitionPeriod, startDate, endDate);

            Assert.AreEqual(repetitionPeriod, createdCycle.RepetitionPeriod);
            Assert.AreEqual(startDate, createdCycle.StartDate);
            Assert.AreEqual(endDate, createdCycle.EndDate);
            Assert.Pass();
        }
    }
}