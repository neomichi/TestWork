using System;

namespace Motiv.Core.Model
{
    public class Subscription
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Limit { get; set; }
        public double Median { get; set; }
        public double FreeTraffics { get; set; }

        public string SecondsLeft { get; set; }



    }
}
