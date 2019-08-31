namespace Motiv.Core
{
    public static class Constant
    {
        public const string RegexDateTime = @"(\d{2}.\d{2}.\d{4} \d{2}:\d{2}:\d{2})";
        public const string RegexDuoble = @"(-?[0-9]+(\.[0-9]+)?) (GB)";
        

        public static string GetDays(int val)
        {
            var num = val % 100;
            if (num>=11 && num<=19)
            {
                return string.Format("осталось| {0} |дней|часов|минут", val);
            }

            num = val % 10;

            switch (num)
            {              
                case 1: return string.Format("остался| {0} |день|час|минута", val);
                case 2:
                case 3: 
                case 4: return string.Format("осталось| {0} |дня|часа|минуты", val);
                case 5:             
                case 6:
                case 7:
                case 8:
                case 9:
                case 0: return string.Format("осталось| {0} |дней|часов|минут", val);
                default:return "";
            }
        }

        


    }
}
