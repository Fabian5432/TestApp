using System;

namespace TestApp.Models
{
    public class RatesModel
    {
        public string RateName { get; }
        public double RateValue { get; }

        public RatesModel(string rateName, 
                          double rateValue)
        {
            RateName = rateName;
            RateValue = rateValue;
        }
    }
}
