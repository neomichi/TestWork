using System;
using System.Collections.Generic;

namespace Motiv.Data.Model
{
    public class Subscription
    {/// <summary>
     /// тариф
     /// </summary>
        public string Tariff { get; set; }
        /// <summary>
        /// дата начала подписки
        /// </summary>
        public DateTime? Start { get; set; }
        /// <summary>
        /// дата конца подписки
        /// </summary>
        public DateTime? End { get; set; }
        public int Limit { get; set; }
        /// <summary>
        /// норма потребления
        /// </summary>
        public string Median { get; set; }

        public double Median2 { get; set; }
        /// <summary>
        /// свободный график
        /// </summary>
        public string FreeTraffic { get; set; }
        /// <summary>
        /// единица измерения
        /// </summary>
        /// ///public string TrafficUnit { get; set; }

        public string TimeLeft { get; set; }
        //баланс
        public string Ballance { get; set; }

        public List<Option> Options { get; set; }
    }

    public class Option
    {
        public string Title { get; set; }
        public DateTime End { get; set; }

    }

}
