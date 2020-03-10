using Newtonsoft.Json;

namespace WebApplication.Models
{
    [JsonObject]
    public class DeleteInfo
    {
        public string DeletedField { get; set; }
        public string DeletedValue { get; set; }
    }
}