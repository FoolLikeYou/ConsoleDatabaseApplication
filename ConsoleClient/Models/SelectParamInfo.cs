using Newtonsoft.Json;

namespace ConsoleClient
{
    [JsonObject]
    public class SelectParamInfo
    {
        public string SelectSourceField { get; set; } 
        public string SelectConditionField { get; set; } 
        public string SelectConditionValue { get; set; } 
    }
}