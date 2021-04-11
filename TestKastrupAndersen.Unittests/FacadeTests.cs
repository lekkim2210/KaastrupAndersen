using NUnit.Framework;
using System;
using TestKaastrupAndersen;

namespace TestKastrupAndersen.Unittests
{
    [TestFixture]
    public class FacadeTests
    {
       //When working more than 3 hours, extra hour is added.
       [Test]
        public void NoWeekendNoBonusHour()
        {
            var time = new DateTime(2021, 04, 11, 0, 0, 0);
            if (time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday)
            {
                time = time.AddDays(2);
            }
            var f = new Facade(time);
            Assert.AreEqual(3, f.CalculateHoursWorked(time.AddHours(-3)));
        }

        [Test]
        public void NoWeekendWithBonusHour([Values(4, 7, 10)] int hours)
        {
            var time = new DateTime(2021, 04, 11, 0,0,0);
            if (time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday)
            {
                time = time.AddDays(2);
            }
            var f = new Facade(time);
            Assert.AreEqual(hours+1, f.CalculateHoursWorked(time.AddHours(-hours)));
        }

        [Test]
        public void WeekendWithBonusHour([Values(4, 7, 10)] int hours)
        {
            var time = new DateTime(2021, 04, 11, 0, 0, 0);
            if (time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday)
            {
                time = time.AddDays(2);
            }
            var f = new Facade(time);
            Assert.AreEqual(hours + 1, f.CalculateHoursWorked(time.AddHours(-hours)));
        }

        [Test]
        public void WeekendNoBonusHour([Values(1, 0)] int hours)
        {
            var time = new DateTime(2021, 04, 11, 0, 0, 0);
            var f = new Facade(time);
            Assert.AreEqual(hours*2 , f.CalculateHoursWorked(time.AddHours(-hours)));
        }

        [Test]
        public void GenerateSalaryMessage()
        {
            var time = new DateTime(2021, 04, 11, 0, 0, 0);
            var f = new Facade(time);
            time = time.AddDays(1);
            Assert.Throws<ArgumentOutOfRangeException>(() => f.GenerateSalaryMessage(time));
        }

        [Test]
        public void GenerateSalaryMessageNoThrow0HoursWorked()
        {
            var time = new DateTime(2021, 04, 11, 0, 0, 0);
            var f = new Facade(time);
            Assert.DoesNotThrow(() => f.GenerateSalaryMessage(time));
        }

        [Test]
        public void GenerateSalaryMessageNoThrowsLessThan24HoursWorked([Values(1,3,5,8, 9, 23)] int hoursWorked)
        {
            var time = new DateTime(2021, 04, 11, 0, 0, 0);
            var f = new Facade(time.AddHours(hoursWorked));
            Assert.DoesNotThrow(() => f.GenerateSalaryMessage(time));
        }
    }
}
