using System;

namespace TestApp.Models
{
    public class QuotesModel
    {
        public string QuoteName { get; }
        public double QuoteValue { get; }

        public QuotesModel(string quoteName,
                           double quoteValue)
        {
            QuoteName = quoteName;
            QuoteValue = quoteValue;
        }
    }
}
