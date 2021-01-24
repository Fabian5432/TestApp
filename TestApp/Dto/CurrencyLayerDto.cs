using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestApp.Dto
{
    public class CurrencyLayerDto
    {
        [JsonProperty("success")]
        public bool Success { get; set; } = default;

        [JsonProperty("terms")]
        public string Terms { get; set; } = default;

        [JsonProperty("privacy")]
        public string Privacy { get; set; } = default;

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; } = default;

        [JsonProperty("source")]
        public string Source { get; set; } = default;

        [JsonProperty("quotes")]
        public Dictionary<string, double> Quotes { get; set; } = default;
    }
}
