using System;
using System.ComponentModel;
using System.Globalization;

namespace Motiv.Data
{
    public static class Constant
    {
        public  enum TimeRangeEnum
        {
            [Description("осталось")]
            Left=0,
            [Description("секунд")]
            Sec=4,
            [Description("минут")]
            Min=3,
            [Description("часов")]
            Hour=2,
            [Description("дней")]
            Day=1,           
           
        }

        public const string Url1 = "https://lisa.motivtelecom.ru/";
        public const string Url2 = "https://lisa.motivtelecom.ru/rest_of_packets/";


        public const string RegexDateTime = @"(\d{2}.\d{2}.\d{4} \d{2}:\d{2}:\d{2})";
        public const string RegexDoubleCost = @"(-?[0-9]+(\.[0-9]+)?) (GB|MB|B)";
        public const string RegexDouble = @"[0-9]*[.,]?[0-9]+";

        public static string GetStringTime(this TimeRangeEnum range, System.TimeSpan val)
        {
            var num = 0;
            switch (range)
            {
                case TimeRangeEnum.Min: num = (int)val.TotalMinutes;break;
                case TimeRangeEnum.Day: num = (int)val.TotalDays; break;
                case TimeRangeEnum.Hour: num = (int)val.TotalHours; break;
                case TimeRangeEnum.Sec: num = (int)val.TotalSeconds; break;
            }

           // var num = val % 100;
            if (num>=11 && num<=19)
            {
                return new string[] { "осталось", "дней", "часов", "минут", "секунд"}[(int)range];
            }

            num = num % 10;

            switch (num)
            {              
                case 1: return new string[] { "остался", "день", "час", "минута", "секунда" }[(int)range];
                case 2:
                case 3: 
                case 4: return new string[] { "осталось", "дня", "часа", "минуты", "секунды" }[(int)range];
                case 5:             
                case 6:
                case 7:
                case 8:
                case 9:
                case 0: return new string[] { "осталось", "дней", "часов", "минут", "секунд" }[(int)range];
                default:return "";
            }


        }

        /// <summary>
        /// CultureInfo.InvariantCulture
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Double ToDouble(this string value )
        {
            return Double.Parse(value, CultureInfo.InvariantCulture);
        }

    }
}
