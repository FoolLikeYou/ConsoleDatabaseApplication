using Newtonsoft.Json;

namespace WebApplication.Models
{
    [JsonObject]
    public class SelectInfo
    {
        public string SelectConditionField { get; set; } 
        public string SelectConditionValue { get; set; }
    }
}