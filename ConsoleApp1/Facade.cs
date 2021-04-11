using System;

namespace TestKaastrupAndersen
{
    public class Facade : IFacade
    {
        private DateTime _currentDateTime;
        public Facade(DateTime currentDateTime)
        {
            _currentDateTime = (DateTime) currentDateTime;
        }

        public Facade()
        {
            _currentDateTime = DateTime.Now;
        }

        public string GenerateSalaryMessage(DateTime startedWork)
        {
            //for every 3 hours, add 1 additional hour
            //if day is saturday or sunday, every hour is doubled.
            // Does this stack? working 3 hours in a saturday adds 6 hours, that mean 2 additional hours? so are those bonus hours also doubled to 4 hours?
            //In this case, i assume the bonus hour for working more than 3 hours is NOT doubled.
            //If totalhours (bonus + worked hours) is more than 7, add 200kr to income as bonus. 
            //Since this is exclusivly touching the value INCOME not hours, and also needs to take in bonus hours, this will be done last.


            if (_currentDateTime < startedWork || _currentDateTime.Day != startedWork.Day)
            {
                throw new ArgumentOutOfRangeException(nameof(startedWork),
                    "Start of work is either on a different date, or later than current time");
            }
            else
            {
                var hoursWorked = CalculateHoursWorked(startedWork);
                var salary = CalculateSalary(hoursWorked);    //This is a step i would usually not do.
                                                                    //Sometimes i meet people who INSIST on keeping this syntax because "its easier to read"
                                                                    //Considering the simplicity of the task at hand, and since this would be a personal program for 1 developer
                                                                    //i didn't really see any value in doing so.

                return "You worked " + hoursWorked + " hours and salary is " + salary + ".";
            }
        }

        public double CalculateSalary(int hoursWorked)
        {
            return hoursWorked > 7 ? 130 * hoursWorked + 200 : 130 * hoursWorked;
        }

        public int CalculateHoursWorked(DateTime start)
        {
            int hoursWorked;
            if (_currentDateTime.DayOfWeek == DayOfWeek.Saturday || _currentDateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                hoursWorked = (int)(_currentDateTime - start).TotalHours * 2; // Because we only care about hours not minutes, but still need to count 8.15 - 9.14 as less than 1 hour
            }
            else
            {
                hoursWorked = (int)(_currentDateTime - start).TotalHours; // Because we only care about hours not minutes, but still need to count 8.15 - 9.14 as less than 1 hour
            }

            hoursWorked = hoursWorked > 3 ? hoursWorked + 1 : hoursWorked;
            return hoursWorked;
        }
    }
}