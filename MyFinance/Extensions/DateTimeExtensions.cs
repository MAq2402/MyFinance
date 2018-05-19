using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GenerateFromString(this DateTime dateTime,string str)
        {
            
            if(string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException();
            }

            var splitedDateTime = str.Split('.');

            if (splitedDateTime.Length != 3)
            {
                throw new ArgumentException();
            }

            var day = Convert.ToInt32(splitedDateTime[0]);
            var month = Convert.ToInt32(splitedDateTime[1]);
            var year = Convert.ToInt32(splitedDateTime[2]);

            return new DateTime(year, month, day);
        }
    }
}
