using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        //Extension method
        public static int GetCurrentAge(this DateTimeOffset dateOfBirth)
        {
            var currentDate = DateTimeOffset.UtcNow;
            var age = currentDate.Year - dateOfBirth.Year;
            if (currentDate < dateOfBirth.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
