using Newtonsoft.Json;

namespace WebApplication.Models
{
    [JsonObject]
    public class SelectParamInfo
    {
        public string SelectSourceField { get; set; } 
        public string SelectConditionField { get; set; } 
        public string SelectConditionValue { get; set; } 
    }
}