using System.Text.Json.Serialization;

namespace KironTestWebAPI.Models
{
    public class NavigationRecursiveDTO
    {
        public string Text { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<NavigationRecursiveDTO>? Children { get; set; }
    }
}
