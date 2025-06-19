using Newtonsoft.Json;
using System.Text.Json.Serialization;
//using System.Text.Json.Serialization;
using System.Text.Json;

namespace KironTestWebAPI.Models.Entities
{
    public class UKBankHolidays
    {
        public class EnglandAndWales
        {
            public string? division { get; set; }
            public List<Event>? events { get; set; }
        }

        public class NorthernIreland
        {
            public string? division { get; set; }
            public List<Event>? events { get; set; }
        }

        public class Scotland
        {
            public string? division { get; set; }
            public List<Event>? events { get; set; }
        }

        public class Event
        {
            public string? title { get; set; }
            public string? date { get; set; }
            public string? notes { get; set; }
            public bool bunting { get; set; }
        }

        public class Root
        {
            [JsonProperty("england-and-wales")]
            public EnglandAndWales? englandandwales { get; set; }
            public Scotland? scotland { get; set; }

            [JsonProperty("northern-ireland")]
            public NorthernIreland? northernireland { get; set; }
        }

    }

    public class HolidayEvent
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("notes")]
        public string Notes { get; set; }

        [JsonPropertyName("bunting")]
        public bool Bunting { get; set; }
    }

    public class Divisions
    {
        [JsonPropertyName("division")]
        public string DivisionName { get; set; }

        [JsonPropertyName("events")]
        public List<HolidayEvent> Events { get; set; }
    }

    // This represents the top-level dictionary structure
    public class HolidayData : Dictionary<string, Divisions> { }

}
