using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Posthuman.Core;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using Posthuman.Services.Helpers;

namespace Posthuman.Services.Tests
{
    [TestFixture]
    public class DaysOfWeekUtilsTests
    {
        #region Test Methods
        [Test]
        public void TestNearestDayCalculation()
        {
            DateTime now = DateTime.Now;
            Habit habit = new Habit
            {
                DaysOfWeek = 48,
                RepetitionPeriod = RepetitionPeriod.Weekly,
                LastCompletedInstanceDate = null,
            };


            var mock = new Mock<IUnitOfWork>();
            var data = "asd";
            var days = DaysOfWeekUtils.DaysOfWeek(habit.DaysOfWeek);
            if(days.Contains(now.DayOfWeek))
            {
                habit.NextIstanceDate = now;
            }
            else
            {
                
            }
        }

        [Test]
        public void testDaysOfTheWeek()
        {
            // Not doing this in a nested loop just
            // in order to see the structure clearly.
            foreach (DayOfWeek[] combination in combinations(7))
                testDaysOfWeek(combination);

            foreach (DayOfWeek[] combination in combinations(6))
                testDaysOfWeek(combination);

            foreach (DayOfWeek[] combination in combinations(5))
                testDaysOfWeek(combination);

            foreach (DayOfWeek[] combination in combinations(4))
                testDaysOfWeek(combination);

            foreach (DayOfWeek[] combination in combinations(3))
                testDaysOfWeek(combination);

            foreach (DayOfWeek[] combination in combinations(2))
                testDaysOfWeek(combination);

            foreach (DayOfWeek[] combination in combinations(1))
                testDaysOfWeek(combination);
        }

        private IEnumerable<DayOfWeek[]> combinations(int arrayLength)
        {
            if (arrayLength == 0)
                yield return new DayOfWeek[0];
            else
                foreach (DayOfWeek dayOfWeek in DaysOfWeekUtils.EachDayOfTheWeek)
                    foreach (DayOfWeek[] combination in combinations(arrayLength - 1))
                        yield return combination.Concat(Enumerable.Repeat(dayOfWeek, 1)).ToArray();
        }

        [Test]
        public void testDaysOfTheWeekUtilsProperty()
        {
            IEnumerable<DayOfWeek> daysOfTheWeek = DaysOfWeekUtils.EachDayOfTheWeek;
            Assert.AreEqual(daysOfTheWeek.Count(), 7);
            Assert.IsTrue(daysOfTheWeek.Contains(DayOfWeek.Sunday));
            Assert.IsTrue(daysOfTheWeek.Contains(DayOfWeek.Monday));
            Assert.IsTrue(daysOfTheWeek.Contains(DayOfWeek.Tuesday));
            Assert.IsTrue(daysOfTheWeek.Contains(DayOfWeek.Wednesday));
            Assert.IsTrue(daysOfTheWeek.Contains(DayOfWeek.Thursday));
            Assert.IsTrue(daysOfTheWeek.Contains(DayOfWeek.Friday));
            Assert.IsTrue(daysOfTheWeek.Contains(DayOfWeek.Saturday));
        }

        [Test]
        public void testMondayToFriday()
        {
            IEnumerable<DayOfWeek> mondayToFriday = DaysOfWeekUtils.MondayToFriday;
            Assert.IsTrue(mondayToFriday.Count() == 5);

            Assert.IsTrue(mondayToFriday.Contains(DayOfWeek.Monday));
            Assert.IsTrue(mondayToFriday.Contains(DayOfWeek.Tuesday));
            Assert.IsTrue(mondayToFriday.Contains(DayOfWeek.Wednesday));
            Assert.IsTrue(mondayToFriday.Contains(DayOfWeek.Thursday));
            Assert.IsTrue(mondayToFriday.Contains(DayOfWeek.Friday));
        }

        [Test]
        public void testNonExistentDays()
        {
            IEnumerable<DayOfWeek> noDays = DaysOfWeekUtils.DaysOfWeek(256);
            Assert.AreEqual(noDays.Count(), 0);
        }
        #endregion

        #region Private Methods
        private void testDaysOfWeek(params DayOfWeek[] daysOfWeek)
        {
            int val = DaysOfWeekUtils.ValueOf(daysOfWeek);
            IEnumerable<DayOfWeek> computedEnumeration = DaysOfWeekUtils.DaysOfWeek(val);
            Assert.AreEqual(daysOfWeek.Distinct().Count(), computedEnumeration.Count());
            foreach (DayOfWeek dayOfWeek in computedEnumeration)
                Assert.IsTrue(daysOfWeek.Contains(dayOfWeek));
        }
        #endregion
    }
}