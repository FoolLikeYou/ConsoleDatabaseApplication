using Newtonsoft.Json;

namespace ConsoleClient
{
    [JsonObject]
    public class SelectInfo
    {
        public string SelectConditionField { get; set; } 
        public string SelectConditionValue { get; set; } 
    }
}