using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestApp.Dto
{
    public class OpenExchangeRatesDto
    {
        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; } = default;

        [JsonProperty("license")]
        public string License { get; set; } = default;

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; } = default;

        [JsonProperty("base")]
        public string Base { get; set; } = default;

        [JsonProperty("rates")]
        public Dictionary<string, double> Rates { get; set; } = default;
    }
}
